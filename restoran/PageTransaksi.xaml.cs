using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace restoran
{
    /// <summary>
    /// Interaction logic for PageTransaksi.xaml
    /// </summary>
    public partial class PageTransaksi : UserControl
    {

        Database database;
        private DataTable dataTable;
        private string searchNamaPelanggan = "";
        public PageTransaksi()
        {
            InitializeComponent();
            database = new Database();
            getTransaksi();
        }

        private void getTransaksi()
        {
            dataTable = new DataTable();
            database.setQuery("SELECT transaksi.id as ID, pelanggan.nama as Nama,total as Total,metode_pembayaran, IIF(status=1,'Sedang Diproses','Selesai') as  Status FROM transaksi " +
                " INNER JOIN pelanggan ON transaksi.id_pelanggan = pelanggan.id WHERE pelanggan.nama LIKE '%"+searchNamaPelanggan+"%' ");
            int i = database.executeWithData().Fill(dataTable);
            table.DataContext = dataTable.DefaultView;  
            table.AutoGenerateColumns = true;
            table.CanUserAddRows = false;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

            if (table.SelectedIndex < 0)
            {
                MessageBox.Show("Anda belom memilih antrian", "Peringatan");
                return;
            }
            DataRowView rowview = table.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            new PopupDetail(int.Parse(id)).Show();
        }

        private void inputNamaPelanggan_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchNamaPelanggan = inputNamaPelanggan.Text;
            getTransaksi();
        }
    }
}
