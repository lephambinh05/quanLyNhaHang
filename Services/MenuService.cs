using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;
using System.Linq;

namespace NhaHang.Services
{
    public class MenuService
    {
        private readonly ApplicationDbContext _context;
        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonAn>> GetAllAsync()
        {
            return await _context.MonAns.Include(m => m.DanhMuc).Include(m => m.ChiNhanh).ToListAsync();
        }

        public async Task<MonAn?> GetByIdAsync(string id)
        {
            return await _context.MonAns.Include(m => m.DanhMuc).Include(m => m.ChiNhanh).FirstOrDefaultAsync(m => m.MaMonAn == id);
        }

        public async Task<bool> CreateAsync(MonAn dish)
        {
            _context.MonAns.Add(dish);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(MonAn dish)
        {
            _context.MonAns.Update(dish);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var dish = await _context.MonAns.FindAsync(id);
            if (dish == null) return false;
            // Xóa tất cả các bản ghi ChiTietDonHang liên quan
            var chiTietList = await _context.ChiTietDonHangs.Where(c => c.MaMonAn == id).ToListAsync();
            if (chiTietList.Count > 0)
                _context.ChiTietDonHangs.RemoveRange(chiTietList);
            _context.MonAns.Remove(dish);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 