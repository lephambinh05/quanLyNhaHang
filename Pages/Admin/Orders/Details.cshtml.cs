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
            // Đồng bộ trạng thái như ở trang danh sách
            if (Order.PhuongThucThanhToan == "Chuyển khoản")
            {
                if (Order.TrangThai == "Đã thanh toán")
                {
                    if (Order.TrangThai != "Đã xác nhận")
                    {
                        Order.TrangThai = "Đã xác nhận";
                        await _shopService.UpdateOrderAsync(Order);
                    }
                }
                else if (Order.TrangThai == "Chờ xác nhận")
                {
                    Order.TrangThai = "Chờ thanh toán - Chờ xác nhận";
                    await _shopService.UpdateOrderAsync(Order);
                }
            }
            else if (Order.PhuongThucThanhToan == "Tiền mặt")
            {
                if (Order.TrangThai == "Chờ xác nhận")
                {
                    Order.TrangThai = "Đã xác nhận";
                    await _shopService.UpdateOrderAsync(Order);
                }
            }
            return Page();
        }
    }
} 