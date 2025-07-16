using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NhaHang.Pages.Menu
{
    public class EditModel : PageModel
    {
        private readonly MenuService _menuService;
        private readonly ImageService _imageService;
        private readonly BranchService _branchService;
        private readonly DanhMucService _danhMucService;
        public List<ChiNhanh> Branches { get; set; } = new();
        public List<DanhMuc> Categories { get; set; } = new();
        public EditModel(MenuService menuService, ImageService imageService, BranchService branchService, DanhMucService danhMucService)
        {
            _menuService = menuService;
            _imageService = imageService;
            _branchService = branchService;
            _danhMucService = danhMucService;
        }
        [BindProperty]
        public MonAn Dish { get; set; } = new();
        [BindProperty]
        public IFormFile? ImageFile { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Branches = await _branchService.GetAllAsync();
            Categories = await _danhMucService.GetAllAsync();
            var dish = await _menuService.GetByIdAsync(id);
            if (dish == null) return NotFound();
            Dish = dish;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Branches = await _branchService.GetAllAsync();
            Categories = await _danhMucService.GetAllAsync();
            if (ImageFile != null)
            {
                var fileName = await _imageService.SaveImageAsync(ImageFile);
                if (!string.IsNullOrEmpty(fileName))
                    Dish.HinhAnh = fileName;
            }
            if (!ModelState.IsValid) return Page();
            var result = await _menuService.UpdateAsync(Dish);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể cập nhật món ăn.");
            return Page();
        }
    }
} 