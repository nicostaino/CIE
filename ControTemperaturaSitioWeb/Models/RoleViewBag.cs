using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControTemperaturaSitioWeb.Models
{
    public class RoleViewBag:Role
    {
        public string hablilitado { get; set; }
    }
}