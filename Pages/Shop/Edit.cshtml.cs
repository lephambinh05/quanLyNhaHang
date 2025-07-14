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
    public class EditModel : PageModel
    {
        private readonly ShopService _shopService;
        public EditModel(ShopService shopService)
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
        public async Task<IActionResult> OnPostAsync()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order.MaKhachHang = customerId;
            if (!ModelState.IsValid) return Page();
            var result = await _shopService.UpdateOrderAsync(Order);
            if (result)
            {
                TempData["Success"] = "Cập nhật đơn hàng thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể cập nhật đơn hàng.");
            return Page();
        }
    }
} 