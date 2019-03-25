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
using System.IO;

namespace Sarif
{
    /// <summary>
    /// Logique d'interaction pour shop.xaml
    /// </summary>
    public partial class shop : Page
    {
        // Instanciation de la db sarifInc.
        private sarifInc db = new sarifInc();
        product product;

        public shop()
        {
            InitializeComponent();
            product = new product();
        }

        // Méthode pour afficher la db sous forme de liste dans la datagrid.
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            productDataGrid.ItemsSource = db.product.ToList();
        }

        // Méthode affichant l'image du produit selectionné à l'endroit correspondant.
        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product = productDataGrid.SelectedItem as product;

            if (product != null)
            {
                var ms = new MemoryStream(product.img);
                var returnImage = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                previewProduct.Source = returnImage;
                previewProduct.Visibility = Visibility.Visible;
            }
        }
    }
}
