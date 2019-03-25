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

namespace Sarif
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> errors = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        // Méthode pour afficher la page de gestion d'ajout dans la frame.
        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new addProduct();
        }
        // Méthode pour afficher la page de gestion d'ajout de produit dans la frame.
        private void Stock_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new productList();
        }
        // Méthode pour afficher la page d'accueil dans la frame au lancement du client.
        private void load_Home(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Home();
        }
        // Méthode pour afficher la page d'accueil dans la frame.
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Home();
        }
        // Méthode pour afficher la fenêtre "À propos".
        private void About_Click(object sender, RoutedEventArgs e)
        {
            about aboutWindow = new about();
            aboutWindow.Show();
        }
        // Méthode pour afficher la liste des produits dans la frame.
        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new shop();
        }
        // Méthode pour afficher le login pour passer en admin.
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Visible;
        }
        
        // Méthode pour afficher le login.
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (adminName.Text == "Amelie" && adminPassword.Password == "lovepicardie")
            {
                errors.Clear();
                MessageBox.Show("Bienvenue Amélie !", "Connecté", MessageBoxButton.OK, MessageBoxImage.Information);
                Manage.Visibility = Visibility.Visible;
                Stock.Visibility = Visibility.Visible;
                Disconnect.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Hidden;
                loginGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                errors.Add("adminName", "Nom incorrect");
                errors.Add("adminPassword", "Mot de passe incorrect");
                foreach (KeyValuePair<string, string> error in errors)
                {
                    (FindName(error.Key + "Label") as Label).Content = error.Value;
                }
            }
        }

        // Méthode pour enlever les indications d'erreurs.
        private void ClearErrors()
        {
            errors.Clear();
            // Remise à zéro des labels.
            adminNameLabel.Content = "";
            adminPasswordLabel.Content = "";
        }

        // Méthode pour fermer le login.
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Hidden;
        }

        // Méthode pour se déconnecter du mode admin.
        private void Disc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Déconnecté", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            Login.Visibility = Visibility.Visible;
            Manage.Visibility = Visibility.Hidden;
            Stock.Visibility = Visibility.Hidden;
            Disconnect.Visibility = Visibility.Hidden;
        }
    }
}