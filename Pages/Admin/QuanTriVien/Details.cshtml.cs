using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin.QuanTriVien
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly QuanTriVienService _service;
        public DetailsModel(QuanTriVienService service)
        {
            _service = service;
        }
        public NhaHang.Models.QuanTriVien? Admin { get; set; }
        public async Task OnGetAsync(string id)
        {
            Admin = await _service.GetByIdAsync(id);
        }
    }
} 