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
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public ProductForm()
        {
            InitializeComponent();
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            updateProductBtn.IsEnabled = false;
            deleteProductBtn.IsEnabled = false;

            saveProductBtn.IsEnabled = false;

            productIdBox.IsEnabled = false;
            productNameBox.IsEnabled = false;
            productBrandBox.IsEnabled = false;
            productPriceBox.IsEnabled = false;
            productQuantityBox.IsEnabled = false;
        }

        void LoadWindow()
        {
            ProductForm window = new ProductForm();
            window.Show();
            this.Hide();
        }

        void LoadData()
        {

            var product = from x in dc.products select x;
            DataGrid.ItemsSource = product;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void addBtn(object sender, RoutedEventArgs e)
        {
            addProductBtn.IsEnabled = false;
            saveProductBtn.IsEnabled = true;

            productIdBox.IsEnabled = true;
            productNameBox.IsEnabled = true;
            productBrandBox.IsEnabled = true;
            productPriceBox.IsEnabled = true;
            productQuantityBox.IsEnabled = true;

            DataGrid.IsEnabled = false;
        }

        private void updateBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.products where x.ProductID == int.Parse(productIdBox.Text) select x).First();
                update.ProductID = int.Parse(productIdBox.Text);
                update.ProductName = productNameBox.Text;
                update.Brand = productBrandBox.Text;
                update.TotalPrice = int.Parse(productPriceBox.Text);
                update.Quantity = int.Parse(productQuantityBox.Text);

                dc.SubmitChanges();

                MessageBox.Show("Product has been updated");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Please fill out all the fields for the product to be updated");
                LoadWindow();
            }
        }

        private void SearchTextBox(object sender, TextChangedEventArgs e)
        {
            try
            {
                var searchResult = dc.products.Where(x => x.ProductID.ToString().Contains(search.Text) ||
                                          x.ProductName.Contains(search.Text) ||
                                          x.Brand.Contains(search.Text) ||
                                          x.TotalPrice.ToString().Contains(search.Text) ||
                                          x.Quantity.ToString().Contains(search.Text));
                DataGrid.ItemsSource = searchResult;
            }
            catch
            {
                MessageBox.Show("That is not a valid action");

            }
        }

        private void DeleteProductBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = (from x in dc.products where x.ProductID == int.Parse(productIdBox.Text) select x).First();
                dc.products.DeleteOnSubmit(delete);
                dc.SubmitChanges();
                LoadData();

                MessageBox.Show("Product has been deleted");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("That is not a valid action");
                LoadWindow();
            }
        }

        private void SaveProductBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var productModel = new product
                {
                    ProductID = int.Parse(productIdBox.Text),
                    ProductName = productNameBox.Text,
                    Brand = productBrandBox.Text,
                    TotalPrice = int.Parse(productPriceBox.Text),
                    Quantity = int.Parse(productQuantityBox.Text)
                };

                productIdBox.Clear();
                productNameBox.Clear();
                productBrandBox.Clear();
                productPriceBox.Clear();
                productQuantityBox.Clear();

                dc.products.InsertOnSubmit(productModel);
                dc.SubmitChanges();

                MessageBox.Show("The Product have been saved");
                LoadData();
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("u need to fill out all field correct");
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productIdBox.IsEnabled = true;
            productNameBox.IsEnabled = true;
            productBrandBox.IsEnabled = true;
            productPriceBox.IsEnabled = true;
            productQuantityBox.IsEnabled = true;

            addProductBtn.IsEnabled = false;
            saveProductBtn.IsEnabled = false;
            updateProductBtn.IsEnabled = true;
            deleteProductBtn.IsEnabled = true;
        }
    }
}
