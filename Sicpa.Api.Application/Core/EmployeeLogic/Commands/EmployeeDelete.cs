using MediatR;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EmployeeLogic.Commands
{
    public class EmployeeDelete
    {
        public class EmployeeDeleteCommand : IRequest<Response<string>>
        {
            public int id { get; set; }
        }

        public class EmployeeDeleteHandler : IRequestHandler<EmployeeDeleteCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<EmployeeDelete> logger;

            public EmployeeDeleteHandler(SicDbContext dbContext, ILogger<EmployeeDelete> logger)
            {
                this.db = dbContext;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await db.Employees.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (employee == null)
                        return new Response<string> { message = "Error, the employee was not found" };

                    db.Employees.Remove(employee);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the employee was not deleted" };

                    return new Response<string> { ok = true, resp = "The employee was successfully deleted" };
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
