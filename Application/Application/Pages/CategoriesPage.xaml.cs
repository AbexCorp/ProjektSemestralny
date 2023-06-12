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

namespace StoreApp.Pages
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent();
        }

        private void addCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (categoryNameBox.Text == null || categoryNameBox.Text == "")
            {
                MessageBox.Show("Category name can't be empty");
                return;
            }
            categoryList.Items.Add(categoryNameBox.Text);
            categoryNameBox.Clear();
        }

        private void removeCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (categoryList.SelectedItem == null)
                return;
            categoryList.Items.Remove(categoryList.SelectedItem);
        }
    }
}
