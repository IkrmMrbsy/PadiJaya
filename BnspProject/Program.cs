using BnspProject.DataAccess;
using BnspProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BnspProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseCrud db = new DatabaseCrud();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Beli");
                Console.WriteLine("2. Cari Barang");
                Console.WriteLine("3. Sortir Transaksi");
                Console.WriteLine("4. Keluar");
                Console.WriteLine("Pilih Menu:");

                var pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        // Input Nama Pelanggan
                        string namaPelanggan;
                        do
                        {
                            Console.Write("Nama Pelanggan: ");
                            namaPelanggan = Console.ReadLine();

                            // Validasi: Nama tidak boleh kosong, tidak boleh hanya spasi, dan tidak boleh mengandung angka
                            if (string.IsNullOrWhiteSpace(namaPelanggan))
                            {
                                Console.WriteLine("Nama Pelanggan tidak boleh kosong. Silakan coba lagi.");
                            }
                            else if (namaPelanggan.Any(char.IsDigit))
                            {
                                Console.WriteLine("Nama Pelanggan tidak boleh mengandung angka. Silakan coba lagi.");
                            }

                        } while (string.IsNullOrWhiteSpace(namaPelanggan) || namaPelanggan.Any(char.IsDigit));

                        // Input ID Produk
                        int idProduk;
                        do
                        {
                            Console.Write("ID Produk: ");
                            string idInput = Console.ReadLine();

                            // Validasi: ID Produk harus berupa angka positif
                            if (!int.TryParse(idInput, out idProduk) || idProduk <= 0)
                            {
                                Console.WriteLine("ID Produk harus berupa angka positif dan tidak boleh minus. Silakan coba lagi.");
                            }

                        } while (idProduk <= 0);

                        // Input Jumlah
                        int jumlah;
                        do
                        {
                            Console.Write("Jumlah: ");
                            string jumlahInput = Console.ReadLine();

                            // Validasi: Jumlah harus berupa angka positif
                            if (!int.TryParse(jumlahInput, out jumlah) || jumlah <= 0)
                            {
                                Console.WriteLine("Jumlah harus berupa angka positif dan tidak boleh minus. Silakan coba lagi.");
                            }

                        } while (jumlah <= 0);

                        // Mendapatkan produk dan memeriksa stok
                        var produk = db.GetProduk(idProduk);
                        if (produk == null || produk.Stock < jumlah)
                        {
                            Console.WriteLine("Produk tidak ditemukan atau stok tidak mencukupi.");
                        }
                        else
                        {
                            // Menghitung total dan pajak
                            decimal totalHarga = produk.HargaJual * jumlah;
                            decimal pajak = totalHarga * 0.11m;
                            decimal totalPajak = totalHarga + pajak;

                            Console.WriteLine($"Total: {totalHarga}, Pajak: {pajak}, Total Setelah Pajak: {totalPajak}");

                            Console.Write("Keterangan: ");
                            string keterangan = Console.ReadLine();

                            Console.Write("Konfirmasi pembelian? (y/n): ");

                            if (Console.ReadLine().ToLower() == "y")
                            {
                                // Update stok dan catat transaksi
                                db.UpdateStok(idProduk, jumlah);
                                db.InsertTransaksi(new Transaksi
                                {
                                    Tanggal = DateTime.Now,
                                    NamaPelanggan = namaPelanggan,
                                    Jenis = produk.Jenis,
                                    Ukuran = produk.Ukuran,
                                    Jumlah = jumlah,
                                    TotalHarga = totalHarga,
                                    TotalPajak = totalPajak,
                                    Keterangan = keterangan
                                });
                                Console.WriteLine("Transaksi berhasil dicatat.");
                            }
                            else
                            {
                                Console.WriteLine("Transaksi dibatalkan.");
                            }
                        }
                        break;


                    case "2":
                        Console.WriteLine("Masukkan jenis barang yang ingin dicari:");
                        string jenis = Console.ReadLine();

                        List<Barang> hasilCari = db.GetProdukByJenis(jenis);

                        if (hasilCari.Count == 0)
                        {
                            Console.WriteLine("Tidak ada produk dengan jenis tersebut.");
                        }
                        else
                        {
                            Console.WriteLine("Hasil Pencarian Berdasarkan Jenis:");
                            foreach (var item in hasilCari)
                            {
                                Console.WriteLine($"ID: {item.Id}, Jenis: {item.Jenis}, Ukuran: {item.Ukuran}, Harga Jual: Rp {item.HargaJual:N2}, Stok: {item.Stock}");
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Pilih metode sortir:");
                        Console.WriteLine("1. Tanggal Terbaru");
                        Console.WriteLine("2. Tanggal Terlama");

                        var sortChoice = Console.ReadLine();

                        List<Transaksi> sortedTransaksi;
                        if (sortChoice == "1")
                        {
                            sortedTransaksi = db.SortTransaksi(false); // Terbaru (DESC)
                        }
                        else if (sortChoice == "2")
                        {
                            sortedTransaksi = db.SortTransaksi(true); // Terlama (ASC)
                        }
                        else
                        {
                            Console.WriteLine("Pilihan Tidak Valid");
                            continue;
                        }

                        if (sortedTransaksi.Count == 0)
                        {
                            Console.WriteLine("Tidak Ada Transaksi");
                        }
                        else
                        {
                            Console.WriteLine("Daftar Transaksi (Di Sortir):");
                            foreach (var item in sortedTransaksi)
                            {
                                Console.WriteLine($"ID: {item.Id}, Tanggal: {item.Tanggal.ToString("yyyy-MM-dd")}, Nama Pelanggan: {item.NamaPelanggan}, Jenis: {item.Jenis}, Ukuran: {item.Ukuran}, Jumlah: {item.Jumlah}, Total Harga: {item.TotalHarga}, Pajak: {item.TotalPajak}, Keterangan: {item.Keterangan}");
                            }
                        }
                        break;

                    case "4":
                        return;


                    default:
                        Console.WriteLine("Input Tidak Valid");
                        break;
                }
            }
        }
    }
}
