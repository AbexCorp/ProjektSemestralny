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
using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
using StoreApp.Model;

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

            using(DatabaseContext db = new())
            {
                IQueryable<Category> categories = db.Categories
                    .OrderBy(p => p.IdCategory);
                foreach (Category category in categories)
                {
                    categoryList.Items.Add(category.Name);
                }
            }
        }



        private void addCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (categoryNameBox.Text == null || categoryNameBox.Text == "")
            {
                MessageBox.Show("Category name can't be empty");
                return;
            }
            using(DatabaseContext db = new())
            {
                Category category = new Category { Name = categoryNameBox.Text };
                db.Categories.Add(category);
                db.SaveChanges();
            }
            categoryList.Items.Add(categoryNameBox.Text);
            categoryNameBox.Clear();
        }

        private void removeCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext db = new())
            {
                if (categoryList.SelectedItem == null)
                    return;

                IQueryable<Category> categoryToDelete = db.Categories
                    .Where(p => p.Name == categoryList.SelectedItem.ToString());
                db.Categories.RemoveRange(categoryToDelete);
                db.SaveChanges();

                categoryList.Items.Remove(categoryList.SelectedItem);
            }
        }
    }
}
