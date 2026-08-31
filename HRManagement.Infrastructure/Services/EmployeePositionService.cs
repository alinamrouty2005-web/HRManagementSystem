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
    public class EmployeePositionService : IEmployeePositionService
    {
        private readonly EmployeePositionRepository _repository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly PositionRepository _positionRepository;

        public EmployeePositionService(
            EmployeePositionRepository repository,
            EmployeeRepository employeeRepository,
            PositionRepository positionRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        public async Task<PagedResultDto<EmployeePositionDto>> GetAllAsync(
     string? search,
     int pageNumber,
     int pageSize,
     int? employeeId,
     int? positionId,
     string? sortBy)
        {
            var employeePositions =
                await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search) &&
                int.TryParse(search, out int searchEmployeeId))
            {
                employeePositions = employeePositions
                    .Where(ep => ep.EmployeeId == searchEmployeeId)
                    .ToList();
            }

            if (employeeId.HasValue)
            {
                employeePositions = employeePositions
                    .Where(ep => ep.EmployeeId == employeeId.Value)
                    .ToList();
            }

            if (positionId.HasValue)
            {
                employeePositions = employeePositions
                    .Where(ep => ep.PositionId == positionId.Value)
                    .ToList();
            }

            if (sortBy == "startDate")
            {
                employeePositions = employeePositions
                    .OrderBy(ep => ep.StartDate)
                    .ToList();
            }
            else
            {
                employeePositions = employeePositions
                    .OrderBy(ep => ep.EmployeeId)
                    .ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = employeePositions.Count();

            var result = employeePositions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ep => new EmployeePositionDto
                {
                    EmployeeId = ep.EmployeeId,
                    PositionId = ep.PositionId,
                    StartDate = ep.StartDate,
                    EndDate = ep.EndDate
                })
                .ToList();

            return new PagedResultDto<EmployeePositionDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<EmployeePositionDto?> GetByIdAsync(int employeeId,int positionId)
        {
            var employeePosition = await _repository.GetByIdAsync(employeeId,positionId);

            if (employeePosition is null)
                return null;

            return new EmployeePositionDto
            {
                EmployeeId = employeePosition.EmployeeId,
                PositionId = employeePosition.PositionId,
                StartDate = employeePosition.StartDate,
                EndDate = employeePosition.EndDate
            };
        }

        public async Task<EmployeePositionDto> CreateAsync(EmployeePositionCreateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var position = await _positionRepository.GetByIdAsync(dto.PositionId);

            if (position is null)
                throw new ValidationException("Position does not exist.");

            if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            {
                throw new ValidationException("End date cannot be before start date.");
            }

            var existing =await _repository.GetByIdAsync(dto.EmployeeId, dto.PositionId);

            if (existing is not null)
                throw new ValidationException("This employee is already assigned to this position.");

            var employeePosition = new EmployeePosition
            {
                EmployeeId = dto.EmployeeId,
                PositionId = dto.PositionId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            await _repository.AddAsync(employeePosition);

            return new EmployeePositionDto
            {
                EmployeeId = employeePosition.EmployeeId,
                PositionId = employeePosition.PositionId,
                StartDate = employeePosition.StartDate,
                EndDate = employeePosition.EndDate
            };
        }

        public async Task<bool> UpdateAsync(int employeeId,int positionId,EmployeePositionUpdateDto dto)
        {
            var employeePosition =await _repository.GetByIdAsync(employeeId,positionId);

            if (employeePosition is null)
                return false;

            var employee =await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var position =await _positionRepository.GetByIdAsync(dto.PositionId);

            if (position is null)
                throw new ValidationException("Position does not exist.");

            if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            {
                throw new ValidationException("End date cannot be before start date.");
            }

            employeePosition.EmployeeId = dto.EmployeeId;
            employeePosition.PositionId = dto.PositionId;
            employeePosition.StartDate = dto.StartDate;
            employeePosition.EndDate = dto.EndDate;

            await _repository.UpdateAsync(employeePosition);

            return true;
        }

        public async Task<bool> DeleteAsync(int employeeId,int positionId)
        {
            var employeePosition =await _repository.GetByIdAsync(employeeId,positionId);

            if (employeePosition is null)
                return false;

            await _repository.DeleteAsync(employeePosition);

            return true;
        }
    }
}
