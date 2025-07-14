using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaHang.Models
{
    /// <summary>
    /// Danh mục món ăn
    /// </summary>
    public class DanhMuc
    {
        [Key]
        [StringLength(10)]
        public string MaDanhMuc { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDanhMuc { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        [StringLength(10)]
        public string MaChiNhanh { get; set; }

        // Navigation properties
        [ForeignKey("MaChiNhanh")]
        public ChiNhanh ChiNhanh { get; set; }
        public ICollection<MonAn> MonAns { get; set; }
    }
} 