using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly QuanTriVienService _adminService;
        public AdminLoginModel(QuanTriVienService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var admin = await _adminService.LoginAsync(Input.Email, Input.Password);
            if (admin == null || (admin.VaiTro != "SuperAdmin" && admin.VaiTro != "Manager"))
            {
                ErrorMessage = "Tài khoản hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
                return Page();
            }
            // TODO: Đăng nhập (set cookie/session)
            // Chuyển hướng về dashboard admin
            return RedirectToPage("/Admin/Dashboard/Index");
        }
    }
} 