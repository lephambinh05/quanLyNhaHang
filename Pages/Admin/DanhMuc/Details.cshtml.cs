using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class DetailsModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        public DetailsModel(DanhMucService danhMucService)
        {
            _danhMucService = danhMucService;
        }
        public DanhMuc? Category { get; set; }
        public async Task OnGetAsync(string id)
        {
            Category = await _danhMucService.GetByIdAsync(id);
        }
    }
} 