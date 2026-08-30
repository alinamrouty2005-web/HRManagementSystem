
using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DataAccess.Repositories
{
    public class LeaveTypeRepository
    {
        private readonly AppDbContext _context;

        public LeaveTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveType>> GetAllAsync()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType?> GetByIdAsync(int id)
        {
            return await _context.LeaveTypes.FindAsync(id);
        }

        public async Task AddAsync(LeaveType leaveType)
        {
            await _context.LeaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeaveType leaveType)
        {
            _context.LeaveTypes.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LeaveType leaveType)
        {
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();
        }
    }
}
