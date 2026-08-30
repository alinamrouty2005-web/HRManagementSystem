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
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly LeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeService(LeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<List<LeaveTypeDto>> GetAllAsync()
        {
            var leaveTypes =await _leaveTypeRepository.GetAllAsync();

            return leaveTypes.Select(l => new LeaveTypeDto
                {
                    LeaveTypeId = l.LeaveTypeId,
                    Name = l.Name,
                    Description = l.Description
                }).ToList();
        }

        public async Task<LeaveTypeDto?> GetByIdAsync(int id)
        {
            var leaveType =await _leaveTypeRepository.GetByIdAsync(id);

            if (leaveType is null)
                return null;

            return new LeaveTypeDto
            {
                LeaveTypeId = leaveType.LeaveTypeId,
                Name = leaveType.Name,
                Description = leaveType.Description
            };
        }

        public async Task<LeaveTypeDto> CreateAsync(LeaveTypeCreateDto dto)
        {
            var leaveType = new LeaveType
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _leaveTypeRepository.AddAsync(leaveType);

            return new LeaveTypeDto
            {
                LeaveTypeId = leaveType.LeaveTypeId,
                Name = leaveType.Name,
                Description = leaveType.Description
            };
        }

        public async Task<bool> UpdateAsync(int id,LeaveTypeUpdateDto dto)
        {
            var leaveType =await _leaveTypeRepository.GetByIdAsync(id);

            if (leaveType is null)
                return false;

            leaveType.Name = dto.Name;
            leaveType.Description = dto.Description;

            await _leaveTypeRepository.UpdateAsync(leaveType);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var leaveType =await _leaveTypeRepository.GetByIdAsync(id);

            if (leaveType is null)
                return false;

            await _leaveTypeRepository.DeleteAsync(leaveType);

            return true;
        }
    }
}
