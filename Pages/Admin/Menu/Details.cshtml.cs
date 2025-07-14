using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Menu
{
    public class DetailsModel : PageModel
    {
        private readonly MenuService _menuService;
        public DetailsModel(MenuService menuService)
        {
            _menuService = menuService;
        }
        public MonAn? Dish { get; set; }
        public async Task OnGetAsync(string id)
        {
            Dish = await _menuService.GetByIdAsync(id);
        }
    }
} 