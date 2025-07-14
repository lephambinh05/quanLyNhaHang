using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly OrderService _orderService;
        public CreateModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DonHang Order { get; set; } = new();
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var result = await _orderService.CreateAsync(Order);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể thêm đơn hàng.");
            return Page();
        }
    }
} 