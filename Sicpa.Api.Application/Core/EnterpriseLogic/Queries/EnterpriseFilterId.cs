using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Dto;
using Sicpa.Api.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EnterpriseLogic.Queries
{
    public class EnterpriseFilterId
    {
        public class EnterpriseFilterIdQuery : IRequest<Response<EnterpriseDto>>
        {
            public int id { get; set; }
        }

        public class EnterpriseFilterIdHandler : IRequestHandler<EnterpriseFilterIdQuery, Response<EnterpriseDto>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EnterpriseFilterId> logger;

            public EnterpriseFilterIdHandler(SicDbContext dbContext, ILogger<EnterpriseFilterId> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<EnterpriseDto>> Handle(EnterpriseFilterIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var enterprise = await db.Enterprises.FindAsync(new object[] { request.id }, cancellationToken: cancellationToken);

                    if (enterprise == null)
                        return new Response<EnterpriseDto> { message = "Error, the company was not found" };

                    var enterpriseDto = mapper.Map<Enterprise, EnterpriseDto>(enterprise);

                    return new Response<EnterpriseDto> { ok = true, resp = enterpriseDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<EnterpriseDto> { message = ex.Message };
                }
            }
        }
    }
}
