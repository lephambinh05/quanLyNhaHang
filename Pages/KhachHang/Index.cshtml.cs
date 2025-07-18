using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.KhachHangPages
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly KhachHangService _khachHangService;
        public IndexModel(KhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        public List<KhachHang> Customers { get; set; } = new();
        public async Task OnGetAsync()
        {
            Customers = await _khachHangService.GetAllAsync();
        }
    }
} 