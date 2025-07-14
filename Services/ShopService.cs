using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class ShopService
    {
        private readonly ApplicationDbContext _context;
        public ShopService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DonHang>> GetAllOrdersAsync(string? customerId = null)
        {
            var query = _context.DonHangs.Include(d => d.KhachHang).Include(d => d.ChiNhanh).AsQueryable();
            if (!string.IsNullOrEmpty(customerId))
                query = query.Where(d => d.MaKhachHang == customerId);
            return await query.ToListAsync();
        }

        public async Task<DonHang?> GetOrderByIdAsync(string id)
        {
            return await _context.DonHangs.Include(d => d.KhachHang).Include(d => d.ChiNhanh).FirstOrDefaultAsync(d => d.MaDonHang == id);
        }

        public async Task<bool> CreateOrderAsync(DonHang order)
        {
            _context.DonHangs.Add(order);
            return await _context.SaveChangesAsync() > 0;
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
    }
} 