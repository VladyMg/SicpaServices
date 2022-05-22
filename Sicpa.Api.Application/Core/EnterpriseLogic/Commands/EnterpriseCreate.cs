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

namespace Sicpa.Api.Application.Core.EnterpriseLogic.Commands
{
    public class EnterpriseCreate
    {
        public class EnterpriseCreateCommand : IRequest<Response<string>>
        {
            public string requestingUser { get; set; }

            public string address { get; set; }

            public string name { get; set; }

            public string phone { get; set; }
        }

        public class EnterpriseCreateValidator : AbstractValidator<EnterpriseCreateCommand>
        {
            public EnterpriseCreateValidator()
            {
                RuleFor(x => x.address).NotEmpty();
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.phone).NotEmpty();
            }
        }

        public class EnterpriseCreateHandler : IRequestHandler<EnterpriseCreateCommand, Response<string>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EnterpriseCreate> logger;

            public EnterpriseCreateHandler(SicDbContext dbContext, IMapper mapper, ILogger<EnterpriseCreate> logger)
            {
                this.db = dbContext;
                this.mapper = mapper;
                this.logger = logger;
            }

            public async Task<Response<string>> Handle(EnterpriseCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var enterprise = await db.Enterprises.AsNoTracking()
                        .SingleOrDefaultAsync(x =>
                        x.name.ToUpper() == request.name.ToUpper(),
                        cancellationToken: cancellationToken);

                    if (enterprise != null)
                        return new Response<string> { message = $"Error, the company {request.name} already exists" };

                    enterprise = mapper.Map<EnterpriseCreateCommand, Enterprise>(request);
                    enterprise.created_by = request.requestingUser;
                    enterprise.modified_by = request.requestingUser;

                    db.Enterprises.Add(enterprise);

                    var val = await db.SaveChangesAsync(cancellationToken);

                    if (val <= 0)
                        return new Response<string> { message = "Error, the company was not created" };

                    return new Response<string> { ok = true, resp = "The company was successfully created" };
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
