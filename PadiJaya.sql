create database PadiJaya;

use PadiJaya;

create table transaksi(
Transaction_Number INT PRIMARY KEY IDENTITY,
Tanggal DATE NOT NULL,
Nama_Pelanggan VARCHAR(100) NOT NULL,
Jenis VARCHAR(50),
Ukuran INT,
Jumlah INT CHECK(Jumlah > 0),
Total_Harga DECIMAL (10, 2),
Total_Pajak decimal (10, 2),
Keterangan VARCHAR(255)
);



create table barang(
Id INT PRIMARY KEY IDENTITY,
Jenis VARCHAR(50),
Ukuran INT,
Harga_Jual DECIMAL (10, 2),
Stock INT
);

insert into barang(Jenis, Ukuran, Harga_Jual, Stock) VALUES
('Topi Koki Setra Ramos', 5, 73000, 124),
('Rojolele Super', 5, 63000, 76),
('Rojolele Super', 10, 120000, 53),
('Rojolele Super', 25, 300000, 37),
('BMW Setra Ramos', 5, 68000, 89),
('Bunga Ramos Setra', 5, 64000, 147),
('Bunga Ramos Setra', 10, 120000, 32),
('Maknyus Premium', 5, 71000, 154),
('Maknyus Premium', 25, 338000, 99),
('Puregreen Beras Merah', 1, 22000, 64),
('Puregreen Beras Merah', 2, 45000, 39);