using MediatR;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EnterpriseLogic.Commands
{
    public class EnterpriseDelete
    {
        public class EnterpriseDeleteCommand : IRequest<Response<string>>
        {
            public int id { get; set; }
        }

        public class EnterpriseDeleteHandler : IRequestHandler<EnterpriseDeleteCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<EnterpriseDelete> logger;

            public EnterpriseDeleteHandler(SicDbContext dbContext, ILogger<EnterpriseDelete> logger)
            {
                this.db = dbContext;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EnterpriseDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var enterprise = await db.Enterprises.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (enterprise == null)
                        return new Response<string> { message = "Error, the company was not found" };

                    db.Enterprises.Remove(enterprise);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the company was not deleted" };

                    return new Response<string> { ok = true, resp = "The company was successfully deleted" };
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
