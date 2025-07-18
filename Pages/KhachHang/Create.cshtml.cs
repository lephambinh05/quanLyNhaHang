using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.KhachHangPages
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CreateModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public CreateModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [BindProperty]
        public KhachHang Customer { get; set; } = new();
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _khachHangService.CreateAsync(Customer);
            if (result)
            {
                TempData["Success"] = "Thêm khách hàng thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể thêm khách hàng.");
            return Page();
        }
    }
} 