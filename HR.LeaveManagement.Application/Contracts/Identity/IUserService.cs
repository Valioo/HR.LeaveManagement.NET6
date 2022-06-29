﻿using HR.LeaveManagement.Application.DTOs.Employee;
using HR.LeaveManagement.Application.DTOs.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<AllEmployeesDto>> GetAllEmployees();
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
        Task<EmployeeDetailsDto> GetEmployeeById(string userId);
        Task<RegistrationResponse> RegisterEmployee(RegisterEmployeeDto registerEmployeeDto);
    }
}
