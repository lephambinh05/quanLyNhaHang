using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NhaHang.Pages.Marketing
{
    public class EditModel : PageModel
    {
        private readonly MarketingService _marketingService;
        private readonly BranchService _branchService;
        public List<ChiNhanh> Branches { get; set; } = new();
        public EditModel(MarketingService marketingService, BranchService branchService)
        {
            _marketingService = marketingService;
            _branchService = branchService;
        }
        [BindProperty]
        public KhuyenMai Promotion { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var promo = await _marketingService.GetByIdAsync(id);
            if (promo == null) return NotFound();
            Promotion = promo;
            Branches = await _branchService.GetAllAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Branches = await _branchService.GetAllAsync();
            if (!ModelState.IsValid) return Page();
            var result = await _marketingService.UpdateAsync(Promotion);
            if (result)
            {
                TempData["Success"] = "Cập nhật khuyến mãi thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể cập nhật khuyến mãi.");
            return Page();
        }
    }
} 