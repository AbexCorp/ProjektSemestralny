using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();

            orderGrid.Items.Clear();
            using (DatabaseContext db = new())
            {
                var query = db.Orders
                    .Include(p => p.Product)
                    .Include(p => p.Product.Category);
                foreach (var item in query)
                {
                    orderGrid.Items.Add(item);
                }
            }
        }
    }
}
