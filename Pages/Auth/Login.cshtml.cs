using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NhaHang.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public LoginModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string MatKhau { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string? HoTen { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // Nếu đã đăng nhập thì redirect về trang chủ
            if (User.Identity?.IsAuthenticated == true)
            {
                Response.Redirect("/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MatKhau))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
                return Page();
            }

            var kh = await _khachHangService.GetAllAsync();
            var user = kh.Find(x => x.Email == Email && x.MatKhau == MatKhau);
            if (user == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không đúng.";
                return Page();
            }

            // Tạo claims cho user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.MaKhachHang),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim("role", "Customer") // Thêm claim này để tương thích
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CustomerCookie");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
            };

            await HttpContext.SignInAsync("CustomerCookie", new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirect về trang được yêu cầu ban đầu hoặc trang chủ
            var returnUrl = Request.Query["ReturnUrl"].ToString();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index");
        }
    }
} 