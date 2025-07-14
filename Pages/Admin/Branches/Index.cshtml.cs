using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Branches
{
    public class IndexModel : PageModel
    {
        private readonly BranchService _branchService;
        public IndexModel(BranchService branchService)
        {
            _branchService = branchService;
        }
        public List<ChiNhanh> Branches { get; set; } = new();
        public async Task OnGetAsync()
        {
            Branches = await _branchService.GetAllAsync();
        }
    }
} 