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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public User()
        {
            InitializeComponent();
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            updateBtn.IsEnabled = false;
            deleteBtn.IsEnabled = false;

            saveBtn.IsEnabled = false;

            userIDBox.IsEnabled = false;
            userNameBox.IsEnabled = false;
            passwordBox.IsEnabled = false;
            roleBox.IsEnabled = false;
        }

        void LoadData()
        {
            var user = from x in dc.users select x;
            DataGrid.ItemsSource = user;
        }

        void LoadWindow()
        {
            User window = new User();
            window.Show();
            this.Hide();
        }

        private void addUser(object sender, RoutedEventArgs e)
        {
            addBtn.IsEnabled = false;
            saveBtn.IsEnabled = true;

            userIDBox.IsEnabled = true;
            userNameBox.IsEnabled = true;
            passwordBox.IsEnabled = true;
            roleBox.IsEnabled = true;

            DataGrid.IsEnabled = false;
        }

        private void updateUser(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.users where x.UserID == int.Parse(userIDBox.Text) select x).First();
                update.UserName = userNameBox.Text;
                update.Password = passwordBox.Text;
                update.Role = roleBox.Text;
                dc.SubmitChanges();

                MessageBox.Show("user has been updated");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("That is not a valid action");
                LoadWindow();
            }
        }

        private void deleteUser(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = (from x in dc.users where x.UserID == int.Parse(userIDBox.Text) select x).First();
                dc.users.DeleteOnSubmit(delete);
                dc.SubmitChanges();
                LoadData();

                MessageBox.Show("user has been deleted");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Thats not a valid action");
            }
        }

        private void saveUser(object sender, RoutedEventArgs e)
        {
            try
            {
                var userModel = new user
                {
                    UserID = int.Parse(userIDBox.Text),
                    UserName = userNameBox.Text,
                    Password = passwordBox.Text,
                    Role = roleBox.Text
                };

                userIDBox.Clear();
                userNameBox.Clear();
                passwordBox.Clear();

                dc.users.InsertOnSubmit(userModel);
                dc.SubmitChanges();

                MessageBox.Show("user has been added");
                LoadData();
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Please Fill out every field right for the data to be updated");
                LoadWindow();
            }
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userIDBox.IsEnabled = true;
            userNameBox.IsEnabled = true;
            passwordBox.IsEnabled = true;
            roleBox.IsEnabled = true;

            addBtn.IsEnabled = false;
            saveBtn.IsEnabled = false;
            updateBtn.IsEnabled = true;
            deleteBtn.IsEnabled = true;
        }
    }
}
