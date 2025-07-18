using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using NhaHang.Models;
using NhaHang.Data;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Services;

namespace NhaHang.Pages
{
    public class MenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public MenuModel(ApplicationDbContext context)
        {
            _context = context;
            Search = "";
            Dishes = new List<MonAn>();
        }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; } = "";
        public List<MonAn> Dishes { get; set; } = new();
        public void OnGet()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewData["CartCount"] = cart.Count;
            var query = _context.MonAns.AsQueryable();
            if (!string.IsNullOrWhiteSpace(Search))
            {
                var keyword = Search.ToLower();
                query = query.Where(m => m.TenMonAn.ToLower().Contains(keyword) || (m.MoTa != null && m.MoTa.ToLower().Contains(keyword)));
            }
            Dishes = query.Where(m => m.CoSan).OrderBy(m => m.TenMonAn).ToList();
        }
        public IActionResult OnPostAddToCart(string maMonAn)
        {
            var dish = _context.MonAns.FirstOrDefault(m => m.MaMonAn == maMonAn && m.CoSan);
            if (dish == null)
            {
                TempData["Error"] = "Món ăn không tồn tại hoặc đã hết!";
                return RedirectToPage(new { search = Search });
            }
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(item => item.MaMonAn == maMonAn);
            if (existingItem != null)
                existingItem.SoLuong++;
            else
                cart.Add(new CartItem
                {
                    MaMonAn = dish.MaMonAn,
                    TenMonAn = dish.TenMonAn,
                    SoLuong = 1,
                    DonGia = dish.Gia,
                    MucCay = dish.MucCay
                });
            HttpContext.Session.Set("Cart", cart);
            ViewData["CartCount"] = cart.Count;
            TempData["Success"] = $"Đã thêm {dish.TenMonAn} vào giỏ hàng!";
            return RedirectToPage(new { search = Search });
        }
    }
} 