using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Menu
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly MenuService _menuService;

        public IndexModel(MenuService menuService)
        {
            _menuService = menuService;
        }

        public List<MonAn> Dishes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Dishes = await _menuService.GetAllAsync();
        }
    }
} 