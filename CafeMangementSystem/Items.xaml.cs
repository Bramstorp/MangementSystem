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
    /// Interaction logic for Drink.xaml
    /// </summary>
    public partial class Items : Window
    {
        ManagementSystemDBDataContext dc = new ManagementSystemDBDataContext(Properties.Settings.Default.CoffeeManagementSystemConnectionString);
        public Items()
        {
            InitializeComponent();
        }

        void LoadWindow()
        {
            Items window = new Items();
            window.Show();
            this.Hide();
        }

        void LoadData()
        {
            var ld = from x in dc.items select x;
            DataGrid.ItemsSource = ld;
        }

        private void Window_load(object sender, RoutedEventArgs e)
        {
            LoadData();

            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;

            saveUserBtn.IsEnabled = false;
            uploadBtn.IsEnabled = false;

            ItemPriceBox.IsEnabled = false;
            ItemNameBox.IsEnabled = false;
            ItemIdBox.IsEnabled = false;

            ImagePath.IsEnabled = false;
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SearchBar(object sender, TextChangedEventArgs e)
        {
            var itemResult = dc.items.Where(x => x.ItemName.Contains(search.Text) ||
                                            x.ItemID.ToString().Contains(search.Text) ||
                                            x.Price.ToString().Contains(search.Text));

            DataGrid.ItemsSource = itemResult;
        }
        private void addBtn(object sender, RoutedEventArgs e)
        {
            addUserBtn.IsEnabled = false;
            saveUserBtn.IsEnabled = true;
            uploadBtn.IsEnabled = true;


            ItemPriceBox.IsEnabled = true;
            ItemNameBox.IsEnabled = true;
            ItemIdBox.IsEnabled = true;

            DataGrid.IsEnabled = false;
        }
        private void updateBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = (from x in dc.items where x.ItemID == int.Parse(ItemIdBox.Text) select x).First();
                update.ItemName = ItemNameBox.Text;
                update.Price = int.Parse(ItemPriceBox.Text);
                update.ItemID = int.Parse(ItemIdBox.Text);
                update.ImageName = ImagePath.Text;
                update.ImageContent = _imageBytes;

                dc.SubmitChanges();

                MessageBox.Show("Item has been updated");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("That is not a valid action");
            }
        }

        private void dealeteBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = (from x in dc.items where x.ItemID == int.Parse(ItemIdBox.Text) select x).First();
                dc.items.DeleteOnSubmit(delete);
                dc.SubmitChanges();
                LoadData();

                MessageBox.Show("item has been deleted");
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Thats not a valid action");
            }

        }

        private void saveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var itemModel = new item
                {
                    ItemName = ItemNameBox.Text,
                    Price = int.Parse(ItemPriceBox.Text),
                    ItemID = int.Parse(ItemIdBox.Text),
                    ImageContent = _imageBytes,
                    ImageName = ImagePath.Text

                };

                dc.items.InsertOnSubmit(itemModel);
                dc.SubmitChanges();

                MessageBox.Show("Item has been added");
                LoadData();
                LoadWindow();
            }
            catch
            {
                MessageBox.Show("Please Fill out every field right for the data to be updated");
                LoadWindow();
            }
        }

        private byte[] _imageBytes = null;
        private void uploadImgBtn(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png)|*.jpg;*.png|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return; }

            ImagePath.Text = dialog.FileName;
            MyImage.Source = new BitmapImage(new Uri(ImagePath.Text));

            using (var fs = new FileStream(ImagePath.Text, FileMode.Open, FileAccess.Read))
            {
                _imageBytes = new byte[fs.Length];
                fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemPriceBox.IsEnabled = true;
            ItemNameBox.IsEnabled = true;
            ItemIdBox.IsEnabled = false;

            addUserBtn.IsEnabled = false;
            saveUserBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;

            uploadBtn.IsEnabled = true;

            MyImage.Source = new BitmapImage(new Uri(ImagePath.Text));

            using (var fs = new FileStream(ImagePath.Text, FileMode.Open, FileAccess.Read))
            {
                _imageBytes = new byte[fs.Length];
                fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }
        }
    }
}
