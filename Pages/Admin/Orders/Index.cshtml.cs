using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace NhaHang.Pages.Orders
{
    [Authorize(AuthenticationSchemes = "AdminCookie", Roles = "SuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly ShopService _shopService;
        
        public IndexModel(ShopService shopService)
        {
            _shopService = shopService;
        }
        
        public List<DonHang> Orders { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? Date { get; set; }
        
        public async Task OnGetAsync()
        {
            var allOrders = await _shopService.GetAllOrdersAsync();
            foreach (var order in allOrders)
            {
                if (order.PhuongThucThanhToan == "Chuyển khoản")
                {
                    if (order.TrangThai == "Đã thanh toán")
                    {
                        // Nếu đã thanh toán chuyển khoản thì tự động xác nhận đơn hàng
                        if (order.TrangThai != "Đã xác nhận")
                        {
                            order.TrangThai = "Đã xác nhận";
                            await _shopService.UpdateOrderAsync(order);
                        }
                    }
                    else if (order.TrangThai == "Chờ xác nhận")
                    {
                        order.TrangThai = "Chờ thanh toán - Chờ xác nhận";
                        await _shopService.UpdateOrderAsync(order);
                    }
                }
                else if (order.PhuongThucThanhToan == "Tiền mặt")
                {
                    if (order.TrangThai == "Chờ xác nhận")
                    {
                        order.TrangThai = "Đã xác nhận";
                        await _shopService.UpdateOrderAsync(order);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Status))
                allOrders = allOrders.Where(o => o.TrangThai == Status).ToList();
            if (!string.IsNullOrEmpty(Search))
                allOrders = allOrders.Where(o =>
                    (o.MaDonHang != null && o.MaDonHang.Contains(Search, StringComparison.OrdinalIgnoreCase)) ||
                    (o.TenKhachHang != null && o.TenKhachHang.Contains(Search, StringComparison.OrdinalIgnoreCase)) ||
                    (o.SoDienThoai != null && o.SoDienThoai.Contains(Search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            if (Date.HasValue)
                allOrders = allOrders.Where(o => o.NgayDatHang.Date == Date.Value.Date).ToList();
            Orders = allOrders;
        }

        public async Task<IActionResult> OnPostConfirmAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var order = await _shopService.GetOrderByIdAsync(id);
                if (order != null && order.TrangThai == "Chờ xác nhận")
                {
                    order.TrangThai = "Đang chế biến";
                    await _shopService.UpdateOrderAsync(order);
                    TempData["Success"] = $"Đã xác nhận đơn hàng {id}.";
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCancelAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var order = await _shopService.GetOrderByIdAsync(id);
                if (order != null && order.TrangThai == "Chờ xác nhận")
                {
                    order.TrangThai = "Hủy";
                    await _shopService.UpdateOrderAsync(order);
                    TempData["Success"] = $"Đã hủy đơn hàng {id}.";
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportAsync()
        {
            var allOrders = await _shopService.GetAllOrdersAsync();
            if (!string.IsNullOrEmpty(Status))
                allOrders = allOrders.Where(o => o.TrangThai == Status).ToList();
            if (!string.IsNullOrEmpty(Search))
                allOrders = allOrders.Where(o =>
                    (o.MaDonHang != null && o.MaDonHang.Contains(Search, StringComparison.OrdinalIgnoreCase)) ||
                    (o.TenKhachHang != null && o.TenKhachHang.Contains(Search, StringComparison.OrdinalIgnoreCase)) ||
                    (o.SoDienThoai != null && o.SoDienThoai.Contains(Search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            if (Date.HasValue)
                allOrders = allOrders.Where(o => o.NgayDatHang.Date == Date.Value.Date).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("Mã đơn,Khách hàng,Số điện thoại,Chi nhánh,Ngày đặt,Trạng thái,Tổng tiền");
            foreach (var order in allOrders)
            {
                var total = order.ChiTietDonHangs?.Sum(ct => ct.SoLuong * ct.DonGia) ?? 0;
                sb.AppendLine($"{order.MaDonHang},{order.TenKhachHang},{order.SoDienThoai},{order.ChiNhanh?.TenChiNhanh},{order.NgayDatHang:dd/MM/yyyy HH:mm},{order.TrangThai},{total}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"donhang_{DateTime.Now:yyyyMMddHHmmss}.csv");
        }
    }
} 