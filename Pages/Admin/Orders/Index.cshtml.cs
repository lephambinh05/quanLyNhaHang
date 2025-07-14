using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly OrderService _orderService;
        public IndexModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        public List<DonHang> Orders { get; set; } = new();
        public async Task OnGetAsync()
        {
            Orders = await _orderService.GetAllAsync();
        }
    }
} 