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
    public class PositionService : IPositionService
    {
        private readonly PositionRepository _positionRepository;

        public PositionService(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<PagedResultDto<PositionDto>> GetAllAsync(string? search,int pageNumber,int pageSize,string? sortBy,decimal? minSalary,decimal? maxSalary)
        {
            var positions =await _positionRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                positions = positions.Where(p =>p.Title.Contains(search) || (p.Description != null &&
                         p.Description.Contains(search))).ToList();
            }

            if (minSalary.HasValue)
            {
                positions = positions.Where(p => p.BaseSalary >= minSalary.Value).ToList();
            }

            if (maxSalary.HasValue)
            {
                positions = positions.Where(p => p.BaseSalary <= maxSalary.Value).ToList();
            }

            if (sortBy == "title")
            {
                positions = positions.OrderBy(p => p.Title).ToList();
            }
            else if (sortBy == "salary")
            {
                positions = positions.OrderBy(p => p.BaseSalary).ToList();
            }
            else
            {
                positions = positions.OrderBy(p => p.PositionId).ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = positions.Count();

            var result = positions.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(p => new PositionDto
                {
                    PositionId = p.PositionId,
                    Title = p.Title,
                    Description = p.Description,
                    BaseSalary = p.BaseSalary
                }).ToList();

            return new PagedResultDto<PositionDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<PositionDto?> GetByIdAsync(int id)
        {
            var position =await _positionRepository.GetByIdAsync(id);

            if (position is null)
                return null;

            return new PositionDto
            {
                PositionId = position.PositionId,
                Title = position.Title,
                Description = position.Description,
                BaseSalary = position.BaseSalary
            };
        }

        public async Task<PositionDto> CreateAsync(PositionCreateDto dto)
        {
            var position = new Position
            {
                Title = dto.Title,
                Description = dto.Description,
                BaseSalary = dto.BaseSalary
            };

            await _positionRepository.AddAsync(position);

            return new PositionDto
            {
                PositionId = position.PositionId,
                Title = position.Title,
                Description = position.Description,
                BaseSalary = position.BaseSalary
            };
        }

        public async Task<bool> UpdateAsync(int id,PositionUpdateDto dto)
        {
            var position =await _positionRepository.GetByIdAsync(id);

            if (position is null)
                return false;

            position.Title = dto.Title;
            position.Description = dto.Description;
            position.BaseSalary = dto.BaseSalary;

            await _positionRepository.UpdateAsync(position);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var position =await _positionRepository.GetByIdAsync(id);

            if (position is null)
                return false;

            await _positionRepository.DeleteAsync(position);

            return true;
        }
    }
}