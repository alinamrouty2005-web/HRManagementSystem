using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace HRManagement.DataAccess.Repositories
{
    public class PayrollRepository
    {
        private readonly AppDbContext _context;

        public PayrollRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payroll>> GetAllAsync()
        {
            return await _context.Payrolls.ToListAsync();
        }

        public async Task<Payroll?> GetByIdAsync(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }

        public async Task AddAsync(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payroll payroll)
        {
            _context.Payrolls.Update(payroll);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Payroll payroll)
        {
            _context.Payrolls.Remove(payroll);
            await _context.SaveChangesAsync();
        }
    }
}
