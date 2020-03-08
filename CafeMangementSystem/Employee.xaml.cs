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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public Employee()
        {
            InitializeComponent();
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            EmpIdBox.IsEnabled = false;
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            AddressBox.IsEnabled = false;
            CityBox.IsEnabled = false;
            PinCodeBox.IsEnabled = false;
            PhoneNrBox.IsEnabled = false;
            PhoneNrBox.IsEnabled = false;
            EmailBox.IsEnabled = false;
            DateJoinPicker.IsEnabled = false;

            SaveBtn.IsEnabled = false;

            DeleteBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
        }
        void LoadData()
        {
            var emp = from x in dc.employees select x;
            DataGrid.ItemsSource = emp;
            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }
        void LoadWindow()
        {
            Employee window = new Employee();
            window.Show();
            this.Hide();
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void addEmployee(object sender, RoutedEventArgs e)
        {
            AddBtn.IsEnabled = false;
            SaveBtn.IsEnabled = true;

            EmpIdBox.IsEnabled = true;
            FirstNameBox.IsEnabled = true;
            LastNameBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            PinCodeBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            EmailBox.IsEnabled = true;
            DateJoinPicker.IsEnabled = true;

            DataGrid.IsEnabled = false;

        }

        private void searchBox(object sender, TextChangedEventArgs e)
        {
            var searchResult = dc.employees.Where(x => x.EmployeeID.ToString().Contains(SearchBox.Text) ||
                                                  x.FirstName.Contains(SearchBox.Text) ||
                                                  x.LastName.Contains(SearchBox.Text) ||
                                                  x.Address.Contains(SearchBox.Text) ||
                                                  x.City.Contains(SearchBox.Text) ||
                                                  x.PinCode.ToString().Contains(SearchBox.Text) ||
                                                  x.PhoneNumber.ToString().Contains(SearchBox.Text) ||
                                                  x.Email.ToString().Contains(SearchBox.Text));

            DataGrid.ItemsSource = searchResult;
        }

        private void deleteBtn(object sender, RoutedEventArgs e)
        {
            var delete = (from x in dc.employees where x.EmployeeID == int.Parse(EmpIdBox.Text) select x).First();
            dc.employees.DeleteOnSubmit(delete);
            dc.SubmitChanges();
            LoadData();

            MessageBox.Show("item has been deleted");
            LoadWindow();
        }

        private void updateBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.employees where x.EmployeeID == int.Parse(EmpIdBox.Text) select x).First();

                update.EmployeeID = int.Parse(EmpIdBox.Text);
                update.FirstName = FirstNameBox.Text;
                update.LastName = LastNameBox.Text;
                update.Address = AddressBox.Text;
                update.City = CityBox.Text;
                update.PinCode = int.Parse(PinCodeBox.Text);
                update.PhoneNumber = int.Parse(PhoneNrBox.Text);
                update.Email = EmailBox.Text;
                update.JoinDate = DateTime.Parse(DateJoinPicker.Text);

                dc.SubmitChanges();
                MessageBox.Show("Employee has been updated");
                LoadWindow();

            }
            catch
            {
                MessageBox.Show("Please Fill out every field right for the data to be updated");
            }
        }

        private void saveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var empModel = new employee
                {
                    EmployeeID = int.Parse(EmpIdBox.Text),
                    FirstName = FirstNameBox.Text,
                    LastName = LastNameBox.Text,
                    Address = AddressBox.Text,
                    City = CityBox.Text,
                    PinCode = int.Parse(PinCodeBox.Text),
                    PhoneNumber = int.Parse(PhoneNrBox.Text),
                    Email = EmailBox.Text,
                    JoinDate = DateJoinPicker.SelectedDate
                };

                EmpIdBox.Clear();
                FirstNameBox.Clear();
                LastNameBox.Clear();
                AddressBox.Clear();
                CityBox.Clear();
                PinCodeBox.Clear();
                PhoneNrBox.Clear();
                EmailBox.Clear();

                dc.employees.InsertOnSubmit(empModel);
                dc.SubmitChanges();

                MessageBox.Show("Employee has been added");
                LoadData();
                LoadWindow();

            }
            catch
            {
                MessageBox.Show("Thats not a valid action");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmpIdBox.IsEnabled = true;
            FirstNameBox.IsEnabled = true;
            LastNameBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            PinCodeBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            PhoneNrBox.IsEnabled = true;
            EmailBox.IsEnabled = true;
            DateJoinPicker.IsEnabled = true;


            AddBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;

        }
    }
}
