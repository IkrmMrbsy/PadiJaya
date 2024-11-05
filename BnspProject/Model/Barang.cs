using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnspProject.Model
{
    internal class Barang
    {
        // Menyimpan ID unik untuk setiap produk.
        public int Id { get; set; }

        // Menyimpan jenis atau kategori produk.
        public string Jenis { get; set; }

        // Menyimpan ukuran produk
        public int Ukuran { get; set; }

        // Menyimpan harga jual dari produk dalam bentuk decimal.
        public decimal HargaJual { get; set; }

        // Menyimpan jumlah stok produk yang tersedia.
        public int Stock { get; set; }
    }
}
