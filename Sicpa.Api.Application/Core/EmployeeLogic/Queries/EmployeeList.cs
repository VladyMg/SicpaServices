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

namespace Sicpa.Api.Application.Core.EmployeeLogic.Queries
{
    public class EmployeeList
    {
        public class EmployeeListQuery : IRequest<Response<List<EmployeeDto>>>
        {

        }

        public class EmployeeListHandler : IRequestHandler<EmployeeListQuery, Response<List<EmployeeDto>>>
        {
            private readonly SicDbContext db;
            private readonly IMapper mapper;
            private readonly ILogger<EmployeeList> logger;

            public EmployeeListHandler(SicDbContext dbContext, ILogger<EmployeeList> logger, IMapper mapper)
            {
                this.db = dbContext;
                this.logger = logger;
                this.mapper = mapper;
            }

            public async Task<Response<List<EmployeeDto>>> Handle(EmployeeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var employees = await db.Employees.AsNoTracking()
                        .OrderBy(x => x.name)
                        .ThenBy(x => x.surnam)
                        .ToListAsync(cancellationToken: cancellationToken);

                    var employeesDto = mapper.Map<List<Employee>, List<EmployeeDto>>(employees);

                    return new Response<List<EmployeeDto>> { ok = true, resp = employeesDto };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);

                    return new Response<List<EmployeeDto>> { message = ex.Message };
                }
            }
        }
    }
}
