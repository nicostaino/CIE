using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaWeb.Models
{
    public class TemperatureRecibeApi
    {
            public long ID { get; set; }
            public string DOCUMENTO { get; set; }
            public DateTime FECHA { get; set; }
            public int ID_CAMPO { get; set; }
            public int ID_USUARIO { get; set; }
            public int LOCAL { get; set; }
            public string NOMBRE { get; set; }
            public string TEMPERATURA { get; set; }
    }
}