using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Clases
{
    public class Temperature : BaseEntity
    {

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public bool local { get; set; }

        public string Value { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public long? LandId { get; set; }
        public virtual Land Land { get; set; }
    }
}
