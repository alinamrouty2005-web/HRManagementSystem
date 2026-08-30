using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace HRManagement.DataAccess.Repositories
{
    public class EmployeePositionRepository
    {
        private readonly AppDbContext _context;

        public EmployeePositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeePosition>> GetAllAsync()
        {
            return await _context.EmployeePositions.ToListAsync();
        }

        public async Task<EmployeePosition?> GetByIdAsync(int employeeId,int positionId)
        {
            return await _context.EmployeePositions.FindAsync(employeeId, positionId);
        }

        public async Task AddAsync(EmployeePosition employeePosition)
        {
            await _context.EmployeePositions.AddAsync(employeePosition);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeePosition employeePosition)
        {
            _context.EmployeePositions.Update(employeePosition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeePosition employeePosition)
        {
            _context.EmployeePositions.Remove(employeePosition);
            await _context.SaveChangesAsync();
        }
    }
}
