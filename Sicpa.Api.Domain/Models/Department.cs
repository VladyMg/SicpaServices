using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Models
{
    public class Department : Entity
    {
        public string name { get; set; }

        public string description { get; set; }

        public string phone { get; set; }

        public int id_enterprise { get; set; }

        public Enterprise enterprise { get; set; }

        [ForeignKey("id_department")]
        public ICollection<Department> departments { get; set; }
    }
}
