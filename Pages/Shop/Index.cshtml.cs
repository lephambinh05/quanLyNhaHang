using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NhaHang.Pages.Shop
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly ShopService _shopService;
        public IndexModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        public List<DonHang> Orders { get; set; } = new();
        public async Task OnGetAsync()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Orders = await _shopService.GetAllOrdersAsync(customerId);
        }
    }
} 