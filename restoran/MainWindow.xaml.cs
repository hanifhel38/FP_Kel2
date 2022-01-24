using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace restoran
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database database;
        public MainWindow()
        {
            InitializeComponent();

            database = new Database();
        }

        private void username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                username.Text = "Username";
            }

            username.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA3A2A5"));
        }

        private void username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Username")
            {
                username.Text = "";
            }
            username.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Password")
            {
                password.Text = "";
            }
            password.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));

        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                password.Text = "Password";
            }
            password.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA3A2A5"));

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "Username")
                username.Text = "";
            if (password.Text == "Password")
                password.Text = "";

            DataTable datatable = new DataTable();
            database.setQuery("SELECT * FROM [user] WHERE username = '" + username.Text + "' AND password = CONVERT(VARCHAR(32), HashBytes('MD5', '" + password.Text + "'), 2)");
            int i = database.executeWithData().Fill(datatable);
            if (i > 0)
                new Menu().Show();
            else
            {
                MessageBox.Show("Username/Password salah!!", "Login Gagal");
                if (username.Text == "")
                    username.Text = "Username";
                if (password.Text == "")
                    password.Text = "Password";
            }

        }
    }
}
