using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    /// 

    public partial class Menu : Window
    {
        private UserControl currentPage;

        public Menu()
        {
            InitializeComponent();
            setCurrentMenu(new PageMenu());
        }

        private void setCurrentMenu(UserControl page)
        {
            if(currentPage != null)
            {
                mainFrame.Children.Clear();
            }

            currentPage = page;
            mainFrame.Children.Add(currentPage);

        }

        private void menu(object sender, MouseButtonEventArgs e)
        {

            setCurrentMenu(new PageMenu());
        }

        private void pesanan(object sender, MouseButtonEventArgs e)
        {

            setCurrentMenu(new PagePesanan());

        }
        private void antrian(object sender, MouseButtonEventArgs e)
        {

            setCurrentMenu(new PageAntrian());

        }

        private void transaksi(object sender, MouseButtonEventArgs e)
        {

            setCurrentMenu(new PageTransaksi());
        }
    }
}
