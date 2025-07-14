using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class DeleteModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        public DeleteModel(DanhMucService danhMucService)
        {
            _danhMucService = danhMucService;
        }
        [BindProperty]
        public DanhMuc Category { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var category = await _danhMucService.GetByIdAsync(id);
            if (category == null) return NotFound();
            Category = category;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _danhMucService.DeleteAsync(id);
            if (result)
            {
                TempData["Success"] = "Xóa danh mục thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa danh mục.");
            return Page();
        }
    }
} 