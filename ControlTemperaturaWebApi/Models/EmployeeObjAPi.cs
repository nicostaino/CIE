using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaWebApi.Models
{
    public class EmployeeObjAPi
    {
        public string document { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public string birthDate { get; set; }
        public bool isForeman { get; set; }
        public long? task { get; set; }
        public string qr { get; set; }
        public long foreman { get; set; }
        public long id { get; set; }
        public long? contractor { get; set; }
        public string base64 { get; set; }
        public long user { get; set; }
        public long field { get; set; }
        public long groupNumber { get; set; }
        public bool local { get; set; }
        public DateTime hiringDate {get;set;}
        public int? contractId { get; set; }
    }


    //public class  AllEmployee
    //{

    //    public long Id { get; set; }
    //    public string Nombre { get; set; }
    //    public string Documento { get; set; }
    //    public long LandId { get; set; }
    //    public string Sex { get; set; }
    //    public string Local { get; set; }
    //    public string Qr { get; set; }
    //    public bool isforeman { get; set; }
    //    public long? ContractorId { get; set; }
    //    public long  EmployeeId { get; set; }
    //    public long ? TypeEmployeeId { get; set; }
    //    public bool isActive { get; set; }
    //  //  public string Img { get; set; }
    //    public DateTime birthDate { get; set; }
    //    public long groupNumber { get; set; } 
    //}
}