using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.QuanTriVienPages
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly QuanTriVienService _service;
        public DeleteModel(QuanTriVienService service)
        {
            _service = service;
        }
        [BindProperty]
        public QuanTriVien Admin { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var admin = await _service.GetByIdAsync(id);
            if (admin == null) return NotFound();
            Admin = admin;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _service.DeleteAsync(id);
            if (result)
            {
                TempData["Success"] = "Xóa tài khoản quản trị thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa tài khoản quản trị.");
            return Page();
        }
    }
} 