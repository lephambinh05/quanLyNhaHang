using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NhaHang.Pages.Orders
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly OrderService _orderService;
        public DeleteModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DonHang Order { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            Order = order;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _orderService.DeleteAsync(id);
            if (result)
                return RedirectToPage("Index");
            ModelState.AddModelError(string.Empty, "Không thể xóa đơn hàng.");
            return Page();
        }
    }
} 