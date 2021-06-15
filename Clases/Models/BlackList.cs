using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
    public class BlackList : BaseEntity
    {
        [Display(Name = "Usuario que realizo el registro")]
        public long UserId { get; set; }
        [Display(Name = "Usuario que realizo el registro")]
        public virtual User UserRegister { get; set; } 
        public long Documento  { get; set; }
        [Display(Name = "Fecha de ingreso")]
        public DateTime AdmissionDate { get; set; }
        [Display(Name = "Motivo de ingreso")]
        public string Reason { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
    }
}
