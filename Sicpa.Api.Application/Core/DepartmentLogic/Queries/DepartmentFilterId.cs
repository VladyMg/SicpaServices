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

namespace Sicpa.Api.Application.Core.DepartmentLogic.Queries
{
    public class DepartmentFilterId
    {
        public class DepartmentFilterIdQuery : IRequest<Response<DepartmentDto>>
        {
            public int id { get; set; }
        }

        public class DepartmentFilterIdHandler : IRequestHandler<DepartmentFilterIdQuery, Response<DepartmentDto>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<DepartmentFilterId> logger;

            public DepartmentFilterIdHandler(SicDbContext dbContext, ILogger<DepartmentFilterId> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<DepartmentDto>> Handle(DepartmentFilterIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var department = await db.Departments.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (department == null)
                        return new Response<DepartmentDto> { message = "Error, the department was not found" };

                    var departmentDto = mapper.Map<Department, DepartmentDto>(department);

                    departmentDto.employees = mapper.Map<List<Employee>, List<EmployeeDto>>(
                        await db.DepartmentsEmpoyees
                        .Where(x => x.id_department == department.id)
                        .Join(
                        db.Employees,
                        dp => dp.id_employee,
                        employee => employee.id,
                        (db, employee) => employee
                        ).ToListAsync(cancellationToken: cancellationToken));

                    return new Response<DepartmentDto> { ok = true, resp = departmentDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<DepartmentDto> { message = ex.Message };
                }
            }
        }
    }
}
