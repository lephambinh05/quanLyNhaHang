using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Marketing
{
    public class IndexModel : PageModel
    {
        private readonly MarketingService _marketingService;
        public IndexModel(MarketingService marketingService)
        {
            _marketingService = marketingService;
        }
        public List<KhuyenMai> Promotions { get; set; } = new();
        public async Task OnGetAsync()
        {
            Promotions = await _marketingService.GetAllAsync();
        }
    }
} 