-- Xóa cơ sở dữ liệu nếu đã tồn tại để tránh xung đột
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'HeThongQuanAn')
    DROP DATABASE HeThongQuanAn;
GO

-- Tạo cơ sở dữ liệu
CREATE DATABASE HeThongQuanAn;
GO

USE HeThongQuanAn;
GO

-- Bảng: ChiNhanh
CREATE TABLE ChiNhanh (
    MaChiNhanh VARCHAR(10) PRIMARY KEY,
    TenChiNhanh NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NOT NULL,
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    TenQuanLy NVARCHAR(100),
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Bảng: KhuyenMai
CREATE TABLE KhuyenMai (
    MaKhuyenMai VARCHAR(10) PRIMARY KEY,
    MaGiamGia NVARCHAR(20) UNIQUE NOT NULL,
    MoTa NVARCHAR(255),
    TyLeGiam DECIMAL(5,2) CHECK (TyLeGiam BETWEEN 0 AND 100), -- Tỷ lệ giảm giá 0-100%
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    ToanHeThong BIT DEFAULT 1,
    MaChiNhanh VARCHAR(10) NULL,
    HinhAnhBanner NVARCHAR(255),
    TieuDeBanner NVARCHAR(100),
    NoiDungBanner NVARCHAR(255),
    CONSTRAINT CHK_NgayKetThuc CHECK (NgayKetThuc >= NgayBatDau),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);
GO

-- Bảng: DanhMuc
CREATE TABLE DanhMuc (
    MaDanhMuc VARCHAR(10) PRIMARY KEY,
    TenDanhMuc NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255),
    MaChiNhanh VARCHAR(10) NULL,
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);
GO

-- Bảng: MonAn
CREATE TABLE MonAn (
    MaMonAn VARCHAR(10) PRIMARY KEY,
    TenMonAn NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    Gia DECIMAL(10,2) NOT NULL CHECK (Gia >= 0),
    MucCay INT CHECK (MucCay BETWEEN 0 AND 7),
    HinhAnh NVARCHAR(255),
    MaDanhMuc VARCHAR(10) NOT NULL,
    MaChiNhanh VARCHAR(10) NOT NULL,
    CoSan BIT DEFAULT 1,
    FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);
GO

-- Bảng: KhachHang
CREATE TABLE KhachHang (
    MaKhachHang VARCHAR(10) PRIMARY KEY,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Bảng: DonHang
CREATE TABLE DonHang (
    MaDonHang VARCHAR(10) PRIMARY KEY,
    MaKhachHang VARCHAR(10) NULL,
    TenKhachHang NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20) NOT NULL,
    DiaChiGiaoHang NVARCHAR(255) NOT NULL,
    NgayDatHang DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) NOT NULL CHECK (TrangThai IN (N'Chưa xử lý', N'Đang chế biến', N'Đang giao', N'Hoàn thành', N'Hủy')),
    MaChiNhanh VARCHAR(10) NOT NULL,
    MaKhuyenMai VARCHAR(10) NULL,
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);
GO

-- Bảng: ChiTietDonHang
CREATE TABLE ChiTietDonHang (
    MaChiTietDonHang VARCHAR(10) PRIMARY KEY,
    MaDonHang VARCHAR(10) NOT NULL,
    MaMonAn VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    MucCay INT CHECK (MucCay BETWEEN 0 AND 7),
    DonGia DECIMAL(10,2) NOT NULL CHECK (DonGia >= 0),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn)
);
GO

-- Bảng: HoaDon
CREATE TABLE HoaDon (
    MaHoaDon VARCHAR(10) PRIMARY KEY,
    MaDonHang VARCHAR(10) NOT NULL,
    TongTien DECIMAL(12,2) NOT NULL CHECK (TongTien >= 0),
    PhuongThucThanhToan NVARCHAR(50) NOT NULL CHECK (PhuongThucThanhToan IN (N'Tiền mặt', N'Chuyển khoản')),
    TrangThaiThanhToan NVARCHAR(50) NOT NULL CHECK (TrangThaiThanhToan IN (N'Chưa thanh toán', N'Đã thanh toán')),
    NgayThanhToan DATETIME NULL,
    MaKhuyenMai VARCHAR(10) NULL,
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai)
);
GO

-- Bảng: QuanTriVien
CREATE TABLE QuanTriVien (
    MaQuanTriVien VARCHAR(10) PRIMARY KEY,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(50) NOT NULL CHECK (VaiTro IN (N'Quản trị Tổng', N'Quản trị Chi nhánh')),
    MaChiNhanh VARCHAR(10) NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);
GO