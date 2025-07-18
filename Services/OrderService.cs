using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DonHang>> GetAllAsync()
        {
            return await _context.DonHangs.Include(d => d.ChiNhanh).Include(d => d.KhachHang).ToListAsync();
        }

        public async Task<DonHang?> GetByIdAsync(string id)
        {
            return await _context.DonHangs
                .Include(d => d.ChiNhanh)
                .Include(d => d.KhachHang)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.MonAn)
                .FirstOrDefaultAsync(d => d.MaDonHang == id);
        }

        public async Task<bool> CreateAsync(DonHang order)
        {
            _context.DonHangs.Add(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DonHang order)
        {
            _context.DonHangs.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var order = await _context.DonHangs.FindAsync(id);
            if (order == null) return false;
            _context.DonHangs.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DonHang>> GetByKhachHangIdAsync(string maKhachHang)
        {
            return await _context.DonHangs
                .Include(d => d.ChiNhanh)
                .Include(d => d.KhachHang)
                .Where(d => d.MaKhachHang == maKhachHang)
                .OrderByDescending(d => d.NgayDatHang)
                .ToListAsync();
        }
    }
} 