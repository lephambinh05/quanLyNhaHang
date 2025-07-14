using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class KhachHangService
    {
        private readonly ApplicationDbContext _context;
        public KhachHangService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhachHang>> GetAllAsync()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public async Task<KhachHang?> GetByIdAsync(string id)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(k => k.MaKhachHang == id);
        }

        public async Task<bool> CreateAsync(KhachHang customer)
        {
            _context.KhachHangs.Add(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(KhachHang customer)
        {
            _context.KhachHangs.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var customer = await _context.KhachHangs.FindAsync(id);
            if (customer == null) return false;
            _context.KhachHangs.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 