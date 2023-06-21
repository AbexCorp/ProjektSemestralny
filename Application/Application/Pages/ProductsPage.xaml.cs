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
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
using StoreApp.Model;

namespace StoreApp.Pages
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            using(DatabaseContext db = new())
            {
                //List
                IQueryable<Product> products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.SingularObject)
                    .OrderBy(p => p.IdProduct);

                foreach (var product in products)
                {
                    productsGrid.Items.Add(product);
                }

                //Adding and Filters
                IQueryable<Category> categories = db.Categories
                    .OrderBy(p => p.IdCategory);
                foreach(var category in categories)
                {
                    selectCategory.Items.Add(category.Name);
                    filterCategory.Items.Add(category.Name);
                }
            }
        }



        #region <<< Adding new products >>>

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = productNameBox.Text;
            if(name == null || name == "")
            {
                MessageBox.Show("Product name can't be empty");
                return;
            }
            if(selectCategory.Text == null)
            {
                MessageBox.Show("Choose category for the new product");
                return;
            }
            if(selectCategory.SelectedValue == null)
            {
                MessageBox.Show("Choose category for the new product");
                return;
            }
            string chosenCategory = selectCategory.Text;


            using (DatabaseContext db = new())
            {
                IQueryable<Category> categoryToAdd = db.Categories
                    .Where(p => p.Name == chosenCategory);
                Category category = categoryToAdd.FirstOrDefault();

                Product newProduct = new Product { Name = name, CategoryId = category.IdCategory };
                db.Products.Add(newProduct);
                db.SaveChanges();
                productsGrid.Items.Add(newProduct);
            }
            productNameBox.Clear();
            selectCategory.SelectedIndex = -1;
        }

        #endregion


        #region <<< Removing products >>>

        private void removeProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(productsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }
            Product product = productsGrid.SelectedItem as Product;
            using (DatabaseContext db = new())
            {
                db.Products.RemoveRange(product);
                db.SaveChanges();
            }
            productsGrid.Items.Remove(product);
        }

        #endregion


        #region <<< Filtering products >>>

        private void filterProductsButton_Click(object sender, RoutedEventArgs e)
        {
            if(filterCategory.Text == null || filterCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Choose a category");
                return;
            }
            productsGrid.Items.Clear();

            using (DatabaseContext db = new())
            {
                IQueryable<Product> products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.SingularObject)
                    .Where(p => p.Category.Name == filterCategory.Text)
                    .OrderBy(p => p.IdProduct);

                foreach (var product in products)
                {
                    productsGrid.Items.Add(product);
                }
            }
        }

        private void resetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            productsGrid.Items.Clear();
            filterCategory.SelectedIndex = -1;

            using (DatabaseContext db = new())
            {
                IQueryable<Product> products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.SingularObject)
                    .OrderBy(p => p.IdProduct);

                foreach (var product in products)
                {
                    productsGrid.Items.Add(product);
                }
            }
        }

        #endregion


        #region <<< Adding new stock >>>

        private void addStockButton_Click(object sender, RoutedEventArgs e)
        {
            if(productsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }
            if (!int.TryParse(amountBox.Text, out int stoctNumber))
            {
                MessageBox.Show("Please input a correct number");
                return;
            }
            if(stoctNumber > 20 || stoctNumber < 1)
            {
                MessageBox.Show("Please input a number between 1 and 20");
                return;
            }

            Product product = productsGrid.SelectedItem as Product;
            using (DatabaseContext db = new())
            {
                for(int i = 0; i < stoctNumber; i++)
                {
                    SingularObject newObject = new SingularObject { ProductId = product.IdProduct };
                    db.Warehouse.Add(newObject);
                }
                int res = db.SaveChanges();
                MessageBox.Show($"Added {res} {product.Name} to warehouse");

                IQueryable<Product> updatedProduct = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.SingularObject)
                    .Where(p => p == product);
                productsGrid.Items.Insert(productsGrid.Items.IndexOf(product), updatedProduct.FirstOrDefault());
                productsGrid.Items.RemoveAt(productsGrid.Items.IndexOf(product));
            }
            amountBox.Clear();
        }

        #endregion


        #region <<< Selling >>>

        private void sellProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(productsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }
            if (!int.TryParse(amountBox.Text, out int stoctToSellNumber))
            {
                MessageBox.Show("Please input a correct number");
                return;
            }
            if(stoctToSellNumber > 10 || stoctToSellNumber < 1)
            {
                MessageBox.Show("Please input a number between 1 and 10");
                return;
            }

            Product product = productsGrid.SelectedItem as Product;

            if(product.SingularObject.Count < stoctToSellNumber)
            {
                MessageBox.Show("Not enough stock to sell");
                return;
            }

            using (DatabaseContext db = new())
            {
                var findStock = db.Warehouse
                    .Include(s => s.Product)
                    .Where(stockToDelete => stockToDelete.ProductId == product.IdProduct)
                    .Take(stoctToSellNumber);

                if(stoctToSellNumber > findStock.Count())
                {
                    MessageBox.Show("Not enough stock to sell");
                    return;
                }

                db.RemoveRange(findStock);
                int result = db.SaveChanges();
                MessageBox.Show($"Sold {result} of {product.Name}");


                Order newOrder = new Order { Amount = stoctToSellNumber, Date = DateTime.Now, ProductId = product.IdProduct };
                db.Orders.Add(newOrder);
                db.SaveChanges();


                IQueryable<Product> updatedProduct = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.SingularObject)
                    .Where(p => p == product);
                productsGrid.Items.Insert(productsGrid.Items.IndexOf(product), updatedProduct.FirstOrDefault());
                productsGrid.Items.RemoveAt(productsGrid.Items.IndexOf(product));
            }
        }

        #endregion
    }
}