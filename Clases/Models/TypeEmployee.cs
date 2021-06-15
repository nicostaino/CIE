using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Clases.Models
{
public class  TypeEmployee:BaseEntity
    {

        [System.ComponentModel.DataAnnotations.Display(Name = "Nombre")]
        public string Description { get; set; }
        public long? LandId { get; set; }
        public virtual Land Land { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
