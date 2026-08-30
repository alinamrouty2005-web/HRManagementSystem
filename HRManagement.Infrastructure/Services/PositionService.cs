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

        public async Task<List<PositionDto>> GetAllAsync()
        {
            var positions =await _positionRepository.GetAllAsync();

            return positions.Select(p => new PositionDto
                {
                    PositionId = p.PositionId,
                    Title = p.Title,
                    Description = p.Description,
                    BaseSalary = p.BaseSalary
                }).ToList();
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