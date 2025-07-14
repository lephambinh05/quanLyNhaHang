using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaHang.Models;
using NhaHang.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin.Dashboard
{
    [Authorize(Roles = "SuperAdmin,Manager")]
    public class DashboardModel : PageModel
    {
        private readonly BranchService _branchService;
        private readonly MenuService _menuService;
        private readonly OrderService _orderService;
        private readonly KhachHangService _customerService;
        private readonly MarketingService _marketingService;

        public DashboardModel(
            BranchService branchService,
            MenuService menuService,
            OrderService orderService,
            KhachHangService customerService,
            MarketingService marketingService)
        {
            _branchService = branchService;
            _menuService = menuService;
            _orderService = orderService;
            _customerService = customerService;
            _marketingService = marketingService;
        }

        public int TotalBranches { get; set; }
        public int TotalDishes { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TodayRevenue { get; set; }
        public int TodayOrders { get; set; }
        public int ActivePromotions { get; set; }
        public List<DonHang> RecentOrders { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Lấy thống kê tổng quan
            var branches = await _branchService.GetAllAsync();
            var dishes = await _menuService.GetAllAsync();
            var orders = await _orderService.GetAllAsync();
            var customers = await _customerService.GetAllAsync();
            var promotions = await _marketingService.GetAllAsync();

            TotalBranches = branches.Count;
            TotalDishes = dishes.Count;
            TotalOrders = orders.Count;
            TotalCustomers = customers.Count;
            ActivePromotions = promotions.Count;

            // Lấy đơn hàng gần đây (5 đơn hàng mới nhất)
            RecentOrders = orders.Take(5).ToList();

            // TODO: Tính toán doanh thu hôm nay và đơn hàng hôm nay
            TodayRevenue = 0; // Sẽ tính toán sau
            TodayOrders = 0; // Sẽ tính toán sau
        }
    }
} 