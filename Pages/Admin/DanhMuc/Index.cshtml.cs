using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class IndexModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        public IndexModel(DanhMucService danhMucService)
        {
            _danhMucService = danhMucService;
        }
        public List<DanhMuc> Categories { get; set; } = new();
        public async Task OnGetAsync()
        {
            Categories = await _danhMucService.GetAllAsync();
        }
    }
} 