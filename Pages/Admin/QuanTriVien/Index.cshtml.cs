using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin.QuanTriVien
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly QuanTriVienService _service;
        public IndexModel(QuanTriVienService service)
        {
            _service = service;
        }
        public List<NhaHang.Models.QuanTriVien> Admins { get; set; } = new();
        public async Task OnGetAsync()
        {
            Admins = await _service.GetAllAsync();
        }
    }
} 