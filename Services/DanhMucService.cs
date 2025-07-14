using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class DanhMucService
    {
        private readonly ApplicationDbContext _context;
        public DanhMucService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DanhMuc>> GetAllAsync()
        {
            return await _context.DanhMucs.Include(d => d.ChiNhanh).ToListAsync();
        }

        public async Task<DanhMuc?> GetByIdAsync(string id)
        {
            return await _context.DanhMucs.Include(d => d.ChiNhanh).FirstOrDefaultAsync(d => d.MaDanhMuc == id);
        }

        public async Task<bool> CreateAsync(DanhMuc category)
        {
            _context.DanhMucs.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DanhMuc category)
        {
            _context.DanhMucs.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var category = await _context.DanhMucs.FindAsync(id);
            if (category == null) return false;
            _context.DanhMucs.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 