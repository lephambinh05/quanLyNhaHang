using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin chi nhánh nhà hàng
    /// </summary>
    public class ChiNhanh
    {
        [Key]
        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        [Required]
        [StringLength(100)]
        public string TenChiNhanh { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string TenQuanLy { get; set; }

        public string? HinhAnh { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<KhuyenMai> KhuyenMais { get; set; }
        public ICollection<DanhMuc> DanhMucs { get; set; }
        public ICollection<MonAn> MonAns { get; set; }
        public ICollection<DonHang> DonHangs { get; set; }
        public ICollection<QuanTriVien> QuanTriViens { get; set; }
    }
} 