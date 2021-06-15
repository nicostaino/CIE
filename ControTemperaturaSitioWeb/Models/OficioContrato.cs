using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControTemperaturaSitioWeb.Models
{
   
    public class OficioContrato
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long LandId { get; set; }
        public long? Contract_Id { get; set; }
        public long? TypeEmployee_Id { get; set; }
        public string estado { get; set; }
    }
}