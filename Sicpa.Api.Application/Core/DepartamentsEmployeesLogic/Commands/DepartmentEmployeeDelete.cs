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
    public class DepartmentEmployeeDelete
    {
        public class DepartmentEmployeeDeleteCommand : IRequest<Response<string>>
        {
            public int id_employee { get; set; }
            public int id_department { get; set; }
        }

        public class DepartmentEmployeeDeleteHandler : IRequestHandler<DepartmentEmployeeDeleteCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<DepartmentEmployeeDelete> logger;

            public DepartmentEmployeeDeleteHandler(SicDbContext db, ILogger<DepartmentEmployeeDelete> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(DepartmentEmployeeDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var department = await db.DepartmentsEmpoyees.AsNoTracking()
                        .FirstOrDefaultAsync(x =>
                        x.id_department == request.id_department &&
                        x.id_employee == request.id_employee,
                        cancellationToken: cancellationToken);

                    if (department == null)
                        return new Response<string> { message = "Error, the relationship was not found" };

                    db.DepartmentsEmpoyees.Remove(department);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the relationship was not deleted" };

                    return new Response<string> { ok = true, resp = "The relationship was successfully deleted" };
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
