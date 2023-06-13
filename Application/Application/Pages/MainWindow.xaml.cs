using StoreApp.Pages;
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

using StoreApp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Apps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StoreWindow window = new StoreWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            this.Close();
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }





        private async void tempbutton_Click(object sender, RoutedEventArgs e)
        {
            using(DatabaseContext db = new())
            {
                bool deleted = await db.Database.EnsureDeletedAsync();
                bool created = await db.Database.EnsureCreatedAsync();
                MessageBox.Show
                (
                    $"Was deleted: {deleted}" + Environment.NewLine +
                    $"Was created: {created}" + Environment.NewLine +
                    db.Database.GenerateCreateScript()
                );
            }
        }

        private async void resetDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            using(DatabaseContext db = new())
            {
                bool deleted = await db.Database.EnsureDeletedAsync();
                bool created = await db.Database.EnsureCreatedAsync();
                MessageBox.Show
                (
                    $"Database was deleted: {deleted}" + Environment.NewLine +
                    $"Database was Created: {created}"
                );
            }
        }
    }
}
