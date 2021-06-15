using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
   public class Contract : BaseEntity
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Contratista")]
        public long? ContractorId { get; set; }

        [Display(Name = "Contratista")]
        public virtual Contractor Contractor { get; set; }
        //[Display(Name = "Esta activo?")]
        //public bool isActive { get; set; }
        [Display(Name = "Fecha Inicio")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [Display(Name = "Fecha Fin")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        public long? LandId { get; set; }

        public virtual Land Land { get; set; }

        public virtual ICollection<TypeEmployee> TypeEmployees { get; set; }
    }
}
