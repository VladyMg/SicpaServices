using MediatR;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.DepartmentLogic.Commands
{
    public class DepartmentDelete
    {
        public class DepartmentDeleteCommand : IRequest<Response<string>>
        {
            public int id { get; set; }
        }

        public class DepartmentDeleteHandler : IRequestHandler<DepartmentDeleteCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<DepartmentDelete> logger;

            public DepartmentDeleteHandler(SicDbContext db, ILogger<DepartmentDelete> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(DepartmentDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var department = await db.Departments.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (department == null)
                        return new Response<string> { message = "Error, the department was not found" };

                    db.Departments.Remove(department);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the department was not deleted" };

                    return new Response<string> { ok = true, resp = "The department was successfully deleted" };
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
