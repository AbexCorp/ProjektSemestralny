using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
using StoreApp.Model;
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
    /// Interaction logic for WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        public WarehousePage()
        {
            InitializeComponent();
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            warehouseGrid.Items.Clear();
            using (DatabaseContext db = new())
            {
                var query = db.Warehouse
                    .Include(p => p.Product)
                    .Include(p => p.Product.Category);
                foreach (var item in query)
                {
                    warehouseGrid.Items.Add(item);
                }
            }
        }

        private void sellThisButton_Click(object sender, RoutedEventArgs e)
        {
            if(warehouseGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }

            SingularObject stock = warehouseGrid.SelectedItem as SingularObject;

            using (DatabaseContext db = new())
            {
                var stockToErase = db.Warehouse
                    .Include(s => s.Product)
                    .Where(stockToDelete => stockToDelete == stock);
                if(stockToErase.Count() != 1)
                {
                    MessageBox.Show("Something went wrong");
                    return;
                }

                db.RemoveRange(stockToErase);
                db.SaveChanges();
                MessageBox.Show($"Sold {stock.Product.Name}");

                Order newOrder = new Order { Amount = 1, Date = DateTime.Now, ProductId = stock.ProductId };
                db.Orders.Add(newOrder);
                db.SaveChanges();
            }
            UpdateGrid();
        }

        private void removeThisButton_Click(object sender, RoutedEventArgs e)
        {
            if(warehouseGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }

            SingularObject stock = warehouseGrid.SelectedItem as SingularObject;

            using (DatabaseContext db = new())
            {
                var stockToErase = db.Warehouse
                    .Include(s => s.Product)
                    .Where(stockToDelete => stockToDelete == stock);
                if(stockToErase.Count() != 1)
                {
                    MessageBox.Show("Something went wrong");
                    return;
                }

                db.RemoveRange(stockToErase);
                db.SaveChanges();
                MessageBox.Show($"Deleted {stock.Product.Name}");
            }
            UpdateGrid();
        }

        private void addMoreButton_Click(object sender, RoutedEventArgs e)
        {
            if(warehouseGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose a product");
                return;
            }

            SingularObject stock = warehouseGrid.SelectedItem as SingularObject;
            SingularObject newStock = new SingularObject { ProductId = stock.ProductId };

            using (DatabaseContext db = new())
            {
                db.Warehouse.Add(newStock);
                db.SaveChanges();
                MessageBox.Show($"Added {stock.Product.Name}");
            }
            UpdateGrid();
        }
    }
}
