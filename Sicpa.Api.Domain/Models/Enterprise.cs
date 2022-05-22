using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicpa.Api.Domain.Models
{
    public class Enterprise : Entity
    {
        public string name { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        [ForeignKey("id_enterprise")]
        public ICollection<Department> departments { get; set; }
    }
}
