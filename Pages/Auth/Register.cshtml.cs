using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public RegisterModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [BindProperty]
        public string HoTen { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string MatKhau { get; set; } = string.Empty;
        [BindProperty]
        public string SoDienThoai { get; set; } = string.Empty;
        [BindProperty]
        public string DiaChi { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(HoTen) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MatKhau))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
                return Page();
            }
            var kh = new KhachHang
            {
                MaKhachHang = System.Guid.NewGuid().ToString("N").Substring(0, 8),
                HoTen = HoTen,
                Email = Email,
                MatKhau = MatKhau,
                SoDienThoai = SoDienThoai,
                DiaChi = DiaChi
            };
            await _khachHangService.CreateAsync(kh);
            Success = true;
            return Page();
        }
    }
} 