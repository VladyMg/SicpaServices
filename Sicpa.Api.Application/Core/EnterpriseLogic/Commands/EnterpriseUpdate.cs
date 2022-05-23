using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EnterpriseLogic.Commands
{
    public class EnterpriseUpdate
    {
        public class EnterpriseUpdateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public int id { get; set; }

            public bool status { get; set; }

            public string address { get; set; }

            public string name { get; set; }

            public string phone { get; set; }
        }

        public class EnterpriseUpdateValidator : AbstractValidator<EnterpriseUpdateCommand>
        {
            public EnterpriseUpdateValidator()
            {
                RuleFor(x => x.id).NotEmpty();
                RuleFor(x => x.address).NotEmpty();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.phone).NotEmpty();
            }
        }

        public class EnterpriseUpdateHandler : IRequestHandler<EnterpriseUpdateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly ILogger<EnterpriseUpdate> logger;

            public EnterpriseUpdateHandler(SicDbContext db, ILogger<EnterpriseUpdate> logger)
            {
                this.db = db;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EnterpriseUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var enterprise = await db.Enterprises.AsNoTracking().SingleOrDefaultAsync(x => x.id == request.id, cancellationToken: cancellationToken);

                    if (enterprise == null)
                        return new Response<string> { message = "Error, the company was not found" };

                    enterprise.modified_by = request.requestingUser;
                    enterprise.modified_date = DateTime.UtcNow;
                    enterprise.status = request.status;
                    enterprise.address = request.address;
                    enterprise.name = request.name;
                    enterprise.phone = request.phone;

                    db.Enterprises.Update(enterprise);
                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the company was not updated" };

                    return new Response<string> { ok = true, resp = "The company was successfully updated" };
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
