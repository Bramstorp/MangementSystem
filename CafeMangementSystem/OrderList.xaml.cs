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
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Data;

namespace CafeMangementSystem
{
    /// <summary>
    /// Interaction logic for BillLoader.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public OrderList()
        {
            InitializeComponent();
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            OrderStatus.IsEnabled = false;
        }

        void LoadData()
        {
            var ld = from x in dc.orders select x;
            DataGrid.ItemsSource = ld;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.orders where x.OrderStatus == OrderStatus.Text select x).First();
                update.OrderStatus = test.Text;

                dc.SubmitChanges();
                LoadData();

                MessageBox.Show("Order has been updated");
            }
            catch
            {
                MessageBox.Show("That is not a valid action");
            }
        }

        private void DropDownBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var itemResult = dc.orders.Where(x => x.OrderID.ToString().Contains(SearchBox.Text));

            DataGrid.ItemsSource = itemResult;
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = dc.orders;
        }
    }
}
