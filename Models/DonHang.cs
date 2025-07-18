using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin đơn hàng
    /// </summary>
    public class DonHang
    {
        [Key]
        [StringLength(10)]
        public string MaDonHang { get; set; }

        [StringLength(10)]
        public string? MaKhachHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKhachHang { get; set; }

        [Required]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChiGiaoHang { get; set; }

        public DateTime NgayDatHang { get; set; } = DateTime.Now;

        //[Required] // BỎ Required
        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(50)]
        public string? PhuongThucThanhToan { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        [StringLength(10)]
        public string? MaKhuyenMai { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }

        // Navigation properties
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh? ChiNhanh { get; set; }
        [ForeignKey("MaKhuyenMai")]
        public KhuyenMai? KhuyenMai { get; set; }
        [ForeignKey("MaKhachHang")]
        public KhachHang? KhachHang { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
        public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
} 