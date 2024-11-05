using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnspProject.Model
{
    internal class Transaksi
    {
        // Menyimpan ID unik untuk setiap Transaksi.
        public int Id { get; set; }

        // Menyimpan Tanggal transaksi, mencatat kapan transaksi terjadi.
        public DateTime Tanggal { get; set; }

        // Menyimpan nama pelanggan yang terlibat dalam transaksi.
        public string NamaPelanggan { get; set; }

        // Menyimpan jenis barang yang dipilih dalam transaksi.
        public string Jenis { get; set; }

        // Menyimpan ukuran barang dalam transaksi.
        public int Ukuran { get; set; }

        // Menyimpan jumlah barang yang dibeli atau dipesan oleh pelanggan.
        public int Jumlah { get; set; }

        // Menyimpan total harga sebelum pajak yang dihitung berdasarkan harga satuan dan jumlah barang.
        public decimal TotalHarga { get; set; }

        // Menyimpan jumlah pajak yang dihitung untuk transaksi berdasarkan total harga.
        public decimal TotalPajak { get; set; }

        // Menyimpan keterangan tambahan tentang transaksi
        public string Keterangan { get; set; }

    }
}
