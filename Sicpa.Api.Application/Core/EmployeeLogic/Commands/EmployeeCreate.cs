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

namespace Sicpa.Api.Application.Core.EmployeeLogic.Commands
{
    public class EmployeeCreate
    {
        public class EmployeeCreateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public int age { get; set; }

            public string mail { get; set; }

            public string name { get; set; }

            public string surnam { get; set; }

            public string position { get; set; }
        }

        public class EmployeeCreateValidator : AbstractValidator<EmployeeCreateCommand>
        {
            public EmployeeCreateValidator()
            {
                RuleFor(x => x.age).NotEmpty().GreaterThan(0);
                RuleFor(x => x.mail).NotEmpty().EmailAddress();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.surnam).NotEmpty();
                RuleFor(x => x.position).NotEmpty();
            }
        }

        public class EmployeeCreateHandler : IRequestHandler<EmployeeCreateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EmployeeCreate> logger;

            public EmployeeCreateHandler(SicDbContext dbContext, IMapper mapper, ILogger<EmployeeCreate> logger)
            {
                this.db = dbContext;
                this.mapper = mapper;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await db.Employees.AsNoTracking()
                        .SingleOrDefaultAsync(x =>
                        x.name.ToUpper() == request.name.ToUpper() &&
                        x.surnam.ToUpper() == request.surnam.ToUpper(),
                        cancellationToken: cancellationToken);

                    if (employee != null)
                        return new Response<string> { message = $"Error, the employee {request.name} {request.surnam} already exists" };

                    employee = mapper.Map<EmployeeCreateCommand, Employee>(request);
                    employee.created_by = request.requestingUser;
                    employee.modified_by = request.requestingUser;
                    employee.user = (request.name + request.surnam).ToLower().Trim();

                    db.Employees.Add(employee);
                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the employee was not created" };

                    return new Response<string> { ok = true, resp = "The employee was successfully created" };
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
