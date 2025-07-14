using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MenuService _menuService;
        private readonly MarketingService _marketingService;

        public IndexModel(MenuService menuService, MarketingService marketingService)
        {
            _menuService = menuService;
            _marketingService = marketingService;
        }

        public List<MonAn> FeaturedDishes { get; set; } = new();
        public List<KhuyenMai> ActivePromotions { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Lấy thực đơn nổi bật (6 món đầu tiên)
            var allDishes = await _menuService.GetAllAsync();
            FeaturedDishes = allDishes.Take(6).ToList();

            // Lấy khuyến mãi đang chạy
            var allPromotions = await _marketingService.GetAllAsync();
            ActivePromotions = allPromotions.Take(3).ToList();
        }
    }
}
