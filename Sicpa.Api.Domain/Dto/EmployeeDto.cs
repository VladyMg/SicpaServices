using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Dto
{
    public class EmployeeDto
    {
        public int id { get; set; }

        public bool status { get; set; }

        public int age { get; set; }

        public string mail { get; set; }

        public string name { get; set; }

        public string surnam { get; set; }

        public string position { get; set; }

        public string user { get; set; }

        public List<DepartmentDto> departments { get; set; }
    }
}
