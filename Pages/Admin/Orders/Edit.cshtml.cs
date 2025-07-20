using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;
using System.Linq;

namespace NhaHang.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly ShopService _shopService;
        
        public EditModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        
        [BindProperty]
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
            
            // Đồng bộ trạng thái như ở danh sách/chi tiết
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
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (Order == null)
            {
                return RedirectToPage("Index");
            }

            if (!ModelState.IsValid)
            {
                // Ghi log lỗi chi tiết vào TempData
                TempData["ErrorDetails"] = string.Join("; ", ModelState.Where(x => x.Value.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}"));
                // Reload order from DB to repopulate fields, keep selected TrangThai
                var orderFromDb = await _shopService.GetOrderByIdAsync(Order.MaDonHang);
                if (orderFromDb != null)
                {
                    orderFromDb.TrangThai = Order.TrangThai;
                    Order = orderFromDb;
                }
                return Page();
            }

            var result = await _shopService.UpdateOrderAsync(Order);
            if (result)
            {
                TempData["Success"] = "Cập nhật trạng thái đơn hàng thành công!";
                return RedirectToPage("Index");
            }

            // Reload order from DB if update fails
            var orderReload = await _shopService.GetOrderByIdAsync(Order.MaDonHang);
            if (orderReload != null)
            {
                orderReload.TrangThai = Order.TrangThai;
                Order = orderReload;
            }
            ModelState.AddModelError(string.Empty, "Không thể cập nhật đơn hàng.");
            return Page();
        }
    }
} 