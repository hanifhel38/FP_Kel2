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
    /// Interaction logic for PageAntrian.xaml
    /// </summary>
    public partial class PageAntrian : UserControl
    {
        Database database;
        private DataTable dataTable;
        public PageAntrian()
        {
            InitializeComponent();
            database = new Database();
            getAntrian();
        }

        private void getAntrian()
        {
            dataTable = new DataTable();
            database.setQuery("SELECT transaksi.id as ID, pelanggan.nama as Nama,total as Total,metode_pembayaran FROM transaksi " +
                "INNER JOIN pelanggan ON transaksi.id_pelanggan = pelanggan.id WHERE transaksi.status = 1");
            int i = database.executeWithData().Fill(dataTable);
            table.DataContext = dataTable.DefaultView;
            table.AutoGenerateColumns = true;
            table.CanUserAddRows = false;
        }

        private void btnAkhiriPesanan_click(object sender, RoutedEventArgs e)
        {
            if(table.SelectedIndex < 0)
            {
                MessageBox.Show("Anda belom memilih antrian","Peringatan");
                return;
            }
            DataRowView rowview = table.SelectedItem as DataRowView;
            string id = rowview.Row[0].ToString();
            database.setQuery("UPDATE transaksi SET status = 0 WHERE id =" + id);
            if (database.execute())
            {
                MessageBox.Show("Pesanan selesai diproses", "Berhasil");
            }
            getAntrian();
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
    }
}
