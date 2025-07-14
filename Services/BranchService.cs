using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class BranchService
    {
        private readonly ApplicationDbContext _context;
        public BranchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChiNhanh>> GetAllAsync()
        {
            return await _context.ChiNhanhs.ToListAsync();
        }

        public async Task<ChiNhanh?> GetByIdAsync(string id)
        {
            return await _context.ChiNhanhs.FindAsync(id);
        }

        public async Task<bool> CreateAsync(ChiNhanh branch)
        {
            _context.ChiNhanhs.Add(branch);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ChiNhanh branch)
        {
            _context.ChiNhanhs.Update(branch);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var branch = await _context.ChiNhanhs.FindAsync(id);
            if (branch == null) return false;
            _context.ChiNhanhs.Remove(branch);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 