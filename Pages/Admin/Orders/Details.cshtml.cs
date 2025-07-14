using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly OrderService _orderService;
        public DetailsModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        public DonHang? Order { get; set; }
        public async Task OnGetAsync(string id)
        {
            Order = await _orderService.GetByIdAsync(id);
        }
    }
} 