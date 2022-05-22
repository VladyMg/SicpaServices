using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Dto;
using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EmployeeLogic.Queries
{
    public class EmployeeFilterId
    {
        public class EmployeeFilterIdQuery : IRequest<Response<EmployeeDto>>
        {
            public int id { get; set; }
        }

        public class EmployeeFilterIdHandler : IRequestHandler<EmployeeFilterIdQuery, Response<EmployeeDto>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EmployeeFilterId> logger;

            public EmployeeFilterIdHandler(SicDbContext dbContext, ILogger<EmployeeFilterId> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<EmployeeDto>> Handle(EmployeeFilterIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await db.Employees.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (employee == null)
                        return new Response<EmployeeDto> { message = "Error, the employee was not found" };

                    var employeeDto = mapper.Map<Employee, EmployeeDto>(employee);

                    employeeDto.departments = mapper.Map<List<Department>, List<DepartmentDto>>(await db.DepartmentsEmpoyees
                        .Where(x => x.id_employee == employee.id)
                        .Join(
                        db.Departments,
                        dp => dp.id_department,
                        department => department.id,
                        (db, department) => department
                        ).ToListAsync(cancellationToken: cancellationToken));

                    return new Response<EmployeeDto> { ok = true, resp = employeeDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<EmployeeDto> { message = ex.Message };
                }
            }
        }
    }
}
