using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
  public   class Contractor : BaseEntity
    {
        [Display(Name = "Nombre Contratista")]
        public string Name { get; set; }
        [Display(Name = "Detalle")]
        public string Description { get; set; }
        [Display(Name = "Habilitado")]
        public bool IsEnabled { get; set; }

        public long? LandId { get; set; }
        public virtual Land Land { get; set; }
    }
}
