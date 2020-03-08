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
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public Supplier()
        {
            InitializeComponent();
        }
        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            SuppIdBox.IsEnabled = false;
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            AddressBox.IsEnabled = false;
            CityBox.IsEnabled = false;
            PinCodeBox.IsEnabled = false;
            PhoneNrBox.IsEnabled = false;
            EmailBox.IsEnabled = false;
            SuppBox.IsEnabled = false;
            DateJoinPicker.IsEnabled = false;

            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;

            SaveBtn.IsEnabled = false;
        }

        void LoadWindow()
        {
            Supplier supp = new Supplier();
            supp.Show();
            this.Hide();
        }

        void LoadData()
        {
            var supp = from x in dc.suppliers select x;
            DataGrid.ItemsSource = supp;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void addBtn(object sender, RoutedEventArgs e)
        {
            AddBtn.IsEnabled = false;
            SaveBtn.IsEnabled = true;

            SuppIdBox.IsEnabled = true;
            FirstNameBox.IsEnabled = true;
            LastNameBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            PinCodeBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            EmailBox.IsEnabled = true;
            SuppBox.IsEnabled = true;
            DateJoinPicker.IsEnabled = true;

            DataGrid.IsEnabled = false;
        }

        private void searchBox(object sender, TextChangedEventArgs e)
        {
            var searchResult = dc.suppliers.Where(x => x.SupplierID.ToString().Contains(SearchBox.Text) ||
                                                  x.FirstName.Contains(SearchBox.Text) ||
                                                  x.LastName.Contains(SearchBox.Text) ||
                                                  x.Address.Contains(SearchBox.Text) ||
                                                  x.City.Contains(SearchBox.Text) ||
                                                  x.PinCode.ToString().Contains(SearchBox.Text) ||
                                                  x.PhoneNumber.ToString().Contains(SearchBox.Text) ||
                                                  x.Email.ToString().Contains(SearchBox.Text) ||
                                                  x.SupplierOf.Contains(SearchBox.Text));

            DataGrid.ItemsSource = searchResult;
        }

        private void deleteBtn(object sender, RoutedEventArgs e)
        {
            var delete = (from x in dc.suppliers where x.SupplierID == int.Parse(SuppIdBox.Text) select x).First();
            dc.suppliers.DeleteOnSubmit(delete);
            dc.SubmitChanges();
            LoadData();

            MessageBox.Show("item has been deleted");
            LoadWindow();
        }

        private void updateBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.suppliers where x.SupplierID == int.Parse(SuppIdBox.Text) select x).First();
                update.SupplierID = int.Parse(SuppIdBox.Text);
                update.FirstName = FirstNameBox.Text;
                update.LastName = LastNameBox.Text;
                update.Address = AddressBox.Text;
                update.City = CityBox.Text;
                update.PinCode = int.Parse(PinCodeBox.Text);
                update.PhoneNumber = int.Parse(PhoneNrBox.Text);
                update.Email = EmailBox.Text;
                update.DateJoin = DateJoinPicker.SelectedDate;
                update.SupplierOf = SuppBox.Text;

                dc.SubmitChanges();

                MessageBox.Show("Supplier has been updated");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("That is not a valid action");
            }
        }

        private void saveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var suppModel = new supplier
                {
                    SupplierID = int.Parse(SuppIdBox.Text),
                    FirstName = FirstNameBox.Text,
                    LastName = LastNameBox.Text,
                    Address = AddressBox.Text,
                    City = CityBox.Text,
                    PinCode = int.Parse(PinCodeBox.Text),
                    PhoneNumber = int.Parse(PhoneNrBox.Text),
                    Email = EmailBox.Text,
                    DateJoin = DateJoinPicker.SelectedDate,
                    SupplierOf = SuppBox.Text,

                };

                SuppIdBox.Clear();
                FirstNameBox.Clear();
                LastNameBox.Clear();
                AddressBox.Clear();
                CityBox.Clear();
                PinCodeBox.Clear();
                PhoneNrBox.Clear();
                EmailBox.Clear();
                SuppBox.Clear();

                dc.suppliers.InsertOnSubmit(suppModel);
                dc.SubmitChanges();

                MessageBox.Show("Supplier has been added");
                LoadData();
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("u need to fill out all fields correct");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SuppIdBox.IsEnabled = true;
            FirstNameBox.IsEnabled = true;
            LastNameBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            PinCodeBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            EmailBox.IsEnabled = true;
            DateJoinPicker.IsEnabled = true;
            SuppBox.IsEnabled = true;

            AddBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
        }
    }
}
