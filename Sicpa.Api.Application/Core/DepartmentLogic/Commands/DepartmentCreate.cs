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

namespace Sicpa.Api.Application.Core.DepartmentLogic.Commands
{
    public class DepartmentCreate
    {
        public class DepartmentCreateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public string description { get; set; }

            public string name { get; set; }

            public string phone { get; set; }

            public int id_enterprise { get; set; }
        }

        public class DepartmentCreateValidator : AbstractValidator<DepartmentCreateCommand>
        {
            public DepartmentCreateValidator()
            {
                RuleFor(x => x.description).NotEmpty();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.phone).NotEmpty();
                RuleFor(x => x.id_enterprise).NotEmpty();
            }
        }

        public class DepartmentCreateHandler : IRequestHandler<DepartmentCreateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<DepartmentCreate> logger;

            public DepartmentCreateHandler(SicDbContext db, IMapper mapper, ILogger<DepartmentCreate> logger)
            {
                this.db = db;
                this.mapper = mapper;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(DepartmentCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var department = await db.Departments.AsNoTracking()
                        .SingleOrDefaultAsync(x =>
                        x.name.ToUpper() == request.name.ToUpper(),
                        cancellationToken: cancellationToken);

                    if (department != null)
                        return new Response<string> { message = $"Error, the department {request.name} already exists" };

                    department = mapper.Map<DepartmentCreateCommand, Department>(request);
                    department.name = department.name.ToUpper();
                    department.created_by = request.requestingUser;
                    department.modified_by = request.requestingUser;

                    db.Departments.Add(department);
                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the department was not created" };

                    return new Response<string> { ok = true, resp = "The department was successfully created" };
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
