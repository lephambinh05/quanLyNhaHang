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
    public class CreateModel : PageModel
    {
        private readonly ShopService _shopService;
        public CreateModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        [BindProperty]
        public DonHang Order { get; set; } = new();
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order.MaKhachHang = customerId;
            if (!ModelState.IsValid) return Page();
            var result = await _shopService.CreateOrderAsync(Order);
            if (result)
            {
                TempData["Success"] = "Đặt hàng thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể đặt hàng.");
            return Page();
        }
    }
} 