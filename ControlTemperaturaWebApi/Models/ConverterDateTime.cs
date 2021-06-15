using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaWebApi.Models
{
    public static class ConverterDateTime
    {
        public static DateTime converteStringTodateTime(string strDate)
        {
            var parts = strDate.Split('/');
            var time = parts[2].Split(' ')[1].Split(':');
            DateTime mydate = new DateTime(int.Parse(parts[2].Split(' ')[0]), int.Parse(parts[1]), int.Parse(parts[0]), int.Parse(time[0]),  int.Parse(time[1]), int.Parse(time[2]));
            return mydate; 
        }

        public static DateTime converteStringTodate(string strDate)
        {
            var parts = strDate.Split('/');
           DateTime mydate = new DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
            return mydate;
        }
    }
}