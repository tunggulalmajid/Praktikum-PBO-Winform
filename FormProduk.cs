using Npgsql;
using Praktikum_PBO_Winform.Models;
using System.Data;
using System.Data.Common;

namespace Praktikum_PBO_Winform
{
    public partial class FormProduk : Form
    {

        string connStr = "Host=localhost;Username=postgres;Password=Gunungsari;Database=CRUDPR";
        Produk produk = new Produk();
        private int _userId;
        private List<Produk> listProduk;
        public FormProduk(int userId)
        {

            InitializeComponent();
            _userId = userId;
            listProduk = produk.LoadProduk(userId);

            showDataGrid();
        }
        public void showDataGrid()
        {
            while (true)
            {
                try
                {
                    dataGridView1.DataSource = listProduk;
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memuat produk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
        }



        private void BtnTambahProduk_Click(object sender, EventArgs e)
        {
            string nama = tbNamaProduk.Text;
            int harga, stok;
            bool isHargaValid = int.TryParse(tbHargaProduk.Text, out harga);
            bool isStokValid = int.TryParse(tbStokProduk.Text, out stok);

            if (string.IsNullOrEmpty(nama) || !isHargaValid || !isStokValid || harga <= 0 || stok <= 0)
            {
                MessageBox.Show("Silakan isi semua field dengan benar. Pastikan harga dan stok berupa angka positif.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            produk.TambahProduk(nama, harga, stok, _userId);
            tbNamaProduk.Clear();
            tbHargaProduk.Clear();
            tbStokProduk.Clear();
            showDataGrid();
        }

       

    }

}