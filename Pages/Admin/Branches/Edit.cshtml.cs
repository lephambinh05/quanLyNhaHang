using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Branches
{
    public class EditModel : PageModel
    {
        private readonly BranchService _branchService;
        public EditModel(BranchService branchService)
        {
            _branchService = branchService;
        }
        [BindProperty]
        public ChiNhanh Branch { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound();
            Branch = branch;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _branchService.UpdateAsync(Branch);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể cập nhật chi nhánh.");
            return Page();
        }
    }
} 