using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using Sicpa.Api.Domain.Dto;
using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.EnterpriseLogic.Queries
{
    public class EnterpriseList
    {
        public class EnterpriseListQuery : IRequest<Response<List<EnterpriseDto>>>
        {

        }

        public class EnterpriseListHandler : IRequestHandler<EnterpriseListQuery, Response<List<EnterpriseDto>>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EnterpriseList> logger;

            public EnterpriseListHandler(SicDbContext dbContext, ILogger<EnterpriseList> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<List<EnterpriseDto>>> Handle(EnterpriseListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var enterprises = await db.Enterprises.AsNoTracking()
                        .OrderBy(x => x.name)
                        .ToListAsync(cancellationToken: cancellationToken);

                    var enterprisesDto = mapper.Map<List<Enterprise>, List<EnterpriseDto>>(enterprises);

                    return new Response<List<EnterpriseDto>> { ok = true, resp = enterprisesDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<List<EnterpriseDto>> { message = ex.Message };
                }
            }
        }
    }
}
