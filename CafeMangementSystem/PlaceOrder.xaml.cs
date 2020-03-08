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
using System.Data;
using Microsoft.Win32;
using System.IO;

namespace CafeMangementSystem
{
    /// <summary>
    /// Interaction logic for PlaceOrder.xaml
    /// </summary>
    public partial class PlaceOrder : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public PlaceOrder()
        {
            InitializeComponent();
        }

        void LoadWindow()
        {
            PlaceOrder window = new PlaceOrder();
            window.Show();
            this.Hide();
        }

        void LoadData()
        {
            var ld = from x in dc.orders select x;
            DataGrid.ItemsSource = ld;
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            List<item> itemData = dc.items.ToList();
            List<user> userData = dc.users.ToList();
           
            ItemDropDown.ItemsSource = itemData;
            ItemDropDown.DisplayMemberPath = "ItemName";

            UserDropDown.ItemsSource = userData;
            UserDropDown.DisplayMemberPath = "UserName";

            LoadData();


            DeleteBtn.IsEnabled = false;

            saveBtn.IsEnabled = false;

            OrderIDBox.IsEnabled = false;
            UserDropDown.IsEnabled = false;
            ItemDropDown.IsEnabled = false;
            PriceBox.IsEnabled = false;
            QtyBox.IsEnabled = false;
            AmountBox.IsEnabled = false;

            ImagePath.IsEnabled = false;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private byte[] _imageBytes = null;
        private void ItemDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyImage.Source = new BitmapImage(new Uri(ImagePath.Text));

            using (var fs = new FileStream(ImagePath.Text, FileMode.Open, FileAccess.Read))
            {
                _imageBytes = new byte[fs.Length];
                fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }
        }

        private void ClearBtn(object sender, RoutedEventArgs e)
        {
            PriceBox.Clear();
            QtyBox.Clear();
            AmountBox.Clear();
        }

        private void saveBillBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var billModel = new order
                {
                    OrderedBy = UserDropDown.Text,
                    ItemName = ItemDropDown.Text,
                    Price = int.Parse(PriceBox.Text),
                    Quantity = int.Parse(QtyBox.Text),
                    Amount = int.Parse(AmountBox.Text),
                    OrderID = int.Parse(OrderIDBox.Text),
                    OrderStatus = "In Progress"
                    
                };

                dc.orders.InsertOnSubmit(billModel);
                dc.SubmitChanges();

                MessageBox.Show("Bill has been added");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Thats not a valid action");
            }
        }

        private void addBillBtn(object sender, RoutedEventArgs e)
        {
            addBtn.IsEnabled = false;
            saveBtn.IsEnabled = true;

            UserDropDown.IsEnabled = true;
            ItemDropDown.IsEnabled = true;
            PriceBox.IsEnabled = true;
            QtyBox.IsEnabled = true;
            AmountBox.IsEnabled = true;
            OrderIDBox.IsEnabled = true;

            DataGrid.IsEnabled = false;
        }

        private void DeleteBillBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = (from x in dc.orders where x.OrderID == int.Parse(OrderIDBox.Text) select x).First();
                dc.orders.DeleteOnSubmit(delete);
                dc.SubmitChanges();
                LoadData();

                MessageBox.Show("bill has been deleted");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Thats not a valid action");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDropDown.IsEnabled = false;

            addBtn.IsEnabled = false;
            saveBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = true;
        }

        private void QtyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                AmountBox.Text = (int.Parse(PriceBox.Text) * int.Parse(QtyBox.Text)).ToString();
            }
            catch
            {

            }
        }
    }
}
