using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Praktikum_PBO_Winform.Models
{
    internal class Produk
    {
        public int produkId { get; set; }
        public string namaProduk { get; set; }
        public int hargaProduk { get; set; }
        public int stok { get; set; }
        string connStr;
        private List<Produk> listProduk;
        public Produk()
        {
           
            connStr = "Host=localhost;Username=postgres;Password=Gunungsari;Database=CRUDPR";
        }
        public void TambahProduk(string nama, int harga, int stok, int _userId)
        {
            string query = "INSERT INTO products (nama_produk, harga_produk, stok, user_id) VALUES (@nama, @harga, @stok, @userId)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@harga", harga);
                        cmd.Parameters.AddWithValue("@stok", stok);
                        cmd.Parameters.AddWithValue("@userId", _userId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Produk berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menambahkan produk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan tidak terduga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public List<Produk> LoadProduk(int _userId)
        {
            string query = "SELECT product_id, nama_produk, harga_produk, stok FROM products WHERE user_id = @userId";
            listProduk = new List<Produk>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", _userId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                Produk produk = new Produk
                                {
                                    produkId = reader.GetInt32(0),
                                    namaProduk = reader.GetString(1),
                                    hargaProduk = reader.GetInt32(2),
                                    stok = reader.GetInt32(3)
                                };
                                listProduk.Add(produk);
                            }
                        }
                    }
                  
                }
                catch (NpgsqlException)
                {

                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return listProduk;
        }
    }
}
