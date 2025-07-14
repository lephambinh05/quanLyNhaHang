using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin quản trị viên
    /// </summary>
    public class QuanTriVien
    {
        [Key]
        [StringLength(10)]
        public string MaQuanTriVien { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string VaiTro { get; set; }

        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh ChiNhanh { get; set; }
    }
} 