using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Models;
using NhaHang.Services;
using System.Threading.Tasks;

namespace NhaHang.Pages.Shop
{
    public class ThanhToanChuyenKhoanModel : PageModel
    {
        private readonly ShopService _shopService;
        public string MaDonHang { get; set; } = string.Empty;
        public DonHang? DonHang { get; set; }
        public bool DaThanhToan { get; set; } = false;
        public string? ThongBaoThanhToan { get; set; }
        public ThanhToanChuyenKhoanModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            MaDonHang = id;
            DonHang = await _shopService.GetOrderByIdAsync(id);
            if (DonHang == null) return NotFound();
            // Kiểm tra trạng thái thanh toán
            if (DonHang.TrangThai == "Đã thanh toán")
            {
                DaThanhToan = true;
                ThongBaoThanhToan = "Đơn hàng đã được xác nhận thanh toán.";
            }
            else
            {
                // Gọi API check giao dịch
                var daThanhToan = await _shopService.CheckAndMarkPaymentAsync(id);
                if (daThanhToan)
                {
                    DaThanhToan = true;
                    ThongBaoThanhToan = "Đã phát hiện giao dịch chuyển khoản hợp lệ. Đơn hàng đã được xác nhận thanh toán.";
                }
                else
                {
                    ThongBaoThanhToan = "Chưa phát hiện giao dịch chuyển khoản hợp lệ. Vui lòng kiểm tra lại sau vài phút.";
                }
            }
            return Page();
        }
    }
} 