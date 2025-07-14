using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Branches
{
    public class DetailsModel : PageModel
    {
        private readonly BranchService _branchService;
        public DetailsModel(BranchService branchService)
        {
            _branchService = branchService;
        }
        public ChiNhanh? Branch { get; set; }
        public async Task OnGetAsync(string id)
        {
            Branch = await _branchService.GetByIdAsync(id);
        }
    }
} 