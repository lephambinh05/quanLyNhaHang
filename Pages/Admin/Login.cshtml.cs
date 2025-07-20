using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        public bool Success { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";
        }

        public async Task<IActionResult> OnGet()
        {
            var result = await HttpContext.AuthenticateAsync("AdminCookie");
            if (result.Succeeded && result.Principal != null &&
                (result.Principal.IsInRole("Admin") || result.Principal.IsInRole("SuperAdmin")))
            {
                return RedirectToPage("/Admin/Dashboard/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();
            var admin = await _adminService.LoginAsync(Input.Email, Input.Password);
            if (admin == null || (admin.VaiTro != "SuperAdmin" && admin.VaiTro != "Admin"))
            {
                ErrorMessage = "Tài khoản hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
                return Page();
            }
            // Đăng nhập (set cookie/session)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.HoTen ?? admin.Email),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Role, admin.VaiTro ?? "")
            };
            var claimsIdentity = new ClaimsIdentity(claims, "AdminCookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("AdminCookie", claimsPrincipal);
            // Lưu thông tin admin vào session
            HttpContext.Session.SetString("AdminEmail", admin.Email);
            HttpContext.Session.SetString("AdminRole", admin.VaiTro ?? "");
            // Nếu có ReturnUrl thì chuyển về đó, không thì về dashboard
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToPage("/Admin/Dashboard/Index");
        }
    }
} 