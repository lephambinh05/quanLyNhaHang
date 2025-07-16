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
        /// <summary>
        /// Mã khuyến mãi (tự sinh, không nhập tay)
        /// </summary>
        [Key]
        [StringLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Không nhập từ form, sinh tự động trong service
        public string? MaKhuyenMai { get; set; }

        [Required]
        [StringLength(20)]
        public string MaGiamGia { get; set; }

        [StringLength(255)]
        public string? MoTa { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal TyLeGiam { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        public bool ToanHeThong { get; set; } = true;

        [StringLength(10)]
        public string? MaChiNhanh { get; set; } = string.Empty;

        [StringLength(255)]
        public string? HinhAnhBanner { get; set; } = string.Empty;

        [StringLength(100)]
        public string? TieuDeBanner { get; set; } = string.Empty;

        [StringLength(255)]
        public string? NoiDungBanner { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh? ChiNhanh { get; set; }
    }
} 