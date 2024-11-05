using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnspProject.DataAccess
{
    internal class Koneksi
    {
        private readonly string connectionString = "Server=.;Database=PadiJaya;Integrated Security=true;TrustServerCertificate=True";

        public SqlConnection BukaKoneksi()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();//Buka koneksi ke database
                return conn;
            }
            catch (SqlException ex)
            {
                //Menampilkan error
                Console.WriteLine("Gagal Membuka Koneksi " + ex.Message);
                return null;//Mengembalikan null kalau koneksi gagal
            }
        }

        public void TutupKoneksi(SqlConnection conn)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Close();//Menutup koneksi database
                }
                catch (SqlException ex)
                {
                    //Menampilkan error
                    Console.WriteLine("Gagal Menutup Koneksi " + ex.Message);
                }
            }
        }
    }
}
