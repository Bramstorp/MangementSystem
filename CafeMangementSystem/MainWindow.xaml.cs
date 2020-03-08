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

namespace CafeMangementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public MainWindow()
        {
            InitializeComponent();
        }
        private void loginBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = dc.users.Where(x => x.UserName == UserNameBox.Text &&
                                          x.Password == PasswordBox.Password).Single();

                if (AdminBox.IsChecked == true && EmployeeBox.IsChecked == true)
                {
                    MessageBox.Show("Please only selected one role!");
                }
                else
                {
                    if (AdminBox.IsChecked == true)
                    {
                        if (user.Role == AdminBox.Content.ToString())
                        {
                            Homepage hp = new Homepage();
                            hp.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("U Dont have access to this!");
                            UserNameBox.Clear();
                            PasswordBox.Clear();
                        }
                    }
                    else if (EmployeeBox.IsChecked == true)
                    {
                        if (user.Role == EmployeeBox.Content.ToString())
                        {
                            Homepage hp = new Homepage();
                            hp.Show();
                            this.Hide();
                        }
                        else if (user.Role == "Admin")
                        {
                            Homepage hp = new Homepage();
                            hp.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("U Dont have access to this!");
                            UserNameBox.Clear();
                            PasswordBox.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("u need to check out ur role");
                    }
                }
            }
            catch
            {
                UserNameBox.Clear();
                PasswordBox.Clear();
                MessageBox.Show("Login failed");
            }

        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
