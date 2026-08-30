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
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly LeaveRequestRepository _leaveRequestRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly LeaveTypeRepository _leaveTypeRepository;

        public LeaveRequestService(LeaveRequestRepository leaveRequestRepository,EmployeeRepository employeeRepository,LeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<List<LeaveRequestDto>> GetAllAsync()
        {
            var requests = await _leaveRequestRepository.GetAllAsync();

            return requests.Select(lr => new LeaveRequestDto
                {
                    LeaveRequestId = lr.LeaveRequestId,
                    EmployeeId = lr.EmployeeId,
                    LeaveTypeId = lr.LeaveTypeId,
                    StartDate = lr.StartDate,
                    EndDate = lr.EndDate,
                    Reason = lr.Reason,
                    IsApproved = lr.IsApproved
                }).ToList();
        }

        public async Task<LeaveRequestDto?> GetByIdAsync(int id)
        {
            var request = await _leaveRequestRepository.GetByIdAsync(id);

            if (request is null)
                return null;

            return new LeaveRequestDto
            {
                LeaveRequestId = request.LeaveRequestId,
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason,
                IsApproved = request.IsApproved
            };
        }

        public async Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var leaveType = await _leaveTypeRepository.GetByIdAsync(dto.LeaveTypeId);

            if (leaveType is null)
                throw new ValidationException("Leave type does not exist.");

            if (dto.EndDate < dto.StartDate)
                throw new ValidationException("End date cannot be before start date.");

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = dto.EmployeeId,
                LeaveTypeId = dto.LeaveTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                IsApproved = dto.IsApproved
            };

            await _leaveRequestRepository.AddAsync(leaveRequest);

            return new LeaveRequestDto
            {
                LeaveRequestId = leaveRequest.LeaveRequestId,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveTypeId = leaveRequest.LeaveTypeId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason,
                IsApproved = leaveRequest.IsApproved
            };
        }

        public async Task<bool> UpdateAsync(int id,LeaveRequestUpdateDto dto)
        {
            var request = await _leaveRequestRepository.GetByIdAsync(id);

            if (request is null)
                return false;

            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var leaveType = await _leaveTypeRepository.GetByIdAsync(dto.LeaveTypeId);

            if (leaveType is null)
                throw new ValidationException("Leave type does not exist.");

            if (dto.EndDate < dto.StartDate)
                throw new ValidationException("End date cannot be before start date.");

            request.EmployeeId = dto.EmployeeId;
            request.LeaveTypeId = dto.LeaveTypeId;
            request.StartDate = dto.StartDate;
            request.EndDate = dto.EndDate;
            request.Reason = dto.Reason;
            request.IsApproved = dto.IsApproved;

            await _leaveRequestRepository.UpdateAsync(request);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _leaveRequestRepository.GetByIdAsync(id);

            if (request is null)
                return false;

            await _leaveRequestRepository.DeleteAsync(request);

            return true;
        }
    }
}
