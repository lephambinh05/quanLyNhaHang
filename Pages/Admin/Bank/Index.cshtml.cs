using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Data;
using System.Threading.Tasks;
using System.Linq;

namespace NhaHang.Pages.Admin.Bank
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ThongTinThanhToan ThongTin { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            ThongTin = _context.ThongTinThanhToans.FirstOrDefault() ?? new ThongTinThanhToan();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin.";
                return Page();
            }
            var existing = _context.ThongTinThanhToans.FirstOrDefault();
            if (existing != null)
            {
                existing.ChuTaiKhoan = ThongTin.ChuTaiKhoan;
                existing.SoTaiKhoan = ThongTin.SoTaiKhoan;
                existing.NganHang = ThongTin.NganHang;
                _context.Update(existing);
            }
            else
            {
                _context.Add(ThongTin);
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật thông tin thành công!";
            return RedirectToPage();
        }
    }
} 