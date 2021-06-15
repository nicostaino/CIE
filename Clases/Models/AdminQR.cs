using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
    public class AdminQR : BaseEntity
    {
        [Display(Name = "Codigo QR Admin")]
        public string QR { get; set; }
        [Display(Name = "Activo?")]
        public bool Active { get; set; }

        [Display(Name = "Campo")]
        public long LandId { get; set; }
        [Display(Name = "Campo")]
        public virtual Land Land { get; set; }
    }
}
