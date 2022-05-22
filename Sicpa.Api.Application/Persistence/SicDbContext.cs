using Microsoft.EntityFrameworkCore;
using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Application.Persistence
{
    public class SicDbContext : DbContext
    {
        public SicDbContext(DbContextOptions<SicDbContext> options) : base(options) { }

        public DbSet<Enterprise> Enterprises { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department_Empoyee> DepartmentsEmpoyees { get; set; }
    }
}
