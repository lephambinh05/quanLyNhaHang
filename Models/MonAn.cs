using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin món ăn
    /// </summary>
    public class MonAn
    {
        [Key]
        [StringLength(10)]
        public string MaMonAn { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMonAn { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        [Range(0, 7)]
        public int? MucCay { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDanhMuc { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        public bool CoSan { get; set; } = true;

        // Navigation properties
        [ForeignKey("MaDanhMuc")]
        public DanhMuc DanhMuc { get; set; }
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh ChiNhanh { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
} 