using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.KhachHangPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class DeleteModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public DeleteModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [BindProperty]
        public KhachHang Customer { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var customer = await _khachHangService.GetByIdAsync(id);
            if (customer == null) return NotFound();
            Customer = customer;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _khachHangService.DeleteAsync(id);
            if (result)
            {
                TempData["Success"] = "Xóa khách hàng thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa khách hàng.");
            return Page();
        }
    }
} 