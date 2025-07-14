using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Marketing
{
    public class EditModel : PageModel
    {
        private readonly MarketingService _marketingService;
        public EditModel(MarketingService marketingService)
        {
            _marketingService = marketingService;
        }
        [BindProperty]
        public KhuyenMai Promotion { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var promo = await _marketingService.GetByIdAsync(id);
            if (promo == null) return NotFound();
            Promotion = promo;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _marketingService.UpdateAsync(Promotion);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể cập nhật khuyến mãi.");
            return Page();
        }
    }
} 