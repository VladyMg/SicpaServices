using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EmployeeLogic.Commands
{
    public class EmployeeUpdate
    {
        public class EmployeeUpdateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public int id { get; set; }

            public bool status { get; set; }

            public int age { get; set; }

            public string mail { get; set; }

            public string name { get; set; }

            public string surnam { get; set; }

            public string position { get; set; }
        }

        public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateCommand>
        {
            public EmployeeUpdateValidator()
            {
                RuleFor(x => x.age).NotEmpty().GreaterThan(0);
                RuleFor(x => x.mail).NotEmpty().EmailAddress();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.surnam).NotEmpty();
                RuleFor(x => x.position).NotEmpty();
            }
        }

        public class EmployeeUpdateHandler : IRequestHandler<EmployeeUpdateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<EmployeeUpdate> logger;

            public EmployeeUpdateHandler(SicDbContext db, ILogger<EmployeeUpdate> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.id == request.id, cancellationToken: cancellationToken);

                    if (employee == null)
                        return new Response<string> { message = "Error, the employee was not found" };

                    employee.modified_by = request.requestingUser;
                    employee.modified_date = DateTime.UtcNow;
                    employee.status = request.status;
                    employee.age = request.age;
                    employee.mail = request.mail;
                    employee.name = request.name;
                    employee.surnam = request.surnam;
                    employee.position = request.position;
                    employee.user = (request.name + request.surnam).ToLower().Trim().Replace(" ", "");

                    db.Employees.Update(employee);
                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the employee was not updated" };

                    return new Response<string> { ok = true, resp = "The employee was successfully updated" };
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
