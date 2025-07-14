using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Models
{
    /// <summary>
    /// Thông tin khách hàng
    /// </summary>
    public class KhachHang
    {
        [Key]
        [StringLength(10)]
        public string MaKhachHang { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<DonHang> DonHangs { get; set; }
    }
} 