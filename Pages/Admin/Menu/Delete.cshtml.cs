using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NhaHang.Pages.Menu
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly MenuService _menuService;
        public DeleteModel(MenuService menuService)
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
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _menuService.DeleteAsync(id);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể xóa món ăn.");
            return Page();
        }
    }
} 