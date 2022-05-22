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
    public class EmployeeValidUser
    {
        public class EmployeeValidUserCommand : IRequest<Response<string>>
        {
            public string user { get; set; }

            public string position { get; set; }
        }

        public class EmployeeValidUserHandler : IRequestHandler<EmployeeValidUserCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<EmployeeValidUser> logger;

            public EmployeeValidUserHandler(SicDbContext db, ILogger<EmployeeValidUser> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EmployeeValidUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await db.Employees.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                        x.user.ToLower() == request.user.ToLower() &&
                        x.position.ToLower() == request.position.ToLower(),
                        cancellationToken: cancellationToken);

                    if (user == null)
                        return new Response<string> { message = "Error, user not allowed" };

                    return new Response<string> { ok = true, resp = "Acces granted" };
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
