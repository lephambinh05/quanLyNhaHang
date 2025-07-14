using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin chương trình khuyến mãi
    /// </summary>
    public class KhuyenMai
    {
        [Key]
        [StringLength(10)]
        public string MaKhuyenMai { get; set; }

        [Required]
        [StringLength(20)]
        public string MaGiamGia { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        [Range(0, 100)]
        public decimal TyLeGiam { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        public bool ToanHeThong { get; set; } = true;

        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        [StringLength(255)]
        public string HinhAnhBanner { get; set; }

        [StringLength(100)]
        public string TieuDeBanner { get; set; }

        [StringLength(255)]
        public string NoiDungBanner { get; set; }

        // Navigation properties
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh ChiNhanh { get; set; }
    }
} 