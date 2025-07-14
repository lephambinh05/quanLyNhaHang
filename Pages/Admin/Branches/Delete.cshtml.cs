using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Branches
{
    public class DeleteModel : PageModel
    {
        private readonly BranchService _branchService;
        public DeleteModel(BranchService branchService)
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
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _branchService.DeleteAsync(id);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể xóa chi nhánh.");
            return Page();
        }
    }
} 