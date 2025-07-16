using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class MarketingService
    {
        private readonly ApplicationDbContext _context;
        public MarketingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhuyenMai>> GetAllAsync()
        {
            return await _context.KhuyenMais.Include(k => k.ChiNhanh).ToListAsync();
        }

        public async Task<KhuyenMai?> GetByIdAsync(string id)
        {
            return await _context.KhuyenMais.Include(k => k.ChiNhanh).FirstOrDefaultAsync(k => k.MaKhuyenMai == id);
        }

        public async Task<bool> CreateAsync(KhuyenMai promo)
        {
            // Sinh mã tự động: KM + số thứ tự tăng dần
            var last = await _context.KhuyenMais.OrderByDescending(k => k.MaKhuyenMai).FirstOrDefaultAsync();
            int nextNumber = 1;
            if (last != null && last.MaKhuyenMai.Length > 2 && int.TryParse(last.MaKhuyenMai.Substring(2), out int lastNum))
            {
                nextNumber = lastNum + 1;
            }
            promo.MaKhuyenMai = $"KM{nextNumber:D2}";
            _context.KhuyenMais.Add(promo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(KhuyenMai promo)
        {
            _context.KhuyenMais.Update(promo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var promo = await _context.KhuyenMais.FindAsync(id);
            if (promo == null) return false;
            _context.KhuyenMais.Remove(promo);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 