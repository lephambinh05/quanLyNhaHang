using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NhaHang.Models;
using NhaHang.Services;
using System.Security.Claims;

namespace nhaHang.Pages.DonHangCuaToi
{
    public class DonHangCuaToiModel : PageModel
    {
        private readonly OrderService _orderService;
        public List<DonHang> DonHangs { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? TrangThaiLoc { get; set; }
        public DonHangCuaToiModel(OrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var maKhachHang = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(maKhachHang))
            {
                return RedirectToPage("/Auth/Login");
            }
            var all = await _orderService.GetByKhachHangIdAsync(maKhachHang);
            if (!string.IsNullOrEmpty(TrangThaiLoc))
            {
                DonHangs = all.Where(d => d.TrangThai == TrangThaiLoc).ToList();
            }
            else
            {
                DonHangs = all;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostHuyAsync(string id)
        {
            var maKhachHang = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(maKhachHang))
                return RedirectToPage("/Auth/Login");
            var don = await _orderService.GetByIdAsync(id);
            if (don == null || don.MaKhachHang != maKhachHang || don.TrangThai != "Chờ xác nhận")
                return NotFound();
            don.TrangThai = "Đã hủy";
            await _orderService.UpdateAsync(don);
            return RedirectToPage();
        }
    }
} 