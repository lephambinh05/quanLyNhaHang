using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Menu
{
    public class EditModel : PageModel
    {
        private readonly MenuService _menuService;
        public EditModel(MenuService menuService)
        {
            _menuService = menuService;
        }
        [BindProperty]
        public MonAn Dish { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var dish = await _menuService.GetByIdAsync(id);
            if (dish == null) return NotFound();
            Dish = dish;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _menuService.UpdateAsync(Dish);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể cập nhật món ăn.");
            return Page();
        }
    }
} 