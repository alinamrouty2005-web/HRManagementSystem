using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.Exceptions;

namespace HRManagement.Infrastructure.Services
{
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly PerformanceReviewRepository _reviewRepository;
        private readonly EmployeeRepository _employeeRepository;

        public PerformanceReviewService(PerformanceReviewRepository reviewRepository,EmployeeRepository employeeRepository)
        {
            _reviewRepository = reviewRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<PerformanceReviewDto>> GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();

            return reviews.Select(r => new PerformanceReviewDto
                {
                    PerformanceReviewId = r.PerformanceReviewId,
                    EmployeeId = r.EmployeeId,
                    ReviewerId = r.ReviewerId,
                    Score = r.Score,
                    Comments = r.Comments,
                    ReviewDate = r.ReviewDate
                }).ToList();
        }

        public async Task<PerformanceReviewDto?> GetByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review is null)
                return null;

            return new PerformanceReviewDto
            {
                PerformanceReviewId = review.PerformanceReviewId,
                EmployeeId = review.EmployeeId,
                ReviewerId = review.ReviewerId,
                Score = review.Score,
                Comments = review.Comments,
                ReviewDate = review.ReviewDate
            };
        }

        public async Task<PerformanceReviewDto> CreateAsync(PerformanceReviewCreateDto dto)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var reviewer =await _employeeRepository.GetByIdAsync(dto.ReviewerId);

            if (reviewer is null)
                throw new ValidationException("Reviewer does not exist.");

            var review = new PerformanceReview
            {
                EmployeeId = dto.EmployeeId,
                ReviewerId = dto.ReviewerId,
                Score = dto.Score,
                Comments = dto.Comments,
                ReviewDate = dto.ReviewDate
            };

            await _reviewRepository.AddAsync(review);

            return new PerformanceReviewDto
            {
                PerformanceReviewId = review.PerformanceReviewId,
                EmployeeId = review.EmployeeId,
                ReviewerId = review.ReviewerId,
                Score = review.Score,
                Comments = review.Comments,
                ReviewDate = review.ReviewDate
            };
        }

        public async Task<bool> UpdateAsync(int id,PerformanceReviewUpdateDto dto)
        {
            var review =await _reviewRepository.GetByIdAsync(id);

            if (review is null)
                return false;

            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var reviewer =await _employeeRepository.GetByIdAsync(dto.ReviewerId);

            if (reviewer is null)
                throw new ValidationException("Reviewer does not exist.");

            review.EmployeeId = dto.EmployeeId;
            review.ReviewerId = dto.ReviewerId;
            review.Score = dto.Score;
            review.Comments = dto.Comments;
            review.ReviewDate = dto.ReviewDate;

            await _reviewRepository.UpdateAsync(review);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);

            if (review is null)
                return false;

            await _reviewRepository.DeleteAsync(review);

            return true;
        }
    }
}
