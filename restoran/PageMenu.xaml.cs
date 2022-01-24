using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : UserControl
    {
        private Database db;
        public ImageSource PictureID { get; set; }
        public PageMenu()
        {
            InitializeComponent();
            db = new Database();
            loadMakanan();
        }


        public void loadMakanan()
        {
            DataTable dataTable = new DataTable();
            db.setQuery("SELECT * FROM makanan WHERE status=1");
            int i = db.executeWithData().Fill(dataTable);
            if (i > 0)
            {
                if (dataTable.Rows.Count > 0)
                {
                    image1.Source = getImageFromDb((byte[])dataTable.Rows[0]["image"]);
                    name1.Text = (dataTable.Rows[0]["nama"]).ToString();
                    harga1.Text = "Rp. " + (dataTable.Rows[0]["harga"]).ToString();
                }
                if (dataTable.Rows.Count > 1)
                {
                    image2.Source = getImageFromDb((byte[])dataTable.Rows[1]["image"]);
                    name2.Text = (dataTable.Rows[1]["nama"]).ToString();
                    harga2.Text = "Rp. " + (dataTable.Rows[1]["harga"]).ToString();
                }
                if (dataTable.Rows.Count>2)
                {
                    image3.Source = getImageFromDb((byte[])dataTable.Rows[2]["image"]);
                    name3.Text = (dataTable.Rows[2]["nama"]).ToString();
                    harga3.Text = "Rp. " + (dataTable.Rows[2]["harga"]).ToString();
                }
                if (dataTable.Rows.Count>3)
                {
                    image4.Source = getImageFromDb((byte[])dataTable.Rows[3]["image"]); 
                    name4.Text = (dataTable.Rows[3]["nama"]).ToString();
                    harga4.Text = "Rp. " + (dataTable.Rows[3]["harga"]).ToString();
                }
                if (dataTable.Rows.Count>4)
                {
                    image5.Source = getImageFromDb((byte[])dataTable.Rows[4]["image"]);
                    name5.Text = (dataTable.Rows[4]["nama"]).ToString();
                    harga5.Text = "Rp. " + (dataTable.Rows[4]["harga"]).ToString();
                }
                if (dataTable.Rows.Count>5)
                {
                    image6.Source = getImageFromDb((byte[])dataTable.Rows[5]["image"]);
                    name6.Text = (dataTable.Rows[5]["nama"]).ToString();
                    harga6.Text = "Rp." + (dataTable.Rows[5]["harga"]).ToString();
                }
                if (dataTable.Rows.Count > 6)
                {
                    image7.Source = getImageFromDb((byte[])dataTable.Rows[6]["image"]);
                    name7.Text = (dataTable.Rows[6]["nama"]).ToString();
                    harga7.Text = "Rp. " + (dataTable.Rows[6]["harga"]).ToString();
                }
                if (dataTable.Rows.Count > 7)
                {
                    image8.Source = getImageFromDb((byte[])dataTable.Rows[7]["image"]);
                    name8.Text = (dataTable.Rows[7]["nama"]).ToString();
                    harga8.Text = "Rp. " + (dataTable.Rows[7]["harga"]).ToString();
                }
                if (dataTable.Rows.Count >8)
                {
                    image9.Source = getImageFromDb((byte[])dataTable.Rows[8]["image"]);
                    name9.Text = (dataTable.Rows[8]["nama"]).ToString();
                    harga9.Text = "Rp. " + (dataTable.Rows[8]["harga"]).ToString();
                }
            }
        }

        public BitmapImage getImageFromDb(byte[] dataImage)
        {
            byte[] pictureIdByte = dataImage;
            MemoryStream ms = new MemoryStream(pictureIdByte);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();
            return image;
        }

    }
}
