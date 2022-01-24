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
    /// Interaction logic for PagePesanan.xaml
    /// </summary>
    public partial class PagePesanan : UserControl
    {
        private Database db;
        DataTable dataTable;
        private DataTable dataPesananMakanan;
        private int total = 0;
        public PagePesanan()
        {
            InitializeComponent();
            db = new Database();
            dataPesananMakanan = new DataTable();
            clearDataPesananMakanan();
            dataPesananMakanan.Columns.Add("id");
            dataPesananMakanan.Columns.Add("harga");
            loadMakanan();
        }
        public void clearDataPesananMakanan()
        {
            dataPesananMakanan.Clear();
            total = 0;
            total_bayar.Text = "Rp. " + total;
            inputNamaPelanggan.Text = "";
            inputNoTelepon.Text = "";
            inputHarga.Text = "";
            inputMakanan.SelectedIndex = -1;
        }
        public void loadMakanan()
        {
            dataTable = new DataTable();
            db.setQuery("SELECT id, nama FROM makanan WHERE status=1");
            int i = db.executeWithData().Fill(dataTable);
            if (i > 0)
            {
                inputMakanan.DisplayMemberPath = "nama";
                inputMakanan.SelectedValuePath = "id";
                inputMakanan.ItemsSource = dataTable.DefaultView;
            }
        }
        public int getHargaFromID(int id)
        {
            dataTable = new DataTable();
            db.setQuery("SELECT harga FROM makanan WHERE id="+id);
            int i = db.executeWithData().Fill(dataTable);
            if(i > 0)
            {
                inputHarga.Text = dataTable.Rows[0]["harga"].ToString();
                return int.Parse(dataTable.Rows[0]["harga"].ToString()); 
            }
            return 0;
        }
        private void inputMakanan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inputMakanan.SelectedIndex < 0) return;
            getHargaFromID(int.Parse(inputMakanan.SelectedValue.ToString()));
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            if (inputMakanan.SelectedIndex < 0) return;
            DataRow row = dataPesananMakanan.NewRow();
            int harga = getHargaFromID(int.Parse(inputMakanan.SelectedValue.ToString()));
            row["id"] = int.Parse(inputMakanan.SelectedValue.ToString());
            row["harga"] = harga;
            dataPesananMakanan.Rows.Add(row);
            total += harga;
            total_bayar.Text = "Rp. " + total;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearDataPesananMakanan();
        }

        private void btnBayar_Click(object sender, RoutedEventArgs e)
        {
            if (dataPesananMakanan.Rows.Count < 1)
            {
                MessageBox.Show("Anda belom memilih makanan", "Pesanan  gagal");
                return; 
            }
            if(inputNamaPelanggan.Text=="" || inputNoTelepon.Text == "")
            {
                MessageBox.Show("Form tidak boleh kosong", "Pesanan  gagal");
                return;
            }
            if(metodeCash.IsChecked == false && metodeDebit.IsChecked == false)
            {
                MessageBox.Show("Anda belom memilih metode pembayaran", "Pesanan  gagal");
                return;
            }

            db.setQuery("INSERT INTO pelanggan(nama,no_telp) VALUES('" + inputNamaPelanggan.Text + "','" + inputNoTelepon.Text + "')");
            if (db.execute())
            {
                string metodePembayaran;
                if (metodeDebit.IsChecked == true)
                {
                    metodePembayaran = "Debit";
                }
                else
                {
                    metodePembayaran = "Cash";
                }
                db.setQuery("INSERT INTO transaksi(metode_pembayaran,total,status,id_pelanggan) VALUES('" + metodePembayaran + "','" + total + "','1',SCOPE_IDENTITY())");
                if (db.execute())
                {
                    db.setQuery("SELECT SCOPE_IDENTITY() as id");
                    dataTable = new DataTable();
                    db.executeWithData().Fill(dataTable);
                    int idTransaksi =int.Parse(dataTable.Rows[0]["id"].ToString());
                    foreach (DataRow row in dataPesananMakanan.Rows)
                    {
                        db.setQuery("INSERT INTO makanan_pelanggan(id_transaksi,id_makanan) VALUES('"+idTransaksi+"','" + row["id"].ToString() + "')");
                        db.execute();   
                    }
                    MessageBox.Show("Pesanan akan diproses", "Pesanan berhasil");
                    clearDataPesananMakanan();
                }
            }
        }
    }
}
