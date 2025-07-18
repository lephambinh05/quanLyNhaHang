using System.ComponentModel.DataAnnotations;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin tài khoản thanh toán ngân hàng
    /// </summary>
    public class ThongTinThanhToan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ChuTaiKhoan { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string SoTaiKhoan { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NganHang { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ApiKey { get; set; }
    }
} 