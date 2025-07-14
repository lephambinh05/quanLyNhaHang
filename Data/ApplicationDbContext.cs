using Microsoft.EntityFrameworkCore;
using NhaHang.Models;

namespace NhaHang.Data
{
    /// <summary>
    /// DbContext cho hệ thống quản lý chuỗi nhà hàng
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<QuanTriVien> QuanTriViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cấu hình các quan hệ, ràng buộc đặc biệt nếu cần
            // (Có thể bổ sung cấu hình Fluent API ở đây nếu cần)

            // Sửa lỗi multiple cascade paths: MonAn - DanhMuc
            modelBuilder.Entity<MonAn>()
                .HasOne(ma => ma.DanhMuc)
                .WithMany(dm => dm.MonAns)
                .HasForeignKey(ma => ma.MaDanhMuc)
                .OnDelete(DeleteBehavior.Restrict);

            // Sửa lỗi multiple cascade paths: DonHang - KhuyenMai
            modelBuilder.Entity<DonHang>()
                .HasOne(dh => dh.KhuyenMai)
                .WithMany()
                .HasForeignKey(dh => dh.MaKhuyenMai)
                .OnDelete(DeleteBehavior.Restrict);

            // Sửa lỗi multiple cascade paths: DonHang - KhachHang
            modelBuilder.Entity<DonHang>()
                .HasOne(dh => dh.KhachHang)
                .WithMany(kh => kh.DonHangs)
                .HasForeignKey(dh => dh.MaKhachHang)
                .OnDelete(DeleteBehavior.Restrict);

            // Sửa lỗi multiple cascade paths: ChiTietDonHang - MonAn
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(ct => ct.MonAn)
                .WithMany(ma => ma.ChiTietDonHangs)
                .HasForeignKey(ct => ct.MaMonAn)
                .OnDelete(DeleteBehavior.Restrict);

            // Sửa lỗi multiple cascade paths: HoaDon - KhuyenMai
            modelBuilder.Entity<HoaDon>()
                .HasOne(hd => hd.KhuyenMai)
                .WithMany()
                .HasForeignKey(hd => hd.MaKhuyenMai)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 