using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class EditModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        public EditModel(DanhMucService danhMucService)
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _danhMucService.UpdateAsync(Category);
            if (result)
            {
                TempData["Success"] = "Cập nhật danh mục thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể cập nhật danh mục.");
            return Page();
        }
    }
} 