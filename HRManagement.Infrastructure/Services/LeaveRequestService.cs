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

        public async Task<PagedResultDto<LeaveRequestDto>> GetAllAsync(string? search,int pageNumber,int pageSize,int? employeeId,int? leaveTypeId,bool? isApproved,string? sortBy)
        {
            var requests =await _leaveRequestRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                requests = requests.Where(r => r.Reason != null && r.Reason.Contains(search)).ToList();
            }

            if (employeeId.HasValue)
            {
                requests = requests.Where(r => r.EmployeeId == employeeId.Value).ToList();
            }

            if (leaveTypeId.HasValue)
            {
                requests = requests.Where(r => r.LeaveTypeId == leaveTypeId.Value).ToList();
            }

            if (isApproved.HasValue)
            {
                requests = requests.Where(r => r.IsApproved == isApproved.Value).ToList();
            }

            if (sortBy == "startDate")
            {
                requests = requests.OrderBy(r => r.StartDate).ToList();
            }
            else if (sortBy == "endDate")
            {
                requests = requests.OrderBy(r => r.EndDate).ToList();
            }
            else
            {
                requests = requests.OrderBy(r => r.LeaveRequestId).ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = requests.Count();

            var result = requests
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new LeaveRequestDto
                {
                    LeaveRequestId = r.LeaveRequestId,
                    EmployeeId = r.EmployeeId,
                    LeaveTypeId = r.LeaveTypeId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Reason = r.Reason,
                    IsApproved = r.IsApproved
                }).ToList();

            return new PagedResultDto<LeaveRequestDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
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
