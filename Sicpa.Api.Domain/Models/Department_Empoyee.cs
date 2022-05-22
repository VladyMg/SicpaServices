using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Models
{
    public class Department_Empoyee : Entity
    {
        [Required]
        public int id_department { get; set; }

        [Required]
        public int id_employee { get; set; }

        public Department department { get; set; }

        public Employee employee { get; set; }
    }
}
