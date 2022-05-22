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
    public class DepartmentUpdate
    {
        public class DepartmentUpdateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public int id { get; set; }

            public bool status { get; set; }

            public string description { get; set; }

            public string name { get; set; }

            public string phone { get; set; }

            public int id_enterprise { get; set; }
        }

        public class DepartmentUpdateValidator : AbstractValidator<DepartmentUpdateCommand>
        {
            public DepartmentUpdateValidator()
            {
                RuleFor(x => x.id).NotEmpty();
                RuleFor(x => x.description).NotEmpty();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.phone).NotEmpty();
                RuleFor(x => x.id_enterprise).NotEmpty();
            }
        }

        public class DepartmentUpdateHandler : IRequestHandler<DepartmentUpdateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<DepartmentUpdate> logger;

            public DepartmentUpdateHandler(SicDbContext db, ILogger<DepartmentUpdate> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(DepartmentUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var department = await db.Departments.AsNoTracking().SingleOrDefaultAsync(x => x.id == request.id, cancellationToken: cancellationToken);

                    if (department == null)
                        return new Response<string> { message = "Error, the department was not found" };

                    department.modified_by = request.requestingUser;
                    department.modified_date = DateTime.UtcNow;
                    department.status = request.status;
                    department.description = request.description;
                    department.name = request.name.ToUpper();
                    department.phone = request.phone;
                    department.id_enterprise = request.id_enterprise;

                    db.Departments.Update(department);
                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the department was not updated" };

                    return new Response<string> { ok = true, resp = "The department was successfully updated" };
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
