using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.DepartamentsEmployeesLogic.Commands
{
    public class DepartmentEmployeeCreate
    {
        public class DepartmentEmployeeCreateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public int id_department { get; set; }

            public int id_employee { get; set; }
        }

        public class DepartmentEmployeeCreateValidator : AbstractValidator<DepartmentEmployeeCreateCommand>
        {
            public DepartmentEmployeeCreateValidator()
            {
                RuleFor(x => x.id_department).GreaterThan(0);
                RuleFor(x => x.id_employee).GreaterThan(0);
            }
        }

        public class DepartmentEmployeeCreateHandler : IRequestHandler<DepartmentEmployeeCreateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<DepartmentEmployeeCreate> logger;

            public DepartmentEmployeeCreateHandler(SicDbContext db, IMapper mapper, ILogger<DepartmentEmployeeCreate> logger)
            {
                this.db = db;
                this.mapper = mapper;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(DepartmentEmployeeCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var department = await db.Departments.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                        x.id == request.id_department,
                        cancellationToken: cancellationToken);

                    if (department == null)
                        return new Response<string> { message = "Error, department was not found" };


                    var employee = await db.Employees.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                        x.id == request.id_employee,
                        cancellationToken: cancellationToken);

                    if (employee == null)
                        return new Response<string> { message = "Error, employee was not found" };


                    var de = await db.DepartmentsEmpoyees.AsNoTracking()
                        .SingleOrDefaultAsync(x =>
                        x.id_department == request.id_department &&
                        x.id_employee == request.id_employee,
                        cancellationToken: cancellationToken);

                    if (de != null)
                        return new Response<string> { message = "Error, relationship was not found" };

                    de = mapper.Map<DepartmentEmployeeCreateCommand, Department_Empoyee>(request);
                    de.created_by = request.requestingUser;
                    de.modified_by = request.requestingUser;

                    db.DepartmentsEmpoyees.Add(de);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, relationship was no created" };

                    return new Response<string> { ok = true, resp = "The relationship was successfully created" };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<string> { message = ex.Message };
                }
            }
        }
    }
}
