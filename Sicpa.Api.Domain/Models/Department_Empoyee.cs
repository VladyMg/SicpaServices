using System.ComponentModel.DataAnnotations;

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
