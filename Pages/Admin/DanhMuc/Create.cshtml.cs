using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class CreateModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        public CreateModel(DanhMucService danhMucService)
        {
            _danhMucService = danhMucService;
        }
        [BindProperty]
        public DanhMuc Category { get; set; } = new();
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _danhMucService.CreateAsync(Category);
            if (result)
            {
                TempData["Success"] = "Thêm danh mục thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể thêm danh mục.");
            return Page();
        }
    }
} 