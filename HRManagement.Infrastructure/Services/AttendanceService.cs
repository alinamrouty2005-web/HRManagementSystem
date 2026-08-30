using System;
using System.Collections.Generic;
using System.Text;
using HRManagement.Core.DTOs;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Repositories;
using HRManagement.Core.Exceptions;


namespace HRManagement.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly EmployeeRepository _employeeRepository;

        public AttendanceService(AttendanceRepository attendanceRepository,EmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<AttendanceDto>> GetAllAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();

            return attendances.Select(a => new AttendanceDto
                {
                    AttendanceId = a.AttendanceId,
                    EmployeeId = a.EmployeeId,
                    Date = a.Date,
                    CheckIn = a.CheckIn,
                    CheckOut = a.CheckOut,
                    IsPresent = a.IsPresent
                }).ToList();
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance is null)
                return null;

            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                Date = attendance.Date,
                CheckIn = attendance.CheckIn,
                CheckOut = attendance.CheckOut,
                IsPresent = attendance.IsPresent
            };
        }

        public async Task<AttendanceDto> CreateAsync(AttendanceCreateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                IsPresent = dto.IsPresent
            };

            await _attendanceRepository.AddAsync(attendance);

            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                Date = attendance.Date,
                CheckIn = attendance.CheckIn,
                CheckOut = attendance.CheckOut,
                IsPresent = attendance.IsPresent
            };
        }
        public async Task<bool> UpdateAsync(int id,AttendanceUpdateDto dto)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance is null)
                return false;

            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            attendance.EmployeeId = dto.EmployeeId;
            attendance.Date = dto.Date;
            attendance.CheckIn = dto.CheckIn;
            attendance.CheckOut = dto.CheckOut;
            attendance.IsPresent = dto.IsPresent;

            await _attendanceRepository.UpdateAsync(attendance);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance is null)
                return false;

            await _attendanceRepository.DeleteAsync(attendance);

            return true;
        }
    }
}
