using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly BranchService _branchService;
        public IndexModel(BranchService branchService)
        {
            _branchService = branchService;
        }
        public List<ChiNhanh> ChiNhanhs { get; set; } = new();
        public async Task OnGetAsync()
        {
            ChiNhanhs = await _branchService.GetAllAsync();
        }
    }
} 