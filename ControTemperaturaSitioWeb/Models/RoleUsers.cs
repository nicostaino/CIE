using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControTemperaturaSitioWeb.Models
{
    public class RoleUsers
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public long? Role_Id { get; set; }
        public long? User_Id { get; set; }
        public string estado { get; set; }
    }
}