using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NhaHang.Pages.Menu
{
    public class CreateModel : PageModel
    {
        private readonly MenuService _menuService;
        private readonly ImageService _imageService;
        public CreateModel(MenuService menuService, ImageService imageService)
        {
            _menuService = menuService;
            _imageService = imageService;
        }
        [BindProperty]
        public MonAn Dish { get; set; } = new();
        [BindProperty]
        public IFormFile? ImageFile { get; set; }
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile != null)
            {
                var fileName = await _imageService.SaveImageAsync(ImageFile);
                if (!string.IsNullOrEmpty(fileName))
                    Dish.HinhAnh = fileName;
            }
            if (!ModelState.IsValid) return Page();
            var result = await _menuService.CreateAsync(Dish);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể thêm món ăn.");
            return Page();
        }
    }
} 