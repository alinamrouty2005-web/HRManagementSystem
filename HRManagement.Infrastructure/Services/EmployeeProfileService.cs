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
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private readonly EmployeeProfileRepository _repository;

        public EmployeeProfileService(EmployeeProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<EmployeeProfileDto>> GetAllAsync(
    string? search,
    int pageNumber,
    int pageSize,
    int? employeeId,
    string? sortBy)
        {
            var profiles =
                await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                profiles = profiles
                    .Where(p =>
                        (p.Address != null &&
                         p.Address.Contains(search)) ||
                        (p.Nationality != null &&
                         p.Nationality.Contains(search)) ||
                        (p.EmergencyContact != null &&
                         p.EmergencyContact.Contains(search)))
                    .ToList();
            }

            if (employeeId.HasValue)
            {
                profiles = profiles
                    .Where(p => p.EmployeeId == employeeId.Value)
                    .ToList();
            }

            if (sortBy == "dateOfBirth")
            {
                profiles = profiles
                    .OrderBy(p => p.DateOfBirth)
                    .ToList();
            }
            else
            {
                profiles = profiles
                    .OrderBy(p => p.EmployeeProfileId)
                    .ToList();
            }

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = profiles.Count();

            var result = profiles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new EmployeeProfileDto
                {
                    EmployeeProfileId = p.EmployeeProfileId,
                    Address = p.Address,
                    DateOfBirth = p.DateOfBirth,
                    Nationality = p.Nationality,
                    EmergencyContact = p.EmergencyContact,
                    EmployeeId = p.EmployeeId
                })
                .ToList();

            return new PagedResultDto<EmployeeProfileDto>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<EmployeeProfileDto?> GetByIdAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile is null)
                return null;

            return new EmployeeProfileDto
            {
                EmployeeProfileId = profile.EmployeeProfileId,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
                Nationality = profile.Nationality,
                EmergencyContact = profile.EmergencyContact,
                EmployeeId = profile.EmployeeId
            };
        }

        public async Task<EmployeeProfileDto?> GetByEmployeeIdAsync(int employeeId)
        {
            var profile = await _repository.GetByEmployeeIdAsync(employeeId);

            if (profile is null)
                return null;

            return new EmployeeProfileDto
            {
                EmployeeProfileId = profile.EmployeeProfileId,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
                Nationality = profile.Nationality,
                EmergencyContact = profile.EmergencyContact,
                EmployeeId = profile.EmployeeId
            };
        }

        public async Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto dto)
        {
            var existingProfile = await _repository.GetByEmployeeIdAsync(dto.EmployeeId);

            if (existingProfile is not null)
                throw new ValidationException("This employee already has a profile.");

            var profile = new EmployeeProfile
            {
                EmployeeId = dto.EmployeeId,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                EmergencyContact = dto.EmergencyContact
            };

            await _repository.AddAsync(profile);

            return new EmployeeProfileDto
            {
                EmployeeProfileId = profile.EmployeeProfileId,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
                Nationality = profile.Nationality,
                EmergencyContact = profile.EmergencyContact,
                EmployeeId = profile.EmployeeId
            };
        }

        public async Task<bool> UpdateAsync(int id,EmployeeProfileUpdateDto dto)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile is null)
                return false;

            profile.EmployeeId = dto.EmployeeId;
            profile.Address = dto.Address;
            profile.DateOfBirth = dto.DateOfBirth;
            profile.Nationality = dto.Nationality;
            profile.EmergencyContact = dto.EmergencyContact;

            await _repository.UpdateAsync(profile);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile is null)
                return false;

            await _repository.DeleteAsync(profile);

            return true;
        }
    }
}
