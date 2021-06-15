using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ControTemperaturaSitioWeb.Models
{
    public static class ExceptionLoger
    {
        public static void Write(Exception exception)
        {
            try
            {

                using (StreamWriter sr = File.AppendText("result.txt")) //new StreamWriter("result.txt", Encoding. ))
                {

                    sr.WriteLine("=>" + DateTime.Now + " " + " An Error occurred: " + exception.StackTrace +
                        " Message: " + exception.Message + "\n\n");
                    sr.Flush();
                }
            }


            catch (Exception e)
            {
                throw;
            }

        }
    }
}