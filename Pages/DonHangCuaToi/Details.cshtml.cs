using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using System.Security.Claims;

namespace nhaHang.Pages.DonHangCuaToi
{
    public class DetailsModel : PageModel
    {
        private readonly OrderService _orderService;
        public DonHang? DonHang { get; set; }
        public DetailsModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var maKhachHang = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(maKhachHang)) return RedirectToPage("/Auth/Login");
            var don = await _orderService.GetByIdAsync(id);
            if (don == null || don.MaKhachHang != maKhachHang) return NotFound();
            DonHang = don;
            return Page();
        }
    }
} 