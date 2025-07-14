using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NhaHang.Pages.Branches
{
    public class CreateModel : PageModel
    {
        private readonly BranchService _branchService;
        private readonly ImageService _imageService;
        public CreateModel(BranchService branchService, ImageService imageService)
        {
            _branchService = branchService;
            _imageService = imageService;
        }
        [BindProperty]
        public ChiNhanh Branch { get; set; } = new();
        [BindProperty]
        public IFormFile? AvatarFile { get; set; }
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (AvatarFile != null)
            {
                var fileName = await _imageService.SaveImageAsync(AvatarFile);
                if (!string.IsNullOrEmpty(fileName))
                    Branch.HinhAnh = fileName;
            }
            if (!ModelState.IsValid) return Page();
            var result = await _branchService.CreateAsync(Branch);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể thêm chi nhánh.");
            return Page();
        }
    }
} 