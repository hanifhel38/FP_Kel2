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
using System.Windows.Shapes;

namespace restoran
{
    /// <summary>
    /// Interaction logic for PopupDetail.xaml
    /// </summary>
    public partial class PopupDetail : Window
    {
        Database database;
        private DataTable dataTable;
        public PopupDetail(int idTransaksi)
        {
            InitializeComponent();
            database = new Database();
            getTransaksi(idTransaksi);
        }

        private void getTransaksi(int idTransaksi)
        {
            dataTable = new DataTable();
            database.setQuery("SELECT makanan.nama as makanan, harga FROM makanan_pelanggan " +
                "INNER JOIN makanan ON makanan_pelanggan.id_makanan = makanan.id WHERE id_transaksi="+idTransaksi); ;
            int i = database.executeWithData().Fill(dataTable);
            table.DataContext = dataTable.DefaultView;
            table.AutoGenerateColumns = true;
            table.CanUserAddRows = false;
        }
    }
}
