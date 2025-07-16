using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin.QuanTriVien
{
    [Authorize(Roles = "SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly QuanTriVienService _service;
        public EditModel(QuanTriVienService service)
        {
            _service = service;
        }
        [BindProperty]
        public NhaHang.Models.QuanTriVien Admin { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var admin = await _service.GetByIdAsync(id);
            if (admin == null) return NotFound();
            Admin = admin;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            // Lấy thông tin cũ từ DB
            var oldAdmin = await _service.GetByIdAsync(Admin.MaQuanTriVien!);
            if (oldAdmin == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy tài khoản quản trị.");
                return Page();
            }
            // Nếu không nhập mật khẩu mới thì giữ nguyên mật khẩu cũ
            if (string.IsNullOrWhiteSpace(Admin.MatKhau))
            {
                Admin.MatKhau = oldAdmin.MatKhau;
            }
            var result = await _service.UpdateAsync(Admin);
            if (result)
            {
                TempData["Success"] = "Cập nhật tài khoản quản trị thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể cập nhật tài khoản quản trị.");
            return Page();
        }
    }
} 