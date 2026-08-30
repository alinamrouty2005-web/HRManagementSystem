using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace HRManagement.DataAccess.Repositories
{
    public class EmployeeProjectRepository
    {
        private readonly AppDbContext _context;

        public EmployeeProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeProject>> GetAllAsync()
        {
            return await _context.EmployeeProjects.ToListAsync();
        }

        public async Task<EmployeeProject?> GetByIdAsync(int employeeId,int projectId)
        {
            return await _context.EmployeeProjects.FindAsync(employeeId, projectId);
        }

        public async Task AddAsync(EmployeeProject employeeProject)
        {
            await _context.EmployeeProjects.AddAsync(employeeProject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Update(employeeProject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Remove(employeeProject);
            await _context.SaveChangesAsync();
        }
    }
}
