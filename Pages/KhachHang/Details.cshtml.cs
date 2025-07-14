using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.KhachHangPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class DetailsModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public DetailsModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        public KhachHang? Customer { get; set; }
        public async Task OnGetAsync(string id)
        {
            Customer = await _khachHangService.GetByIdAsync(id);
        }
    }
} 