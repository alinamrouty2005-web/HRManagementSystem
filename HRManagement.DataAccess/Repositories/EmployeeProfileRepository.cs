using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace HRManagement.DataAccess.Repositories
{
    public class EmployeeProfileRepository
    {
        private readonly AppDbContext _context;

        public EmployeeProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeProfile>> GetAllAsync()
        {
            return await _context.EmployeeProfiles.ToListAsync();
        }

        public async Task<EmployeeProfile?> GetByIdAsync(int id)
        {
            return await _context.EmployeeProfiles.FindAsync(id);
        }

        public async Task<EmployeeProfile?> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeProfiles.FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
        }

        public async Task AddAsync(EmployeeProfile profile)
        {
            await _context.EmployeeProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeProfile profile)
        {
            _context.EmployeeProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeeProfile profile)
        {
            _context.EmployeeProfiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}
