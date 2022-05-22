using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicpa.Api.Domain.Models
{
    public class Employee : Entity
    {
        public int age { get; set; }

        public string mail { get; set; }

        public string name { get; set; }

        public string surnam { get; set; }

        public string position { get; set; }

        public string user { get; set; }

        [ForeignKey("id_employee")]
        public ICollection<Employee> employees { get; set; }
    }

}
