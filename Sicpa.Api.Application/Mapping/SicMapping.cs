using AutoMapper;
using Sicpa.Api.Application.Core.DepartamentsEmployeesLogic.Commands;
using Sicpa.Api.Application.Core.DepartmentLogic.Commands;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using Sicpa.Api.Application.Core.EnterpriseLogic.Commands;
using Sicpa.Api.Domain.Dto;
using Sicpa.Api.Domain.Models;

namespace Sicpa.Api.Application.Mapping
{
    public class SicMapping : Profile
    {
        public SicMapping()
        {
            CreateMap<EnterpriseCreate.EnterpriseCreateCommand, Enterprise>();

            CreateMap<DepartmentCreate.DepartmentCreateCommand, Department>();

            CreateMap<EmployeeCreate.EmployeeCreateCommand, Employee>();

            CreateMap<DepartmentEmployeeCreate.DepartmentEmployeeCreateCommand, Department_Empoyee>();

            CreateMap<Department, DepartmentDto>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<Enterprise, EnterpriseDto>();
        }
    }
}
