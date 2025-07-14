using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NhaHang.Pages.Shop
{
    [Authorize(Roles = "Customer")]
    public class DetailsModel : PageModel
    {
        private readonly ShopService _shopService;
        public DetailsModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        public DonHang? Order { get; set; }
        public async Task OnGetAsync(string id)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _shopService.GetOrderByIdAsync(id);
            if (order == null || order.MaKhachHang != customerId)
            {
                Order = null;
                return;
            }
            Order = order;
        }
    }
} 