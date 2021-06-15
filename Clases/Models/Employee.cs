 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
    public class Employee : BaseEntity
    {
 
        [Display(Name = "Nombre Trabajador")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Trabajador")]
        public string Apellido { get; set; }

        [Display(Name = "Documento")] 
        public string   Documento { get; set; }

        [Display(Name = "Campo")]
        public long LandId { get; set; }

        [Display(Name = "Campo")]
        public virtual Land Land {get;set;}

        [Display(Name = "Sexo")]
        public string Sex { get; set; }

        [Display(Name = "En planilla")]
        public bool Local { get; set; }

        //Se toma el ultimo QR Ordenado por decha Desc
        public virtual List<QR> QRHistory { get; set; }

        [Display(Name = "Tipo Empleado")]
        public long? TypeEmployeeId { get; set; }

        [Display(Name = "Tipo Empleado")]
        public TypeEmployee TypeEmployee { get; set; }

        [Display(Name = "Es capataz?")]
        public bool isforeman { get; set; }

        [Display(Name = "Capataz")]
        public long? EmployeeId { get; set; }

        [Display(Name = "Capataz")]
        public virtual Employee Foreman { get; set; }

        [Display(Name = "Contratista")]
        public long? ContractorId { get; set; }

        [Display(Name = "Contratista")]
        public virtual Contractor Contractor { get; set; }

        [Display(Name = "Esta activo?")]
        public bool isActive { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Img { get; set; }

        public DateTime? hiringDate { get; set; }

        public DateTime birthDate { get; set; }

        public DateTime lastUpdate { get; set; }

        public long groupNumber { get; set; }

        public string QR { get; set; }

        public int? ContractId { get; set; }

        public virtual Contract Contract { get; set; }

        public string NombreCompleto()
        {
            return this.Apellido + ", " + this.Nombre;
        }
    }
}
