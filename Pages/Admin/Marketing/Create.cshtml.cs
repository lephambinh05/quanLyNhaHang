using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NhaHang.Pages.Marketing
{
    public class CreateModel : PageModel
    {
        private readonly MarketingService _marketingService;
        private readonly ImageService _imageService;
        private readonly BranchService _branchService;
        public List<ChiNhanh> Branches { get; set; } = new();
        public CreateModel(MarketingService marketingService, ImageService imageService, BranchService branchService)
        {
            _marketingService = marketingService;
            _imageService = imageService;
            _branchService = branchService;
        }
        [BindProperty]
        public KhuyenMai Promotion { get; set; } = new();
        [BindProperty]
        public IFormFile? BannerFile { get; set; }
        public async Task OnGetAsync()
        {
            Branches = await _branchService.GetAllAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (BannerFile != null)
            {
                var fileName = await _imageService.SaveImageAsync(BannerFile);
                if (!string.IsNullOrEmpty(fileName))
                    Promotion.HinhAnhBanner = fileName;
            }
            if (string.IsNullOrEmpty(Promotion.MaChiNhanh))
            {
                Promotion.MaChiNhanh = "ALL";
            }
            Branches = await _branchService.GetAllAsync();
            if (!ModelState.IsValid) return Page();
            var result = await _marketingService.CreateAsync(Promotion);
            if (result)
            {
                TempData["Success"] = "Thêm khuyến mãi thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể thêm khuyến mãi.");
            return Page();
        }
    }
} 