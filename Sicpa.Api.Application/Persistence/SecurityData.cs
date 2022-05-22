using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Persistence
{
    public class SecurityData
    {
        public static async Task InsertAdminUser(SicDbContext db, ILogger<SecurityData> logger)
        {
            try
            {
                var admin = await db.Employees.AsNoTracking()
                        .SingleOrDefaultAsync(x =>
                        x.user == "sicpaadmin");

                if (admin == null)
                {
                    admin = new Employee
                    {
                        created_by = "system",
                        modified_by = "system",
                        age = 1,
                        mail = "alejandro.manguia@hotmail.com",
                        name = "sicpa",
                        surnam = "admin",
                        position = "admin",
                        user = "sicpaadmin"
                    };

                    await db.Employees.AddAsync(admin);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message, ex);
            }
        }
    }
}
