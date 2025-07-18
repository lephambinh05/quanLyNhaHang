using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Pages.Shop
{
    public class OrderInputModel
    {
        [Required, StringLength(100)]
        public string TenKhachHang { get; set; } = "";
        [Required, StringLength(20)]
        public string SoDienThoai { get; set; } = "";
        [Required, StringLength(255)]
        public string DiaChiGiaoHang { get; set; } = "";
        [Required, StringLength(10)]
        public string MaChiNhanh { get; set; } = "";
        [StringLength(255)]
        public string? GhiChu { get; set; }

        // Thêm phương thức thanh toán
        [Required]
        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; } = "TienMat";
    }

    public class CreateModel : PageModel
    {
        private readonly ShopService _shopService;
        private readonly MenuService _menuService;
        private readonly BranchService _branchService;

        public CreateModel(ShopService shopService, MenuService menuService, BranchService branchService)
        {
            _shopService = shopService;
            _menuService = menuService;
            _branchService = branchService;
        }

        [BindProperty]
        public OrderInputModel Order { get; set; } = new();

        [BindProperty]
        public List<CartItem> CartItems { get; set; } = new();

        public List<MonAn> AvailableDishes { get; set; } = new();
        public List<ChiNhanh> Branches { get; set; } = new();
        public bool ShowOrderFormErrors { get; set; } = false;

        public async Task OnGetAsync()
        {
            AvailableDishes = await _menuService.GetAllAsync();
            Branches = await _branchService.GetAllAsync();
            CartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewData["CartCount"] = CartItems.Count;
            // Pre-fill thông tin nếu đã đăng nhập
            if (User.Identity.IsAuthenticated)
            {
                var customerName = User.FindFirstValue(ClaimTypes.Name);
                var customerPhone = User.FindFirstValue("PhoneNumber");
                if (!string.IsNullOrEmpty(customerName))
                    Order.TenKhachHang = customerName;
                if (!string.IsNullOrEmpty(customerPhone))
                    Order.SoDienThoai = customerPhone;
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string maMonAn)
        {
            var dish = await _menuService.GetByIdAsync(maMonAn);
            if (dish == null) return RedirectToPage();
            AvailableDishes = await _menuService.GetAllAsync();
            Branches = await _branchService.GetAllAsync();
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cartItems.FirstOrDefault(item => item.MaMonAn == maMonAn);
            if (existingItem != null)
                existingItem.SoLuong++;
            else
                cartItems.Add(new CartItem
                {
                    MaMonAn = dish.MaMonAn,
                    TenMonAn = dish.TenMonAn,
                    SoLuong = 1,
                    DonGia = dish.Gia,
                    MucCay = dish.MucCay
                });
            HttpContext.Session.Set("Cart", cartItems);
            CartItems = cartItems;
            ViewData["CartCount"] = CartItems.Count;
            // Pre-fill nếu đã đăng nhập
            if (User.Identity.IsAuthenticated)
            {
                var customerName = User.FindFirstValue(ClaimTypes.Name);
                var customerPhone = User.FindFirstValue("PhoneNumber");
                if (!string.IsNullOrEmpty(customerName))
                    Order.TenKhachHang = customerName;
                if (!string.IsNullOrEmpty(customerPhone))
                    Order.SoDienThoai = customerPhone;
            }
            ShowOrderFormErrors = false;
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(string maMonAn)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            cartItems.RemoveAll(item => item.MaMonAn == maMonAn);
            HttpContext.Session.Set("Cart", cartItems);
            AvailableDishes = await _menuService.GetAllAsync();
            Branches = await _branchService.GetAllAsync();
            CartItems = cartItems;
            ViewData["CartCount"] = CartItems.Count;
            // Pre-fill nếu đã đăng nhập
            if (User.Identity.IsAuthenticated)
            {
                var customerName = User.FindFirstValue(ClaimTypes.Name);
                var customerPhone = User.FindFirstValue("PhoneNumber");
                if (!string.IsNullOrEmpty(customerName))
                    Order.TenKhachHang = customerName;
                if (!string.IsNullOrEmpty(customerPhone))
                    Order.SoDienThoai = customerPhone;
            }
            ShowOrderFormErrors = false;
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateQuantityAsync(string maMonAn, int soLuong)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cartItems.FirstOrDefault(i => i.MaMonAn == maMonAn);
            if (item != null)
            {
                if (soLuong <= 0)
                    cartItems.Remove(item);
                else
                    item.SoLuong = soLuong;
            }
            HttpContext.Session.Set("Cart", cartItems);
            AvailableDishes = await _menuService.GetAllAsync();
            Branches = await _branchService.GetAllAsync();
            CartItems = cartItems;
            // Pre-fill nếu đã đăng nhập
            if (User.Identity.IsAuthenticated)
            {
                var customerName = User.FindFirstValue(ClaimTypes.Name);
                var customerPhone = User.FindFirstValue("PhoneNumber");
                if (!string.IsNullOrEmpty(customerName))
                    Order.TenKhachHang = customerName;
                if (!string.IsNullOrEmpty(customerPhone))
                    Order.SoDienThoai = customerPhone;
            }
            ShowOrderFormErrors = false;
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ShowOrderFormErrors = true;
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (cartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Giỏ hàng trống. Vui lòng chọn món ăn.");
                AvailableDishes = await _menuService.GetAllAsync();
                Branches = await _branchService.GetAllAsync();
                CartItems = cartItems;
                return Page();
            }
            if (!ModelState.IsValid)
            {
                AvailableDishes = await _menuService.GetAllAsync();
                Branches = await _branchService.GetAllAsync();
                CartItems = cartItems;
                return Page();
            }
            // Map lại giá trị phương thức thanh toán cho đúng với database
            string pttt = Order.PhuongThucThanhToan;
            if (pttt == "TienMat") pttt = "Tiền mặt";
            else if (pttt == "ChuyenKhoan") pttt = "Chuyển khoản";
            // Tạo mới DonHang từ ViewModel
            var donHang = new DonHang
            {
                TenKhachHang = Order.TenKhachHang,
                SoDienThoai = Order.SoDienThoai,
                DiaChiGiaoHang = Order.DiaChiGiaoHang,
                MaChiNhanh = Order.MaChiNhanh,
                GhiChu = Order.GhiChu,
                TrangThai = "Chờ xác nhận",
                NgayDatHang = DateTime.Now,
                PhuongThucThanhToan = pttt
            };
            if (User.Identity.IsAuthenticated)
            {
                var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                donHang.MaKhachHang = customerId;
            }
            var result = await _shopService.CreateOrderAsync(donHang, cartItems);
            if (result)
            {
                HttpContext.Session.Remove("Cart");
                TempData["Success"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
                // Nạp lại dữ liệu để hiển thị form rỗng và thông báo thành công
                AvailableDishes = await _menuService.GetAllAsync();
                Branches = await _branchService.GetAllAsync();
                CartItems = new List<CartItem>();
                Order = new OrderInputModel();
                ShowOrderFormErrors = false;
                return Page();
            }
            // Nếu có lỗi chi tiết từ ShopService, hiển thị lên giao diện
            if (HttpContext.Items["ErrorDetail"] is string errorDetail && !string.IsNullOrWhiteSpace(errorDetail))
            {
                ModelState.AddModelError(string.Empty, "Chi tiết lỗi: " + errorDetail);
            }
            ModelState.AddModelError(string.Empty, "Không thể đặt hàng. Vui lòng thử lại.");
            AvailableDishes = await _menuService.GetAllAsync();
            Branches = await _branchService.GetAllAsync();
            CartItems = cartItems;
            return Page();
        }
    }
} 