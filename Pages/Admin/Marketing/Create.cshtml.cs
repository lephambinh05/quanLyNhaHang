using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NhaHang.Pages.Marketing
{
    public class CreateModel : PageModel
    {
        private readonly MarketingService _marketingService;
        private readonly ImageService _imageService;
        public CreateModel(MarketingService marketingService, ImageService imageService)
        {
            _marketingService = marketingService;
            _imageService = imageService;
        }
        [BindProperty]
        public KhuyenMai Promotion { get; set; } = new();
        [BindProperty]
        public IFormFile? BannerFile { get; set; }
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (BannerFile != null)
            {
                var fileName = await _imageService.SaveImageAsync(BannerFile);
                if (!string.IsNullOrEmpty(fileName))
                    Promotion.HinhAnhBanner = fileName;
            }
            if (!ModelState.IsValid) return Page();
            var result = await _marketingService.CreateAsync(Promotion);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể thêm khuyến mãi.");
            return Page();
        }
    }
} 