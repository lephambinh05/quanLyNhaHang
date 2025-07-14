using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NhaHang.Models;
using NhaHang.Data;

namespace NhaHang.Services
{
    public class QuanTriVienService
    {
        private readonly ApplicationDbContext _context;
        public QuanTriVienService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuanTriVien>> GetAllAsync()
        {
            return await _context.QuanTriViens.Include(q => q.ChiNhanh).ToListAsync();
        }

        public async Task<QuanTriVien?> GetByIdAsync(string id)
        {
            return await _context.QuanTriViens.Include(q => q.ChiNhanh).FirstOrDefaultAsync(q => q.MaQuanTriVien == id);
        }

        public async Task<bool> CreateAsync(QuanTriVien admin)
        {
            _context.QuanTriViens.Add(admin);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(QuanTriVien admin)
        {
            _context.QuanTriViens.Update(admin);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var admin = await _context.QuanTriViens.FindAsync(id);
            if (admin == null) return false;
            _context.QuanTriViens.Remove(admin);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<QuanTriVien?> LoginAsync(string email, string password)
        {
            return await _context.QuanTriViens.FirstOrDefaultAsync(q => q.Email == email && q.MatKhau == password);
        }
    }
} 