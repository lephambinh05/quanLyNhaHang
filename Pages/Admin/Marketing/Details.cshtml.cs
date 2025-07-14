using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Marketing
{
    public class DetailsModel : PageModel
    {
        private readonly MarketingService _marketingService;
        public DetailsModel(MarketingService marketingService)
        {
            _marketingService = marketingService;
        }
        public KhuyenMai? Promotion { get; set; }
        public async Task OnGetAsync(string id)
        {
            Promotion = await _marketingService.GetByIdAsync(id);
        }
    }
} 