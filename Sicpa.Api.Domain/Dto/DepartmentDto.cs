using Sicpa.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Dto
{
    public class DepartmentDto
    {
        public int id { get; set; }

        public bool status { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public string phone { get; set; }

        public int id_enterprise { get; set; }

        public List<EmployeeDto> employees { get; set; }
    }
}
