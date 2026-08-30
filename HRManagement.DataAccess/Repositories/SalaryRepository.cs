using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace HRManagement.DataAccess.Repositories
{
    public class SalaryRepository
    {
        private readonly AppDbContext _context;

        public SalaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Salary>> GetAllAsync()
        {
            return await _context.Salaries.ToListAsync();
        }

        public async Task<Salary?> GetByIdAsync(int id)
        {
            return await _context.Salaries.FindAsync(id);
        }

        public async Task AddAsync(Salary salary)
        {
            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Salary salary)
        {
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Salary salary)
        {
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
        }
    }
}
