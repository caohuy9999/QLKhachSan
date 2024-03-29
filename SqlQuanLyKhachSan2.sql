-- tạo DATABASE
CREATE DATABASE SQLQLKhachSan2
GO


USE [SQLQLKhachSan2]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaChiTietHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[MaHoaDon] [int] NULL,
	[MaDichVu] [int] NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](10, 2) NOT NULL,
	[ThanhTien] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChiTietHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietLuuTru]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietLuuTru](
	[MaLuuTru] [int] IDENTITY(1,1) NOT NULL,
	[MaDatPhong] [int] NULL,
	[SoLuongNguoi] [int] NOT NULL,
	[ThoiGianVaoPhong] [datetime] NOT NULL,
	[ThoiGianDuKienTraPhong] [datetime] NOT NULL,
	[ThoiGianThucTeTraPhong] [datetime] NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLuuTru] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatPhong]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatPhong](
	[MaDatPhong] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NULL,
	[MaPhong] [int] NULL,
	[NgayNhanPhong] [datetime] NOT NULL,
	[NgayTraPhong] [datetime] NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDatPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DichVu]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVu](
	[MaDichVu] [int] IDENTITY(1,1) NOT NULL,
	[TenDichVu] [nvarchar](255) NOT NULL,
	[Gia] [decimal](10, 2) NOT NULL,
	[MaLoaiDichVu] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDichVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DichVuDuocSuDung]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVuDuocSuDung](
	[MaSuDung] [int] IDENTITY(1,1) NOT NULL,
	[MaDichVu] [int] NULL,
	[MaDatPhong] [int] NULL,
	[NgaySuDung] [datetime] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[MaChiTietHoaDon] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSuDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[MaDatPhong] [int] NULL,
	[MaKhachHang] [int] NULL,
	[MaNhanVien] [int] NULL,
	[NgayLapHoaDon] [datetime] NOT NULL,
	[TongTienPhong] [decimal](10, 2) NOT NULL,
	[TongTienDichVu] [decimal](10, 2) NOT NULL,
	[TongTien] [decimal](10, 2) NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
	[DaThanhToan] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[SoDienThoai] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](500) NULL,
	[LoaiKhachHang] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDichVu]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDichVu](
	[MaLoaiDichVu] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiDichVu] [nvarchar](255) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiDichVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhong](
	[MaLoaiPhong] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiPhong] [nvarchar](255) NOT NULL,
	[GiaTheoDem] [decimal](10, 2) NOT NULL,
	[SucChua] [int] NOT NULL,
	[MieuTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](255) NOT NULL,
	[HoTen] [nvarchar](255) NOT NULL,
	[ChucVu] [nvarchar](100) NOT NULL,
	[Luong] [decimal](10, 2) NULL,
	[PathAvatar] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien_Quyen]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien_Quyen](
	[MaNhanVien] [int] NOT NULL,
	[MaQuyen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC,
	[MaQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanHoi]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanHoi](
	[MaPhanHoi] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NULL,
	[DanhGia] [int] NOT NULL,
	[YKien] [nvarchar](max) NULL,
	[NgayPhanHoi] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhanHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[MaPhong] [int] IDENTITY(1,1) NOT NULL,
	[SoPhong] [nvarchar](50) NOT NULL,
	[MaLoaiPhong] [int] NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongTienIch]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongTienIch](
	[MaPhongTienIch] [int] IDENTITY(1,1) NOT NULL,
	[MaPhong] [int] NULL,
	[MaTienIch] [int] NULL,
	[TrangThai] [nvarchar](100) NOT NULL,
	[SoLuong] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhongTienIch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quyen]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quyen](
	[MaQuyen] [int] IDENTITY(1,1) NOT NULL,
	[TenQuyen] [nvarchar](100) NOT NULL,
	[MieuTa] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuKien_UuDai]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuKien_UuDai](
	[MaSuKien] [int] IDENTITY(1,1) NOT NULL,
	[TenSuKien] [nvarchar](255) NOT NULL,
	[ThoiGianBatDau] [datetime] NOT NULL,
	[ThoiGianKetThuc] [datetime] NOT NULL,
	[MieuTa] [nvarchar](max) NULL,
	[UuDai] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSuKien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhToan]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhToan](
	[MaThanhToan] [int] IDENTITY(1,1) NOT NULL,
	[MaDatPhong] [int] NULL,
	[TongTien] [decimal](10, 2) NOT NULL,
	[NgayThanhToan] [datetime] NOT NULL,
	[PhuongThucThanhToan] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaThanhToan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TienIch]    Script Date: 30/11/2023 08:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TienIch](
	[MaTienIch] [int] IDENTITY(1,1) NOT NULL,
	[TenTienIch] [nvarchar](255) NOT NULL,
	[MieuTa] [nvarchar](max) NULL,
	[pathIMG] [nvarchar](max) NULL,
	[SoLuong] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTienIch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] ON 

INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 1, 1, 2, CAST(50000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (2, 1, 2, 1, CAST(80000.00 AS Decimal(10, 2)), CAST(80000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (3, 2, 1, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (4, 1, 1, 2, CAST(100000.00 AS Decimal(10, 2)), CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (5, 1, 2, 3, CAST(150000.00 AS Decimal(10, 2)), CAST(450000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (6, 1, 3, 4, CAST(200000.00 AS Decimal(10, 2)), CAST(800000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (7, 1, 1, 1, CAST(100000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (8, 12, 1, 3, CAST(100000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (9, 12, 2, 4, CAST(150000.00 AS Decimal(10, 2)), CAST(600000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (10, 12, 3, 5, CAST(200000.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (11, 12, 4, 1, CAST(500000.00 AS Decimal(10, 2)), CAST(500000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (12, 12, 1, 3, CAST(100000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (13, 12, 2, 4, CAST(150000.00 AS Decimal(10, 2)), CAST(600000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (14, 12, 3, 5, CAST(200000.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (15, 12, 4, 1, CAST(500000.00 AS Decimal(10, 2)), CAST(500000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (16, 9, 1, 3, CAST(100000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (17, 9, 2, 2, CAST(150000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (18, 9, 4, 3, CAST(500000.00 AS Decimal(10, 2)), CAST(1500000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (19, 17, 1, 3, CAST(100000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (20, 19, 2, 2, CAST(150000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (21, 19, 1, 1, CAST(100000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (22, 19, 1, 1, CAST(100000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon], [MaHoaDon], [MaDichVu], [SoLuong], [DonGia], [ThanhTien]) VALUES (23, 19, 1, 1, CAST(100000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietLuuTru] ON 

INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (1, 1, 2, CAST(N'2023-09-20T14:00:00.000' AS DateTime), CAST(N'2023-09-22T12:00:00.000' AS DateTime), NULL, N'Đã Thanh Toán')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (2, 9, 1, CAST(N'2023-11-06T16:38:38.377' AS DateTime), CAST(N'2023-11-08T11:00:00.000' AS DateTime), NULL, N'Đã Thanh Toán')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (3, 10, 1, CAST(N'2023-11-07T06:39:51.917' AS DateTime), CAST(N'2023-11-08T11:59:59.000' AS DateTime), NULL, N'Đã Thanh Toán')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (4, 11, 1, CAST(N'2023-11-07T06:49:35.250' AS DateTime), CAST(N'2023-11-09T10:45:15.000' AS DateTime), NULL, N'Đã Thanh Toán')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (5, 12, 1, CAST(N'2023-11-08T16:13:40.470' AS DateTime), CAST(N'2023-11-10T11:30:00.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (6, 13, 1, CAST(N'2023-11-11T04:37:07.500' AS DateTime), CAST(N'2023-11-13T04:36:19.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (7, 14, 1, CAST(N'2023-11-11T04:47:05.540' AS DateTime), CAST(N'2023-11-15T04:46:44.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (8, 15, 1, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(N'2023-11-13T04:48:32.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (9, 16, 1, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(N'2023-11-13T04:48:32.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (10, 17, 1, CAST(N'2023-11-12T00:50:44.497' AS DateTime), CAST(N'2023-11-13T11:30:11.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (11, 18, 1, CAST(N'2023-11-12T00:54:18.000' AS DateTime), CAST(N'2023-11-15T11:50:22.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (12, 19, 1, CAST(N'2023-11-12T01:52:38.853' AS DateTime), CAST(N'2023-11-18T11:50:10.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (13, 20, 1, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(N'2023-11-20T11:50:56.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (14, 21, 1, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(N'2023-11-20T11:50:56.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (15, 22, 1, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(N'2023-11-14T02:17:54.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (16, 23, 1, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(N'2023-11-14T02:17:54.000' AS DateTime), NULL, N'Đang sử dụng')
INSERT [dbo].[ChiTietLuuTru] ([MaLuuTru], [MaDatPhong], [SoLuongNguoi], [ThoiGianVaoPhong], [ThoiGianDuKienTraPhong], [ThoiGianThucTeTraPhong], [TinhTrang]) VALUES (17, 24, 1, CAST(N'2023-11-14T18:28:13.713' AS DateTime), CAST(N'2023-11-18T11:50:45.000' AS DateTime), NULL, N'Đang sử dụng')
SET IDENTITY_INSERT [dbo].[ChiTietLuuTru] OFF
GO
SET IDENTITY_INSERT [dbo].[DatPhong] ON 

INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (1, 1, 1, CAST(N'2023-09-23T00:00:00.000' AS DateTime), CAST(N'2023-09-25T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (2, 2, 2, CAST(N'2023-09-24T00:00:00.000' AS DateTime), CAST(N'2023-09-26T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (3, 3, 3, CAST(N'2023-09-30T00:00:00.000' AS DateTime), CAST(N'2023-10-03T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (4, 4, 4, CAST(N'2023-10-01T00:00:00.000' AS DateTime), CAST(N'2023-10-05T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (5, 5, 5, CAST(N'2023-09-28T00:00:00.000' AS DateTime), CAST(N'2023-11-08T11:59:59.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (6, 1, 1, CAST(N'2023-09-20T00:00:00.000' AS DateTime), CAST(N'2023-09-25T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (7, 2, 2, CAST(N'2023-09-19T00:00:00.000' AS DateTime), CAST(N'2023-09-23T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (8, 3, 3, CAST(N'2023-09-21T00:00:00.000' AS DateTime), CAST(N'2023-09-23T00:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (9, 6, 1, CAST(N'2023-11-06T16:38:38.377' AS DateTime), CAST(N'2023-11-08T11:00:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (10, 6, 2, CAST(N'2023-11-07T06:39:51.917' AS DateTime), CAST(N'2023-11-08T11:59:59.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (11, 5, 7, CAST(N'2023-11-07T06:49:35.250' AS DateTime), CAST(N'2023-11-09T10:45:15.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (12, 6, 1, CAST(N'2023-11-08T16:13:40.470' AS DateTime), CAST(N'2023-11-10T11:30:00.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (13, 6, 3, CAST(N'2023-11-11T04:37:07.500' AS DateTime), CAST(N'2023-11-13T04:36:19.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (14, 6, 2, CAST(N'2023-11-11T04:47:05.540' AS DateTime), CAST(N'2023-11-15T04:46:44.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (15, 6, 4, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(N'2023-11-13T04:48:32.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (16, 6, 5, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(N'2023-11-13T04:48:32.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (17, 6, 1, CAST(N'2023-11-12T00:50:44.497' AS DateTime), CAST(N'2023-11-13T11:30:11.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (18, 2, 6, CAST(N'2023-11-12T00:54:18.000' AS DateTime), CAST(N'2023-11-15T11:50:22.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (19, 2, 10, CAST(N'2023-11-12T01:52:38.853' AS DateTime), CAST(N'2023-11-18T11:50:10.000' AS DateTime), N'Đang sử dụng')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (20, 6, 12, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(N'2023-11-20T11:50:56.000' AS DateTime), N'Đang sử dụng')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (21, 6, 13, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(N'2023-11-20T11:50:56.000' AS DateTime), N'Đang sử dụng')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (22, 6, 7, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(N'2023-11-14T02:17:54.000' AS DateTime), N'CheckOut Thành Công')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (23, 6, 11, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(N'2023-11-14T02:17:54.000' AS DateTime), N'Đang sử dụng')
INSERT [dbo].[DatPhong] ([MaDatPhong], [MaKhachHang], [MaPhong], [NgayNhanPhong], [NgayTraPhong], [TinhTrang]) VALUES (24, 2, 1, CAST(N'2023-11-14T18:28:13.713' AS DateTime), CAST(N'2023-11-18T11:50:45.000' AS DateTime), N'CheckOut Thành Công')
SET IDENTITY_INSERT [dbo].[DatPhong] OFF
GO
SET IDENTITY_INSERT [dbo].[DichVu] ON 

INSERT [dbo].[DichVu] ([MaDichVu], [TenDichVu], [Gia], [MaLoaiDichVu]) VALUES (1, N'Ẩm thực', CAST(100000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[DichVu] ([MaDichVu], [TenDichVu], [Gia], [MaLoaiDichVu]) VALUES (2, N'Làm đẹp', CAST(150000.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[DichVu] ([MaDichVu], [TenDichVu], [Gia], [MaLoaiDichVu]) VALUES (3, N'Dịch lịch', CAST(200000.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[DichVu] ([MaDichVu], [TenDichVu], [Gia], [MaLoaiDichVu]) VALUES (4, N'Bữa Tối', CAST(500000.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[DichVu] OFF
GO
SET IDENTITY_INSERT [dbo].[DichVuDuocSuDung] ON 

INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (1, 1, 1, CAST(N'2023-09-23T00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (2, 2, 2, CAST(N'2023-09-24T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (3, 1, 1, CAST(N'2023-11-06T16:39:23.000' AS DateTime), 2, 4)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (4, 2, 1, CAST(N'2023-11-06T16:39:25.000' AS DateTime), 3, 5)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (5, 3, 1, CAST(N'2023-11-06T16:39:25.000' AS DateTime), 4, 6)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (6, 1, 1, CAST(N'2023-11-06T17:21:47.000' AS DateTime), 1, 7)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (7, 1, 17, CAST(N'2023-11-12T03:25:48.000' AS DateTime), 3, 8)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (8, 2, 17, CAST(N'2023-11-12T03:25:49.000' AS DateTime), 4, 9)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (9, 3, 17, CAST(N'2023-11-12T03:25:50.000' AS DateTime), 5, 10)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (10, 4, 17, CAST(N'2023-11-12T03:25:50.000' AS DateTime), 1, 11)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (11, 1, 17, CAST(N'2023-11-12T03:25:48.000' AS DateTime), 3, 12)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (12, 2, 17, CAST(N'2023-11-12T03:25:49.000' AS DateTime), 4, 13)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (13, 3, 17, CAST(N'2023-11-12T03:25:50.000' AS DateTime), 5, 14)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (14, 4, 17, CAST(N'2023-11-12T03:25:50.000' AS DateTime), 1, 15)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (15, 1, 14, CAST(N'2023-11-12T03:34:36.000' AS DateTime), 3, 16)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (16, 2, 14, CAST(N'2023-11-12T03:34:36.000' AS DateTime), 2, 17)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (17, 4, 14, CAST(N'2023-11-12T03:36:16.000' AS DateTime), 3, 18)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (18, 1, 22, CAST(N'2023-11-12T03:42:23.000' AS DateTime), 3, 19)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (19, 2, 24, CAST(N'2023-11-19T01:47:47.000' AS DateTime), 2, 20)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (20, 1, 24, CAST(N'2023-11-19T01:47:47.000' AS DateTime), 1, 21)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (21, 1, 24, CAST(N'2023-11-23T02:32:51.000' AS DateTime), 1, 22)
INSERT [dbo].[DichVuDuocSuDung] ([MaSuDung], [MaDichVu], [MaDatPhong], [NgaySuDung], [SoLuong], [MaChiTietHoaDon]) VALUES (22, 1, 24, CAST(N'2023-11-23T03:06:55.000' AS DateTime), 1, 23)
SET IDENTITY_INSERT [dbo].[DichVuDuocSuDung] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (1, 1, 1, 1, CAST(N'2023-11-06T00:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(10, 2)), CAST(1550000.00 AS Decimal(10, 2)), CAST(650000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (2, 2, 2, 1, CAST(N'2023-09-21T00:00:00.000' AS DateTime), CAST(600000.00 AS Decimal(10, 2)), CAST(250000.00 AS Decimal(10, 2)), CAST(850000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (7, 12, 1, 1, CAST(N'2023-11-08T16:13:40.470' AS DateTime), CAST(4500000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(4500000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (8, 13, 1, 1, CAST(N'2023-11-11T04:37:07.500' AS DateTime), CAST(1200000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(1200000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (9, 14, 1, 1, CAST(N'2023-11-11T04:47:05.540' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(2100000.00 AS Decimal(10, 2)), CAST(3100000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (10, 15, 1, 1, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (11, 16, 1, 1, CAST(N'2023-11-11T04:50:40.933' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (12, 17, 1, 1, CAST(N'2023-11-12T00:50:44.497' AS DateTime), CAST(5250000.00 AS Decimal(10, 2)), CAST(4800000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (13, 18, 1, 1, CAST(N'2023-11-12T00:54:18.000' AS DateTime), CAST(1000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(1000000.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (14, 19, 1, 1, CAST(N'2023-11-12T01:52:38.853' AS DateTime), CAST(49000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Chưa thanh toán', 0)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (15, 20, 1, 1, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(10800000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Chưa thanh toán', 0)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (16, 21, 1, 1, CAST(N'2023-11-12T02:06:29.717' AS DateTime), CAST(9000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Chưa thanh toán', 0)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (17, 22, 1, 1, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(16000000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (18, 23, 1, 1, CAST(N'2023-11-12T02:18:43.213' AS DateTime), CAST(3000000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Chưa thanh toán', 0)
INSERT [dbo].[HoaDon] ([MaHoaDon], [MaDatPhong], [MaKhachHang], [MaNhanVien], [NgayLapHoaDon], [TongTienPhong], [TongTienDichVu], [TongTien], [TinhTrang], [DaThanhToan]) VALUES (19, 24, 1, 1, CAST(N'2023-11-14T18:28:13.713' AS DateTime), CAST(14250000.00 AS Decimal(10, 2)), CAST(600000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'Tiền Mặt', 1)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (1, N'Nguyễn Van A', N'nva@gmail.com', N'0901234567', N'23 Ðinh Tiên Hoàng, Q.1, TP.HCM', N'Thường')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (2, N'Tr?n Th? B', N'ttb@yahoo.com', N'0912345678', N'45 Nguy?n Th? Minh Khai, Q.3, TP.HCM', N'VIP')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (3, N'Ph?m Van C', N'pvc@hotmail.com', N'0923456789', N'12 Lý T? Tr?ng, Q.1, TP.HCM', N'Thu?ng')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (4, N'Lê Th? D', N'ltd@outlook.com', N'0934567890', N'89 Nguy?n Trãi, Q.5, TP.HCM', N'Thu?ng')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (5, N'Hoàng Van E', N'hve@zoho.com', N'0945678901', N'67 Ði?n Biên Ph?, Q.3, TP.HCM', N'VIP')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (6, N'Cao xuân huy', N'caohuy8899@gmail.com', N'0866697324', N'hà nội', N'Thường')
INSERT [dbo].[KhachHang] ([MaKhachHang], [HoTen], [Email], [SoDienThoai], [DiaChi], [LoaiKhachHang]) VALUES (7, N'Cao Xuân Lâm', N'lamCao123456', N'086666666', N'hà nội', N'VIP')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiDichVu] ON 

INSERT [dbo].[LoaiDichVu] ([MaLoaiDichVu], [TenLoaiDichVu], [MoTa]) VALUES (1, N'?m th?c', N'ẩm thực của khách sạn')
INSERT [dbo].[LoaiDichVu] ([MaLoaiDichVu], [TenLoaiDichVu], [MoTa]) VALUES (2, N'Spa & Massage', N'Dịch vụ làm đẹp , spa của khách sạn')
INSERT [dbo].[LoaiDichVu] ([MaLoaiDichVu], [TenLoaiDichVu], [MoTa]) VALUES (3, N'Du l?ch', N'Tua Du lịch của khách sạn')
SET IDENTITY_INSERT [dbo].[LoaiDichVu] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiPhong] ON 

INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (1, N'Phòng Đơn', CAST(1200000.00 AS Decimal(10, 2)), 1, N'Phòng dành cho m?t ngu?i')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (2, N'Phòng Ðôi', CAST(1500000.00 AS Decimal(10, 2)), 2, N'Phòng dành cho hai ngu?i')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (3, N'Phòng Gia Ðình', CAST(2500000.00 AS Decimal(10, 2)), 4, N'Phòng dành cho gia dình nh?')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (4, N'Phòng VIP', CAST(4000000.00 AS Decimal(10, 2)), 2, N'Phòng sang trọng dành cho VIP')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (5, N'Phòng Tập Thể', CAST(1000000.00 AS Decimal(10, 2)), 6, N'Phòng dành cho nhóm l?n')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (6, N'Phòng Hoàng gia', CAST(5000000.00 AS Decimal(10, 2)), 2, N'SUT là hạng phòng cao cấp nhất của mỗi khách sạn. Và với mục đích tăng thêm mức độ VIP, phòng SUT hay được đặt tên là: phòng Hoàng gia (Royal Suite Room), phòng Tổng Thống (President Room)… Một đặc điểm dễ nhận thấy là phòng Suite thường nằm ở vị trí cho tầm nhìn đẹp nhất và trong mỗi phòng như vậy có thể có nhiều phòng chức năng khác nhau: phòng khách, phòng ngủ, phòng họp, phòng ăn')
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaTheoDem], [SucChua], [MieuTa]) VALUES (7, N'Phòng Tổng Thống', CAST(10000000.00 AS Decimal(10, 2)), 5, N'Phòng Tổng Thống 10 Sao')
SET IDENTITY_INSERT [dbo].[LoaiPhong] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (1, N'1', N'1', N'Cao xuan huy', N'admin', CAST(0.00 AS Decimal(10, 2)), N'\img\Avatar\avatar.jpg')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (2, N'2', N'2', N'Nguy?n Th? Y', N'Quản lý', CAST(0.00 AS Decimal(10, 2)), N'\img\Avatar\IMG_20220911_204357_204.jpg')
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (3, N'nhanvien3', N'mk123456', N'Lê Van Z', N'Nhân Viên', CAST(6500000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (4, N'nhanvien4', N'mk123456', N'Tr?n Van K', N'Nhân Viên', CAST(5000000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (5, N'nhanvien5', N'mk123456', N'Hoàng Th? L', N'Nhân Viên', CAST(7000000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (6, N'nhanvien6', N'mk123456', N'Duong Van Minh', N'Nhân Viên', CAST(5000000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (7, N'nhanvien7', N'mk123456', N'Nguy?n Th? Lan', N'Nhân Viên', CAST(7000000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (8, N'nhanvien8', N'mk654321', N'Lê Th? Hà', N'Nhân Viên', CAST(4500000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenDangNhap], [MatKhau], [HoTen], [ChucVu], [Luong], [PathAvatar]) VALUES (10, N'9', N'9', N'HUY HUYHUY', N'Nhân viên', CAST(4500000.00 AS Decimal(10, 2)), N'')
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
INSERT [dbo].[NhanVien_Quyen] ([MaNhanVien], [MaQuyen]) VALUES (1, 1)
INSERT [dbo].[NhanVien_Quyen] ([MaNhanVien], [MaQuyen]) VALUES (2, 2)
INSERT [dbo].[NhanVien_Quyen] ([MaNhanVien], [MaQuyen]) VALUES (3, 3)
INSERT [dbo].[NhanVien_Quyen] ([MaNhanVien], [MaQuyen]) VALUES (4, 3)
INSERT [dbo].[NhanVien_Quyen] ([MaNhanVien], [MaQuyen]) VALUES (5, 3)
GO
SET IDENTITY_INSERT [dbo].[Phong] ON 

INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (1, N'100', 2, N'Đang don dẹp')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (2, N'101', 5, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (3, N'102', 1, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (4, N'103', 5, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (5, N'104', 5, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (6, N'105', 5, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (7, N'106', 4, N'Đang don dẹp')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (8, N'107', 2, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (9, N'108', 4, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (10, N'109', 5, N'Đang sử dụng')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (11, N'110', 2, N'Đang sử dụng')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (12, N'111', 1, N'Đang sử dụng')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (13, N'112', 5, N'Đang sử dụng')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (14, N'113', 1, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (15, N'114', 4, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (16, N'115', 4, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (17, N'116', 2, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (18, N'117', 2, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (19, N'118', 4, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (20, N'119', 1, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (21, N'120', 1, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (22, N'121', 7, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (23, N'122', 3, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (24, N'123', 3, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (25, N'124', 3, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (26, N'125', 3, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (27, N'126', 6, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (28, N'127', 6, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (29, N'128', 6, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (30, N'129', 6, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (31, N'130', 6, N'Trống')
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [MaLoaiPhong], [TinhTrang]) VALUES (32, N'131', 1, N'Trống')
SET IDENTITY_INSERT [dbo].[Phong] OFF
GO
SET IDENTITY_INSERT [dbo].[PhongTienIch] ON 

INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (1, 5, 10, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (2, 2, 3, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (3, 2, 2, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (4, 2, 4, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (5, 4, 5, N'Hoạt động', 3)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (6, 4, 5, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (7, 5, 9, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (8, 5, 9, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (9, 16, 9, N'Hoạt động', 1)
INSERT [dbo].[PhongTienIch] ([MaPhongTienIch], [MaPhong], [MaTienIch], [TrangThai], [SoLuong]) VALUES (10, 6, 10, N'Hoạt động', 2)
SET IDENTITY_INSERT [dbo].[PhongTienIch] OFF
GO
SET IDENTITY_INSERT [dbo].[Quyen] ON 

INSERT [dbo].[Quyen] ([MaQuyen], [TenQuyen], [MieuTa]) VALUES (1, N'admin', N'admin')
INSERT [dbo].[Quyen] ([MaQuyen], [TenQuyen], [MieuTa]) VALUES (2, N'Quản lý', N'Quyền của quản lý khách sạn')
INSERT [dbo].[Quyen] ([MaQuyen], [TenQuyen], [MieuTa]) VALUES (3, N'Nhân viên', N'Quyền của nhân viên tiếp tân')
SET IDENTITY_INSERT [dbo].[Quyen] OFF
GO
SET IDENTITY_INSERT [dbo].[TienIch] ON 

INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (1, N'Wi-Fi', N'Mi?n phí truy c?p Wi-Fi trong toàn b? khu v?c khách s?n', N'\img\TienNghi\wifi.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (2, N'Bơi Lội', N'B? boi ngoài tr?i v?i qu?y bar', N'\img\TienNghi\phongngubeboi1.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (3, N'Phòng Tập', N'Phòng t?p hi?n d?i v?i d?ch v? hu?n luy?n viên cá nhân', N'\img\TienNghi\gym.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (4, N'Spa', N'D?ch v? massage và cham sóc s?c kh?e', N'\img\TienNghi\massage.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (5, N'Tivi TLC', N'Tivi 32', N'\img\TienNghi\Tivi_TCL.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (6, N'Lò Xi Sóng', N'Làm Chín , Hâm Nóng Đồ ăn Tức Thì', N'\img\TienNghi\LoXiSong.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (7, N'Máy lọc Không Khí', N'Máy lọc Không Khí', N'\img\TienNghi\241702-600x600-1.png', 10)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (9, N'Điều Hòa', N'Làm Mát Phòng Công Xuất : 24000', N'\img\TienNghi\10051354-may-lanh-daikin-inverter-1-hp-ftky25wmvmv-2.png', 4)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (10, N'Smart Tivi', N'Smart Tivi 65 Inch', N'\img\TienNghi\10052165-smart-tivi-nanocell-lg-4k-65-inch-65nano76sqa-1.png', 8)
INSERT [dbo].[TienIch] ([MaTienIch], [TenTienIch], [MieuTa], [pathIMG], [SoLuong]) VALUES (11, N'Tủ lạnh Panasonic', N'Tủ lạnh Panasonic Inverter 650 lít PRIME+ Edition Multi Door NR-WY720ZMMV', N'\img\TienNghi\tu-lanh-panasonic-inverter-650-lit-multi-door-nr-wy720zmmv-051023-100239-600x600.png', 10)
SET IDENTITY_INSERT [dbo].[TienIch] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__NhanVien__55F68FC0522B8A9A]    Script Date: 30/11/2023 08:24:25 ******/
ALTER TABLE [dbo].[NhanVien] ADD UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Quyen__5637EE79BD91CC9B]    Script Date: 30/11/2023 08:24:25 ******/
ALTER TABLE [dbo].[Quyen] ADD UNIQUE NONCLUSTERED 
(
	[TenQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HoaDon] ADD  DEFAULT ((0)) FOR [DaThanhToan]
GO
ALTER TABLE [dbo].[PhongTienIch] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[TienIch] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaDichVu])
REFERENCES [dbo].[DichVu] ([MaDichVu])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietLuuTru]  WITH CHECK ADD FOREIGN KEY([MaDatPhong])
REFERENCES [dbo].[DatPhong] ([MaDatPhong])
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[DichVu]  WITH CHECK ADD FOREIGN KEY([MaLoaiDichVu])
REFERENCES [dbo].[LoaiDichVu] ([MaLoaiDichVu])
GO
ALTER TABLE [dbo].[DichVuDuocSuDung]  WITH CHECK ADD FOREIGN KEY([MaChiTietHoaDon])
REFERENCES [dbo].[ChiTietHoaDon] ([MaChiTietHoaDon])
GO
ALTER TABLE [dbo].[DichVuDuocSuDung]  WITH CHECK ADD FOREIGN KEY([MaDatPhong])
REFERENCES [dbo].[DatPhong] ([MaDatPhong])
GO
ALTER TABLE [dbo].[DichVuDuocSuDung]  WITH CHECK ADD FOREIGN KEY([MaDichVu])
REFERENCES [dbo].[DichVu] ([MaDichVu])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaDatPhong])
REFERENCES [dbo].[DatPhong] ([MaDatPhong])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[NhanVien_Quyen]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[NhanVien_Quyen]  WITH CHECK ADD FOREIGN KEY([MaQuyen])
REFERENCES [dbo].[Quyen] ([MaQuyen])
GO
ALTER TABLE [dbo].[PhanHoi]  WITH CHECK ADD FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD FOREIGN KEY([MaLoaiPhong])
REFERENCES [dbo].[LoaiPhong] ([MaLoaiPhong])
GO
ALTER TABLE [dbo].[PhongTienIch]  WITH CHECK ADD FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[PhongTienIch]  WITH CHECK ADD FOREIGN KEY([MaTienIch])
REFERENCES [dbo].[TienIch] ([MaTienIch])
GO
ALTER TABLE [dbo].[ThanhToan]  WITH CHECK ADD FOREIGN KEY([MaDatPhong])
REFERENCES [dbo].[DatPhong] ([MaDatPhong])
GO
ALTER TABLE [dbo].[PhanHoi]  WITH CHECK ADD CHECK  (([DanhGia]>=(1) AND [DanhGia]<=(5)))
GO
