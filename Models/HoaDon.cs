using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin hóa đơn thanh toán
    /// </summary>
    public class HoaDon
    {
        [Key]
        [StringLength(10)]
        public string MaHoaDon { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDonHang { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TongTien { get; set; }

        [Required]
        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThaiThanhToan { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        [StringLength(10)]
        public string MaKhuyenMai { get; set; }

        // Navigation properties
        [ForeignKey("MaDonHang")]
        public DonHang DonHang { get; set; }
        [ForeignKey("MaKhuyenMai")]
        public KhuyenMai KhuyenMai { get; set; }
    }
} 