using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.QuanTriVienPages
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly QuanTriVienService _service;
        public CreateModel(QuanTriVienService service)
        {
            _service = service;
        }
        [BindProperty]
        public QuanTriVien Admin { get; set; } = new();
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _service.CreateAsync(Admin);
            if (result)
            {
                TempData["Success"] = "Thêm tài khoản quản trị thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể thêm tài khoản quản trị.");
            return Page();
        }
    }
} 