using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NhaHang.Services
{
    public class ShopService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShopService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DonHang>> GetAllOrdersAsync(string? customerId = null)
        {
            var query = _context.DonHangs
                .Include(d => d.KhachHang)
                .Include(d => d.ChiNhanh)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.MonAn)
                .AsQueryable();
            if (!string.IsNullOrEmpty(customerId))
                query = query.Where(d => d.MaKhachHang == customerId);
            return await query.ToListAsync();
        }

        public async Task<DonHang?> GetOrderByIdAsync(string id)
        {
            return await _context.DonHangs
                .Include(d => d.KhachHang)
                .Include(d => d.ChiNhanh)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.MonAn)
                .FirstOrDefaultAsync(d => d.MaDonHang == id);
        }

        public async Task<bool> CreateOrderAsync(DonHang order, List<Models.CartItem> cartItems)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Tạo mã đơn hàng tự động
                order.MaDonHang = await GenerateOrderIdAsync();
                
                _context.DonHangs.Add(order);
                await _context.SaveChangesAsync();

                // Lấy mã chi tiết cuối cùng 1 lần
                string lastId = await GetLastOrderDetailIdAsync();
                int lastNumber = lastId == null ? 0 : int.Parse(lastId.Substring(2));

                // Tạo chi tiết đơn hàng
                foreach (var item in cartItems)
                {
                    lastNumber++;
                    var chiTiet = new ChiTietDonHang
                    {
                        MaChiTietDonHang = $"CT{lastNumber:D8}",
                        MaDonHang = order.MaDonHang,
                        MaMonAn = item.MaMonAn,
                        SoLuong = item.SoLuong,
                        DonGia = item.DonGia,
                        MucCay = item.MucCay
                    };
                    _context.ChiTietDonHangs.Add(chiTiet);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateOrderAsync ERROR] {ex}");
                await transaction.RollbackAsync();
                // Ghi chi tiết lỗi vào HttpContext nếu có
                var httpContext = _httpContextAccessor?.HttpContext;
                if (httpContext != null)
                {
                    httpContext.Items["ErrorDetail"] = ex.ToString();
                }
                return false;
            }
        }

        public async Task<bool> UpdateOrderAsync(DonHang order)
        {
            _context.DonHangs.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var order = await _context.DonHangs.FindAsync(id);
            if (order == null) return false;
            _context.DonHangs.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> GenerateOrderIdAsync()
        {
            var lastOrder = await _context.DonHangs
                .OrderByDescending(o => o.MaDonHang)
                .FirstOrDefaultAsync();

            if (lastOrder == null)
                return "DH00000001";

            var lastNumber = int.Parse(lastOrder.MaDonHang.Substring(2));
            return $"DH{(lastNumber + 1):D8}";
        }

        private async Task<string> GetLastOrderDetailIdAsync()
        {
            var lastDetail = await _context.ChiTietDonHangs
                .OrderByDescending(ct => ct.MaChiTietDonHang)
                .FirstOrDefaultAsync();
            return lastDetail?.MaChiTietDonHang;
        }
    }


} 