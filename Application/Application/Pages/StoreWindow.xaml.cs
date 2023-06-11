using Apps;
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
using System.Windows.Shapes;

namespace StoreApp.Pages
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {
        public StoreWindow()
        {
            InitializeComponent();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Source = new System.Uri("CategoriesPage.xaml", UriKind.Relative);
        }

        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage.Source = new System.Uri("ProductsPage.xaml", UriKind.Relative);
        }
    }
}
