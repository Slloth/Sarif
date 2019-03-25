using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;

namespace Sarif
{
    /// <summary>
    /// Logique d'interaction pour addProduct.xaml
    /// </summary>
    public partial class addProduct : Page
    {
        // Instanciation de la db.
        private sarifInc db = new sarifInc();
        Dictionary<string, string> errors = new Dictionary<string, string>();

        categories categorieToAdd;
        // Ajout de regex.
        string regexLetter = @"^[A-Za-zéèàêâôûùïüç\-\. ]+$";
        string regexDigit = @"^[0-9]+";
        string regexSerial = @"^[a-zA-Z\-_]+[1-9]+";
        string regexDescription = @"^[a-zA-Z\-éêëèâäüûùîïöôÿ!.,;!? ]+";

        public addProduct()
        {
            InitializeComponent();
            categorieToAdd = new categories();
        }

        private void Combo(object sender, RoutedEventArgs e)
        {
            productCategory.ItemsSource = db.categories.ToList();
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            ClearErrors();
            // Instanciation de la table "customers" que j'ai nommé addProduct.
            product addProduct = new product();

            // Récupération et vérification des entrées pour le nom.
            if (!string.IsNullOrEmpty(productName.Text))
            {
                if (!Regex.IsMatch(productName.Text, regexLetter))
                {
                    errors.Add("productName", "Entrez un nom valide");
                }
            }
            else
            {
                errors.Add("productName", "Entrez un nom");
            }

            // Récupération et vérification des entrées pour la description.
            if (!string.IsNullOrEmpty(productDescription.Text))
            {
                if (!Regex.IsMatch(productDescription.Text, regexDescription))
                {
                    errors.Add("productDescription", "Entrez une description valide");
                }
            }
            else
            {
                errors.Add("productDescription", "Entrez une description");
            }

            // Récupération et vérification des entrées pour la quantité.
            if (!string.IsNullOrEmpty(productNumber.Text))
            {
                if (!Regex.IsMatch(productNumber.Text, regexDigit))
                {
                    errors.Add("productNumber", "Entrez une quantite valide");
                }
            }
            else
            {
                errors.Add("productNumber", "Entrez une quantite");
            }

            // Récupération et vérification des entrées pour le numéro de série.
            if (!string.IsNullOrEmpty(productSerial.Text))
            {
                if (!Regex.IsMatch(productSerial.Text, regexSerial))
                {
                    errors.Add("productSerial", "Entrez un n° de serie valide");
                }
            }
            else
            {
                errors.Add("productSerial", "Entrez un n° de serie");
            }

            // Récupération et vérification des entrées pour le prix.
            if (!string.IsNullOrEmpty(productPrice.Text))
            {
                if (!Regex.IsMatch(productPrice.Text, regexDigit))
                {
                    errors.Add("productPrice", "Entrez un prix valide");
                }
            }
            else
            {
                errors.Add("productPrice", "Entrez un prix");
            }

            //Récupération et vérification des entrées pour le chemin de l'image.
            if (string.IsNullOrEmpty(productImg.Text))
            {
                errors.Add("productImg", "Entrez le chemin de l'image");
            }

            // Récupération et vérification du choix de la ComboBox.
            if (productCategory.SelectedValue == null)
            {
                MessageBox.Show("Choisissez une catégorie !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (errors.Count == 0)
            {
                addProduct.name = productName.Text;
                addProduct.number = int.Parse(productNumber.Text);
                addProduct.price = int.Parse(productPrice.Text);
                addProduct.serialNumber = productSerial.Text;
                addProduct.description = productDescription.Text;
                addProduct.idCategorie = Convert.ToInt32(productCategory.SelectedValue);
                addProduct.img = File.ReadAllBytes(productImg.Text);

                // Ajout + sauvegarde de l'ajout dans la db.
                db.product.Add(addProduct);
                db.SaveChanges();

                // Message de confirmation d'ajout.
                MessageBox.Show("Produit ajouté !", "Succès", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                // Remise à zéro des textbox.
                productName.Text = "";
                productSerial.Text = "";
                productNumber.Text = "";
                productDescription.Text = "";
                productPrice.Text = "";
                productCategory.Text = "";
                productImg.Text = "";
            }
            else
            {
                foreach (KeyValuePair<string, string> error in errors)
                {
                    (FindName(error.Key) as TextBox).BorderBrush = Brushes.Red;
                    (FindName(error.Key + "Label") as Label).Content = error.Value;
                }
            }
        }

        // Méthode pour enlever les indications d'erreurs.
        private void ClearErrors()
        {
            errors.Clear();

            // Remise à zéro des labels.
            productNameLabel.Content = "";
            productSerialLabel.Content = "";
            productNumberLabel.Content = "";
            productDescriptionLabel.Content = "";
            productPriceLabel.Content = "";
            productImgLabel.Content = "";

            productName.BorderBrush = Brushes.Transparent;
            productSerial.BorderBrush = Brushes.Transparent;
            productNumber.BorderBrush = Brushes.Transparent;
            productDescription.BorderBrush = Brushes.Transparent;
            productPrice.BorderBrush = Brushes.Transparent;
            productImg.BorderBrush = Brushes.Transparent;
        }

        private void CancelCustomerForm_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Impossible de retourner en arrière.");
            }
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e) // Méhode pour parcourir le chemin où se situe l'image
        {
            // Création du système "OpenFileDialog" pour parcourir
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Lancer OpenFileDialog en appelant la méthode ShowDialog
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                productImg.Text = openFileDlg.FileName;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }
    }
}