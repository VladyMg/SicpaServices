using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicpa.Api.Domain.Dto
{
    public class EnterpriseDto
    {
        public int id { get; set; }

        public bool status { get; set; }

        public string name { get; set; }

        public string phone { get; set; }

        public string address { get; set; }        
    }
}
