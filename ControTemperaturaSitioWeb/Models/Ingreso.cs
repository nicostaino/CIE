using Clases;
using Clases.Models;
using System;

namespace ControTemperaturaSitioWeb.Controllers
{
    public  class EgresosFromView
    {
       public int? retiredbefore { get; set; }
      public string descriptionout { get; set; }
        ////  public DateTime? IngressDateTime { get; set; }
         public DateTime? egressDateTime { get; set; }
        //  //Se toma el ultimo QR Ordenado por decha Desc
         public long? EmployeeId { get; set; }
         public Employee Employee { get; set; }
        
        public long? ForemanId { get; set; }
        public Employee Foreman { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }
        public long? LandId { get; set; }
        public long? contractorista_Id { get; set; }
        public long? TypeEmployeeId { get; set; }
        public long? groupNumber { get; set; }
        public Contractor Contractor { get; set; }
        public int? TakenFromForeman { get; set; }
        public TypeEmployee  TypeEmployee { get; set; }
        public string Accion { get; set; }
    }

    public class EgresosView
    {
        public int? retiredbefore { get; set; }
        public string descriptionout { get; set; }
        ////  public DateTime? IngressDateTime { get; set; }
        public DateTime? egressDateTime { get; set; }
        //  //Se toma el ultimo QR Ordenado por decha Desc
        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Documento { get; set; }
        public long? ForemanId { get; set; }
        public string ForemanName { get; set; }
        public long? UserId { get; set; }
        public long? LandId { get; set; }
        public long? contractorista_Id { get; set; }
        public long? TypeEmployeeId { get; set; }
        public long? groupNumber { get; set; }
        public string ContractorName { get; set; }
        public int? TakenFromForeman { get; set; }
        public string TypeEmployeeName { get; set; }
        public string Accion { get; set; }
        public string RegisterDate { get; set; }
    }

    public class EgresosTrabajadorView
    {
        ////  public DateTime? IngressDateTime { get; set; }
        public long Id { get; set; }
        //  //Se toma el ultimo QR Ordenado por decha Desc
        public DateTime? WorkDate { get; set; }
        public DateTime? IngressDateTime { get; set; }
        public DateTime? EgressDateTime { get; set; }
        public long EmployeeId { get; set; }
    }
}