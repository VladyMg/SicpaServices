using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicpa.Api.Domain.Models
{
    [Table("departments_employees")]
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
