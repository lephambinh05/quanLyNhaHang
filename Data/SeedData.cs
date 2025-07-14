using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NhaHang.Models;
using System;
using System.Linq;

namespace NhaHang.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Nếu đã có dữ liệu thì không seed nữa
                if (context.ChiNhanhs.Any()) return;

                // Seed ChiNhanh
                var branches = new[]
                {
                    new ChiNhanh { MaChiNhanh = "CN01", TenChiNhanh = "Chi nhánh Quận 1", DiaChi = "123 Lê Lợi, Q1", SoDienThoai = "0909123456", Email = "q1@demo.com", TenQuanLy = "Nguyễn Văn A" },
                    new ChiNhanh { MaChiNhanh = "CN02", TenChiNhanh = "Chi nhánh Quận 2", DiaChi = "456 Mai Chí Thọ, Q2", SoDienThoai = "0909234567", Email = "q2@demo.com", TenQuanLy = "Trần Thị B" }
                };
                context.ChiNhanhs.AddRange(branches);

                // Seed DanhMuc
                var categories = new[]
                {
                    new DanhMuc { MaDanhMuc = "DM01", TenDanhMuc = "Món chính", MaChiNhanh = "CN01", MoTa = "" },
                    new DanhMuc { MaDanhMuc = "DM02", TenDanhMuc = "Đồ uống", MaChiNhanh = "CN01", MoTa = "" },
                    new DanhMuc { MaDanhMuc = "DM03", TenDanhMuc = "Món chính", MaChiNhanh = "CN02", MoTa = "" }
                };
                context.DanhMucs.AddRange(categories);

                // Seed MonAn
                var foods = new[]
                {
                    new MonAn { MaMonAn = "MA01", TenMonAn = "Phở bò", Gia = 50000, MaDanhMuc = "DM01", MaChiNhanh = "CN01", CoSan = true, HinhAnh = "", MoTa = "" },
                    new MonAn { MaMonAn = "MA02", TenMonAn = "Cà phê sữa", Gia = 25000, MaDanhMuc = "DM02", MaChiNhanh = "CN01", CoSan = true, HinhAnh = "", MoTa = "" },
                    new MonAn { MaMonAn = "MA03", TenMonAn = "Bún chả", Gia = 60000, MaDanhMuc = "DM03", MaChiNhanh = "CN02", CoSan = true, HinhAnh = "", MoTa = "" }
                };
                context.MonAns.AddRange(foods);

                // Seed KhachHang
                var customers = new[]
                {
                    new KhachHang { MaKhachHang = "KH01", Email = "kh1@demo.com", MatKhau = "123456", HoTen = "Lê Văn C", SoDienThoai = "0909345678", DiaChi = "" },
                    new KhachHang { MaKhachHang = "KH02", Email = "kh2@demo.com", MatKhau = "123456", HoTen = "Phạm Thị D", SoDienThoai = "0909456789", DiaChi = "" }
                };
                context.KhachHangs.AddRange(customers);

                // Seed QuanTriVien
                var admins = new[]
                {
                    new QuanTriVien { MaQuanTriVien = "AD01", Email = "admin@demo.com", MatKhau = "123456", HoTen = "Admin Tổng", VaiTro = "Quản trị Tổng", MaChiNhanh = "CN01" },
                    new QuanTriVien { MaQuanTriVien = "QL01", Email = "q1@demo.com", MatKhau = "123456", HoTen = "Nguyễn Văn A", VaiTro = "Quản trị Chi nhánh", MaChiNhanh = "CN01" }
                };
                context.QuanTriViens.AddRange(admins);

                // Seed KhuyenMai
                var km = new KhuyenMai {
                    MaKhuyenMai = "KM01",
                    MaGiamGia = "SALE10",
                    TyLeGiam = 10,
                    NgayBatDau = DateTime.Now.AddDays(-10),
                    NgayKetThuc = DateTime.Now.AddDays(10),
                    ToanHeThong = true,
                    HinhAnhBanner = "banner.jpg",
                    MaChiNhanh = "CN01",
                    MoTa = "",
                    TieuDeBanner = "Khuyến mãi lớn!",
                    NoiDungBanner = "" // Thêm nội dung banner rỗng để tránh lỗi NOT NULL
                };
                context.KhuyenMais.Add(km);

                // Lưu thay đổi
                context.SaveChanges();
            }
        }
    }
} 