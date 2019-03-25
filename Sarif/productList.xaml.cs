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
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.IO;

namespace Sarif
{
    /// <summary>
    /// Logique d'interaction pour prductList.xaml
    /// </summary>
    public partial class productList : Page
    {
        // Instanciation de la db sarifInc.
        private sarifInc db = new sarifInc();
        categories categories;
        product product;

        public productList()
        {
            InitializeComponent();
            categories = new categories();
            product = new product();
        }

        // Méthode pour afficher la db sous forme de liste dans la datagrid.
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            productDataGrid.ItemsSource = db.product.ToList();
        }
        // Méthode affichant les éléments et l'image du produit selectionné dans les champs correspondant.
        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product = productDataGrid.SelectedItem as product;

            if (product != null)
            {
                editName.Text = product.name;
                editPrice.Text = Convert.ToString(product.price);
                editCategorie.ItemsSource = db.categories.ToList();
                for (int i = 0; i < editCategorie.Items.Count; i++)
                {
                    categories category = editCategorie.Items[i] as categories;
                    if (product.idCategorie == category.idCategorie)
                    {
                        editCategorie.SelectedIndex = i;
                    }

                }
                editNumber.Text = Convert.ToString(product.number);
                var ms = new MemoryStream(product.img);
                var returnImage = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                previewProduct.Source = returnImage;
                previewProduct.Visibility = Visibility.Visible;
            }
        }

        // Méthode pour remettre à zéro les champs.
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            editName.Text = "";
            editNumber.Text = "";
            editCategorie.SelectedIndex = -1;
            editPrice.Text = "";
        }

        // Méthode permettant de modifier et ajouter les nouvelles valeurs suite à la vérification de la validité de chaque champs par les Regex.
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Êtes vous sur de vouloir modifier ?", "Confirmation", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Déclaration des Regex.
                string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
                string regexNumber = @"^[0-9]{1,7}$";
                bool isValid = true;
                int error = 0;

                // Vérification si le champs n'est pas vide ou null.
                if (!String.IsNullOrEmpty(editName.Text))
                {
                    // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                    if (!Regex.IsMatch(editName.Text, regexName))
                    {
                        // Ajout du message d'erreur concernant l'erreur lié au champ en question dans la grille d'erreur.
                        ErrorMessage.Text += "Veuillez saisir un nom valide";
                        isValid = false;
                        error++;
                    }
                    // Si il n'y a pas d'erreur on affiche la valeur de la db au champ correspondant.
                    else
                    {
                        product.name = editName.Text;
                    }
                }
                else
                {
                    // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                    ErrorMessage.Text += Environment.NewLine + "Veuillez saisir un nom";
                    isValid = false;
                    error++;
                }
                // Vérification si le champs n'est pas vide ou null.
                if (!String.IsNullOrEmpty(editNumber.Text))
                {
                    // Utilisation de la regex "regexNumber" + ajout au compteurs d'erreur si il y en a une.
                    if (!Regex.IsMatch(editNumber.Text, regexNumber))
                    {
                        // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                        ErrorMessage.Text += Environment.NewLine + "Veuillez saisir une quantité valide";
                        isValid = false;
                        error++;
                    }
                    // Si il n'y a pas d'erreur on affiche la valeur de la db au champ correspondant.
                    else
                    {
                        product.number = Convert.ToInt32(editNumber.Text);
                    }
                }
                else
                {
                    // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                    ErrorMessage.Text += Environment.NewLine + "Veuillez saisir une quantité";
                    isValid = false;
                    error++;
                }

                if (editCategorie.SelectedIndex == -1)
                {
                    // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                    ErrorMessage.Text += Environment.NewLine + "Veuillez selectionner une catégorie";
                    isValid = false;
                    error++;
                }
                // Si il n'y a pas d'erreur on affiche la valeur de la db au champ correspondant.
                else
                {
                    product.idCategorie = ((categories)editCategorie.SelectedItem).idCategorie;
                }
                // Vérification si le champs n'est pas vide ou null.
                if (!String.IsNullOrEmpty(editPrice.Text))
                {
                    // Utilisation de la regex "regexNumber" + ajout au compteurs d'erreur si il y en a une.
                    if (!Regex.IsMatch(editPrice.Text, regexNumber))
                    {
                        // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                        ErrorMessage.Text += Environment.NewLine + "Veuillez saisir un prix valide";
                        isValid = false;
                        error++;
                    }
                    // Si il n'y a pas d'erreur on affiche la valeur de la db au champ correspondant.
                    else
                    {
                        product.price = Convert.ToInt32(editPrice.Text);
                    }
                }
                else
                {
                    // Utilisation de la regex "regexName" + ajout au compteurs d'erreur si il y en a une.
                    ErrorMessage.Text += Environment.NewLine + "Veuillez saisir un prix";
                    isValid = false;
                    error++;
                }

                // Si il n'y a pas d'erreur on ajout la valeur modifié à la db puis sauvegarde suivi d'un méssage de succès.
                if (isValid == true)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    productDataGrid.Items.Refresh();
                    errorGrid.Visibility = Visibility.Hidden;

                    MessageBox.Show("Le produit a bien été modifié", "Succes");
                }
                else
                {
                    // Affichage de la grille d'erreur s'il y en a avec l'indication des champs érronés + message box affichant le nombre d'erreur trouvé.
                    errorGrid.Visibility = Visibility.Visible;
                    MessageBox.Show(error + " Erreur(s) détectée(s)");
                }
            }
        }

        // Méthode permettant la suppression de la ligne dans la db.
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Affichage d'une message pour la confirmation de la supression des données selectionnés.
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Êtes vous sûr de vouloir supprimer ce rendez-vous ?", "Confirmation", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Ajout d'une variable local pour stocké le produit selectionné qui va être supprimé.
                product productToDelete = productDataGrid.SelectedItem as product;

                if (productToDelete != null)
                {
                    // Suppression de la ligne selectionné.
                    db.product.Remove(product);
                    // Sauvegarde de la suppression.
                    db.SaveChanges();
                    // Remise à zéro puis réaffichage de la datagrid pour mettre à jour l'affichage suivit d'un méssage de succès.
                    productDataGrid.ItemsSource = null;
                    productDataGrid.ItemsSource = db.product.ToList();
                    previewProduct.Visibility = Visibility.Hidden;
                    MessageBox.Show("Le produit a bien été supprimé", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Méthode permettant de fermer/cacher la "pop-up" d'erreur au click sur la croix.
        private void closeGrid_Click(object sender, RoutedEventArgs e)
        {
            errorGrid.Visibility = Visibility.Hidden;
        }

        // Méthode pour trier le tableau non mise au point.

        //private void Sort_Click(object sender, RoutedEventArgs e)
        //{
        //    int SortVar = Convert.ToInt32(categoryDropDown.SelectedValue);
        //    switch (SortVar)
        //    {
        //        case 1:
        //            productDataGrid.ItemsSource = db.product.Where(TempProduct => TempProduct.categories.idCategorie == 1).ToList();
        //            break;
        //        case 2:
        //            productDataGrid.ItemsSource = db.product.Where(TempProduct => TempProduct.categories.idCategorie == 2).ToList();
        //            break;
        //        case 3:
        //            productDataGrid.ItemsSource = db.product.Where(TempProduct => TempProduct.categories.idCategorie == 3).ToList();
        //            break;
        //        case 4:
        //            productDataGrid.ItemsSource = db.product.Where(TempProduct => TempProduct.categories.idCategorie == 4).ToList();
        //            break;

        //        default:
        //            productDataGrid.ItemsSource = db.product.ToList();
        //            break;

        //    }
        //}
        //// Bouton de suppression du choix de tri, on remet la methode d'affichage de base 
        //private void NoSort_Click(object sender, RoutedEventArgs e)
        //{
        //    productDataGrid.ItemsSource = db.product.ToList();
        //}
    }
}
