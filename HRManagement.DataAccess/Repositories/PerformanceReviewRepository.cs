using HRManagement.Core.Entities;
using HRManagement.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace HRManagement.DataAccess.Repositories
{
    public class PerformanceReviewRepository
    {
        private readonly AppDbContext _context;

        public PerformanceReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerformanceReview>> GetAllAsync()
        {
            return await _context.PerformanceReviews.ToListAsync();
        }

        public async Task<PerformanceReview?> GetByIdAsync(int id)
        {
            return await _context.PerformanceReviews.FindAsync(id);
        }

        public async Task AddAsync(PerformanceReview review)
        {
            await _context.PerformanceReviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PerformanceReview review)
        {
            _context.PerformanceReviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PerformanceReview review)
        {
            _context.PerformanceReviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
