using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Models
{
    public class Entity
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string created_by { get; set; }

        [Required]
        public DateTime created_date { get; set; } = DateTime.UtcNow;

        [Required]
        public string modified_by { get; set; }

        [Required]
        public DateTime modified_date { get; set; } = DateTime.UtcNow;

        [Required]
        public bool status { get; set; } = true;
    }
}
