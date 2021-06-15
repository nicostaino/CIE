using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaWebApi.Models
{
    public class FormIngressEgress
    {
        public bool specialEgress { get; set; }
        public string egressReason { get; set; }
        public string time { get; set; }
        public long? employee_document { get; set; }  
        public long? foreman_document { get; set; }
        public long? contractor_id { get; set; } 
        public long field_id { get; set; }
        public long user_id { get; set; }   
        public bool IsSyncToVisma { get; set; }
        public bool isForeman { get; set; }
        public int task_id   { get; set; }
        public int groupNumber { get; set; }
        public int? contract_id{ get; set; }

    }
}