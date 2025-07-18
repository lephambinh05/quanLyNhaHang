using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NhaHang.Pages.Orders
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ShopService _shopService;
        
        public DetailsModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        
        public DonHang? Order { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("Index");
            }
            
            Order = await _shopService.GetOrderByIdAsync(id);
            
            if (Order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng";
                return RedirectToPage("Index");
            }
            
            return Page();
        }
    }
} 