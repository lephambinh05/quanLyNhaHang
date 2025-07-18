using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NhaHang.Pages.Admin.QuanTriVien
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly QuanTriVienService _service;
        private readonly BranchService _branchService;
        public List<SelectListItem> BranchOptions { get; set; } = new();
        public CreateModel(QuanTriVienService service, BranchService branchService)
        {
            _service = service;
            _branchService = branchService;
        }
        [BindProperty]
        public NhaHang.Models.QuanTriVien Admin { get; set; } = new();
        public async Task OnGetAsync()
        {
            var branches = await _branchService.GetAllAsync();
            BranchOptions = branches.Select(b => new SelectListItem { Value = b.MaChiNhanh, Text = b.TenChiNhanh }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Tự sinh mã quản trị viên (ví dụ: QTV + 6 ký tự số ngẫu nhiên)
            Admin.MaQuanTriVien = $"QTV{Guid.NewGuid().ToString("N").Substring(0,6).ToUpper()}";
            var branches = await _branchService.GetAllAsync();
            BranchOptions = branches.Select(b => new SelectListItem { Value = b.MaChiNhanh, Text = b.TenChiNhanh }).ToList();
            if (!ModelState.IsValid) return Page();
            var result = await _service.CreateAsync(Admin);
            if (result)
            {
                TempData["Success"] = "Thêm tài khoản quản trị thành công!";
                return RedirectToPage("Index");
            }
            ModelState.AddModelError(string.Empty, "Không thể thêm tài khoản quản trị.");
            return Page();
        }
    }
} 