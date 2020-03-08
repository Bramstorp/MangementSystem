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
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public Homepage()
        {
            InitializeComponent();
        }

        private void addDrinkBtn(object sender, RoutedEventArgs e)
        {
            Items drink = new Items();
            drink.Show();
        }

        private void addEmployeeBtn(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();
        }

        private void addProduct(object sender, RoutedEventArgs e)
        {
            ProductForm pf = new ProductForm();
            pf.Show();
        }

        private void readStock(object sender, RoutedEventArgs e)
        {
            Stock stock = new Stock();
            stock.Show();
        }

        private void placeOrder(object sender, RoutedEventArgs e)
        {
            PlaceOrder pl = new PlaceOrder();
            pl.Show();
        }

        private void addSupplier(object sender, RoutedEventArgs e)
        {
            Supplier sup = new Supplier();
            sup.Show();
        }

        private void addUser(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Show();
        }

        private void logoutBtn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void readOrder(object sender, RoutedEventArgs e)
        {
            OrderList ol = new OrderList();
            ol.Show();
        }
    }
}
