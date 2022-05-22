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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Core.DepartmentLogic.Queries
{
    public class DepartmentList
    {
        public class DepartmentListQuery : IRequest<Response<List<DepartmentDto>>>
        {

        }

        public class DepartmentListHandler : IRequestHandler<DepartmentListQuery, Response<List<DepartmentDto>>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<DepartmentList> logger;

            public DepartmentListHandler(SicDbContext dbContext, ILogger<DepartmentList> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<List<DepartmentDto>>> Handle(DepartmentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var departments = await db.Departments.AsNoTracking()
                        .OrderBy(x => x.name)
                        .ToListAsync(cancellationToken: cancellationToken);

                    var departmentsDto = mapper.Map<List<Department>, List<DepartmentDto>>(departments);

                    return new Response<List<DepartmentDto>> { ok = true, resp = departmentsDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<List<DepartmentDto>> { message = ex.Message };
                }
            }
        }
    }
}
