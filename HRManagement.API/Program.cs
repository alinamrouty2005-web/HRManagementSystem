using HRManagement.Core.Interfaces;
using HRManagement.DataAccess.Context;
using HRManagement.DataAccess.Repositories;
using HRManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HRManagement.API.Middleware;
using HRManagement.Core.Entities;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HRManagement")));

builder.Services.AddScoped<EmployeeRepository>();

builder.Services.AddScoped<DepartmentRepository>();

builder.Services.AddScoped<PositionRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<AttendanceRepository>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<LeaveTypeRepository>();

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

builder.Services.AddScoped<LeaveRequestRepository>();

builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

builder.Services.AddScoped<SalaryRepository>();

builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddScoped<PayrollRepository>();

builder.Services.AddScoped<IPayrollService, PayrollService>();

builder.Services.AddScoped<PerformanceReviewRepository>();

builder.Services.AddScoped<IPerformanceReviewService,PerformanceReviewService>();

builder.Services.AddScoped<ProjectRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<EmployeeProjectRepository>();

builder.Services.AddScoped<IEmployeeProjectService, EmployeeProjectService>();

builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<EmployeeProfileRepository>();

builder.Services.AddScoped<IEmployeeProfileService, EmployeeProfileService>();

builder.Services.AddScoped<EmployeePositionRepository>();

builder.Services.AddScoped<IEmployeePositionService,EmployeePositionService>();

// Application health check
builder.Services.AddHealthChecks();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    var adminUsername =
        builder.Configuration["AdminSeed:Username"];

    var adminPassword =
        builder.Configuration["AdminSeed:Password"];

    var employee = await context.Employees.FindAsync(1);

    if (employee is not null &&
        !string.IsNullOrEmpty(adminUsername) &&
        !string.IsNullOrEmpty(adminPassword))
    {
        var existingUser = await context.Users
            .FirstOrDefaultAsync(u =>
                u.Username == adminUsername);

        if (existingUser is null)
        {
            var adminUser = new User
            {
                Username = adminUsername,
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(adminPassword),
                EmployeeId = employee.EmployeeId,
                IsActive = true
            };

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();

            var adminRole = new UserRole
            {
                UserId = adminUser.UserId,
                RoleId = 1
            };

            await context.UserRoles.AddAsync(adminRole);
            await context.SaveChangesAsync();
        }
    }
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
