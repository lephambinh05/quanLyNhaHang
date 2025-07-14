using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NhaHang.Pages.Shop
{
    [Authorize(Roles = "Customer")]
    public class DeleteModel : PageModel
    {
        private readonly ShopService _shopService;
        public DeleteModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        [BindProperty]
        public DonHang Order { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _shopService.GetOrderByIdAsync(id);
            if (order == null || order.MaKhachHang != customerId) return NotFound();
            Order = order;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var order = await _shopService.GetOrderByIdAsync(id);
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (order == null || order.MaKhachHang != customerId) return NotFound();
            var result = await _shopService.DeleteOrderAsync(id);
            if (result)
            {
                TempData["Success"] = "Xóa đơn hàng thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa đơn hàng.");
            return Page();
        }
    }
} 