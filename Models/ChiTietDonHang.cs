using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Chi tiết đơn hàng
    /// </summary>
    public class ChiTietDonHang
    {
        [Key]
        [StringLength(10)]
        public string MaChiTietDonHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDonHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaMonAn { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }

        [Range(0, 7)]
        public int? MucCay { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal DonGia { get; set; }

        // Navigation properties
        [ForeignKey("MaDonHang")]
        public DonHang DonHang { get; set; }
        [ForeignKey("MaMonAn")]
        public MonAn MonAn { get; set; }
    }
} 