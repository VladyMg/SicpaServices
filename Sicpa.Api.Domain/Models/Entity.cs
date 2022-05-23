using System;
using System.ComponentModel.DataAnnotations;

namespace Sicpa.Api.Domain.Models
{
    public class Entity
    {
        [Key]
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
