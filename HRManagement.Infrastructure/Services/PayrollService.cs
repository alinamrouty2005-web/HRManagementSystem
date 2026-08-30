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
    public class PayrollService : IPayrollService
    {
        private readonly PayrollRepository _payrollRepository;
        private readonly EmployeeRepository _employeeRepository;

        public PayrollService(PayrollRepository payrollRepository,EmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<PayrollDto>> GetAllAsync()
        {
            var payrolls = await _payrollRepository.GetAllAsync();

            return payrolls.Select(p => new PayrollDto
                {
                    PayrollId = p.PayrollId,
                    EmployeeId = p.EmployeeId,
                    GrossSalary = p.GrossSalary,
                    TotalDeductions = p.TotalDeductions,
                    NetSalary = p.NetSalary,
                    PayrollDate = p.PayrollDate,
                    IsPaid = p.IsPaid
                }).ToList();
        }

        public async Task<PayrollDto?> GetByIdAsync(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);

            if (payroll is null)
                return null;

            return new PayrollDto
            {
                PayrollId = payroll.PayrollId,
                EmployeeId = payroll.EmployeeId,
                GrossSalary = payroll.GrossSalary,
                TotalDeductions = payroll.TotalDeductions,
                NetSalary = payroll.NetSalary,
                PayrollDate = payroll.PayrollDate,
                IsPaid = payroll.IsPaid
            };
        }

        public async Task<PayrollDto> CreateAsync(PayrollCreateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            if (dto.TotalDeductions > dto.GrossSalary)
                throw new ValidationException("Total deductions cannot be greater than gross salary.");

            var calculatedNetSalary =dto.GrossSalary - dto.TotalDeductions;

            if (dto.NetSalary != calculatedNetSalary)
                throw new ValidationException("Net salary must equal gross salary minus total deductions.");

            var payroll = new Payroll
            {
                EmployeeId = dto.EmployeeId,
                GrossSalary = dto.GrossSalary,
                TotalDeductions = dto.TotalDeductions,
                NetSalary = dto.NetSalary,
                PayrollDate = dto.PayrollDate,
                IsPaid = dto.IsPaid
            };

            await _payrollRepository.AddAsync(payroll);

            return new PayrollDto
            {
                PayrollId = payroll.PayrollId,
                EmployeeId = payroll.EmployeeId,
                GrossSalary = payroll.GrossSalary,
                TotalDeductions = payroll.TotalDeductions,
                NetSalary = payroll.NetSalary,
                PayrollDate = payroll.PayrollDate,
                IsPaid = payroll.IsPaid
            };
        }

        public async Task<bool> UpdateAsync(int id,PayrollUpdateDto dto)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);

            if (payroll is null)
                return false;

            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                throw new ValidationException("Employee does not exist.");

            if (dto.TotalDeductions > dto.GrossSalary)
                throw new ValidationException("Total deductions cannot be greater than gross salary.");

            var calculatedNetSalary = dto.GrossSalary - dto.TotalDeductions;

            if (dto.NetSalary != calculatedNetSalary)
                throw new ValidationException("Net salary must equal gross salary minus total deductions.");

            payroll.EmployeeId = dto.EmployeeId;
            payroll.GrossSalary = dto.GrossSalary;
            payroll.TotalDeductions = dto.TotalDeductions;
            payroll.NetSalary = dto.NetSalary;
            payroll.PayrollDate = dto.PayrollDate;
            payroll.IsPaid = dto.IsPaid;

            await _payrollRepository.UpdateAsync(payroll);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payroll = await _payrollRepository.GetByIdAsync(id);

            if (payroll is null)
                return false;

            await _payrollRepository.DeleteAsync(payroll);

            return true;
        }
    }
}
