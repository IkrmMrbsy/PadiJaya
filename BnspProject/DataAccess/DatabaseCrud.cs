using BnspProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnspProject.DataAccess
{
    internal class DatabaseCrud
    {
        private readonly Koneksi koneksi = new Koneksi();

        public Barang GetProduk(int id)
        {
            SqlConnection conn = koneksi.BukaKoneksi();

            Barang barang = null;

            //memeriksa apakah koneksi ke database berhasil dibuka
            if (conn != null)
            {
                string query = "SELECT * FROM barang WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Membaca data dari SqlDataReader dan menginisialisasi objek Produk
                    barang = new Barang
                    {
                        Id = reader.GetInt32(0),
                        Jenis = reader.GetString(1),
                        Ukuran = reader.GetInt32(2),
                        HargaJual = reader.GetDecimal(3),
                        Stock = reader.GetInt32(4)
                    };
                }
                // Menutup koneksi setelah selesai menggunakan
                koneksi.TutupKoneksi(conn);
            }
            // Mengembalikan objek produk yang diambil dari database
            return barang;
        }

        public void InsertTransaksi(Transaksi transaksi)
        {
            // Membuka koneksi ke database
            SqlConnection conn = koneksi.BukaKoneksi();
            if (conn != null)
            {
                try
                {
                    // Query SQL untuk menyisipkan data transaksi ke dalam tabel transaksi
                    string query = "INSERT INTO transaksi (Tanggal, Nama_Pelanggan, Jenis, Ukuran, Jumlah, Total_Harga, Total_Pajak, Keterangan) VALUES (@Tanggal, @NamaPelanggan, @Jenis, @Ukuran, @Jumlah, @Total_Harga, @Total_Pajak, @Keterangan)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Menambahkan parameter untuk setiap kolom yang akan diisi
                    cmd.Parameters.AddWithValue("@Tanggal", transaksi.Tanggal);
                    cmd.Parameters.AddWithValue("@NamaPelanggan", transaksi.NamaPelanggan);
                    cmd.Parameters.AddWithValue("@Jenis", transaksi.Jenis);
                    cmd.Parameters.AddWithValue("@Ukuran", transaksi.Ukuran);
                    cmd.Parameters.AddWithValue("@Jumlah", transaksi.Jumlah);
                    cmd.Parameters.AddWithValue("@Total_Harga", transaksi.TotalHarga);
                    cmd.Parameters.AddWithValue("@Total_Pajak", transaksi.TotalPajak);
                    cmd.Parameters.AddWithValue("@Keterangan", transaksi.Keterangan);

                    // Menjalankan perintah insert
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Menangani kesalahan jika transaksi gagal disimpan
                    Console.WriteLine("Gagal Menyimpan Transaksi " + ex.Message);
                }
                finally
                {
                    // Menutup koneksi setelah selesai
                    koneksi.TutupKoneksi(conn);
                }
            }
        }

        //Untuk Update Stok ketika setiap ada pembelian akan berkurang stok nya
        public bool UpdateStok(int id, int jumlah)
        {
            // Membuka koneksi ke database
            SqlConnection conn = koneksi.BukaKoneksi();
            if (conn != null)
            {
                // Query SQL untuk memperbarui stok produk
                string query = "UPDATE barang SET Stock = Stock - @Jumlah WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Jumlah", jumlah);

                // Menjalankan perintah update dan mendapatkan hasilnya
                int result = cmd.ExecuteNonQuery();

                // Menutup koneksi setelah selesai
                koneksi.TutupKoneksi(conn);

                // Mengembalikan true jika update berhasil, false jika gagal
                return result > 0;
            }
            // Mengembalikan false jika koneksi gagal dibuka
            return false;
        }

        //Mencari dan mendapatkan daftar produk berdasarkan jenis yang diberikan.
        public List<Barang> GetProdukByJenis(string jenis)
        {
            List<Barang> barangList = new List<Barang>();
            SqlConnection conn = koneksi.BukaKoneksi();

            if (conn != null)
            {
                try
                {
                    // Query SQL untuk mendapatkan semua data produk yang jenisnya mengandung kata kunci tertentu
                    String query = "SELECT * FROM barang WHERE Jenis LIKE @Jenis";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Jenis", "%" + jenis + "%");

                    // Menambahkan parameter pencarian, dengan wildcard untuk pencarian sebagian
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Membaca data dari setiap kolom hasil query dan mengisi objek Barang
                        Barang produk = new Barang()
                        {
                            Id = reader.GetInt32(0),
                            Jenis = reader.GetString(1),
                            Ukuran = reader.GetInt32(2),
                            HargaJual = reader.GetDecimal(3),
                            Stock = reader.GetInt32(4)
                        };
                        // Menambahkan produk yang ditemukan ke dalam list barang
                        barangList.Add(produk);
                    }
                }
                catch (SqlException ex)
                {
                    // Menangani kesalahan saat pencarian produk
                    Console.WriteLine("Gagal Mencari Produk: " + ex.Message);
                }
                finally
                {
                    // Menutup koneksi database setelah selesai
                    koneksi.TutupKoneksi(conn);
                }
            }
            return barangList;
        }

        public List<Transaksi> SortTransaksi(bool ascending = true)
        {
            List<Transaksi> transaksiList = new List<Transaksi>();
            SqlConnection conn = koneksi.BukaKoneksi();

            if (conn != null)
            {
                try
                {
                    string query = "SELECT * FROM transaksi ORDER BY Tanggal " + (ascending ? "ASC" : "DESC");
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transaksi transaksi = new Transaksi()
                        {
                            Id = reader.GetInt32(0),
                            Tanggal = reader.GetDateTime(1),
                            NamaPelanggan = reader.GetString(2),
                            Jenis = reader.GetString(3),
                            Ukuran = reader.GetInt32(4),
                            Jumlah = reader.GetInt32(5),
                            TotalHarga = reader.GetDecimal(6),
                            TotalPajak = reader.GetDecimal(7),
                            Keterangan = reader.GetString(8)
                        };
                        transaksiList.Add(transaksi);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Gagal Menampilkan Transaksi: " + ex.Message);
                }
                finally
                {
                    koneksi.TutupKoneksi(conn);
                }
            }
            return transaksiList;
        }

    }
}
