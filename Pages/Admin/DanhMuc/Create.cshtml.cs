using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace NhaHang.Pages.DanhMucPages
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class CreateModel : PageModel
    {
        private readonly DanhMucService _danhMucService;
        private readonly BranchService _branchService;
        public List<SelectListItem> BranchOptions { get; set; } = new();
        public CreateModel(DanhMucService danhMucService, BranchService branchService)
        {
            _danhMucService = danhMucService;
            _branchService = branchService;
        }
        [BindProperty]
        public DanhMuc Category { get; set; } = new();
        public async Task OnGetAsync()
        {
            var branches = await _branchService.GetAllAsync();
            BranchOptions = branches.Select(b => new SelectListItem { Value = b.MaChiNhanh, Text = b.TenChiNhanh }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var branches = await _branchService.GetAllAsync();
            BranchOptions = branches.Select(b => new SelectListItem { Value = b.MaChiNhanh, Text = b.TenChiNhanh }).ToList();
            if (!ModelState.IsValid) return Page();
            // Tự sinh mã danh mục (ví dụ: DM + 6 ký tự số ngẫu nhiên)
            Category.MaDanhMuc = $"DM{Guid.NewGuid().ToString("N").Substring(0,6).ToUpper()}";
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