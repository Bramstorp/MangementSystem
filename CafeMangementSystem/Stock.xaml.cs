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

namespace CafeMangementSystem
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public Stock()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            var stock = from x in dc.products select x;
            DataGrid.ItemsSource = stock;
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            ProductIDBox.IsEnabled = false;
            ProductNameBox.IsEnabled = false;
            ProductBrandBox.IsEnabled = false;
            ProductQtyBox.IsEnabled = false;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SearchBar(object sender, TextChangedEventArgs e)
        {
            var itemResult = dc.products.Where(x => x.ProductName.Contains(search.Text) ||
                                            x.ProductID.ToString().Contains(search.Text) ||
                                            x.Quantity.ToString().Contains(search.Text) ||
                                            x.Brand.Contains(search.Text));


            DataGrid.ItemsSource = itemResult;
        }
    }
}
