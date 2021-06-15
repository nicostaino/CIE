using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ControlTemperaturaWebApi.Models
{

    public static class ExceptionLoger
    {
        public static void Write(Exception exception)
        {
            try
            {
                string directory = @"C:\Logs";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string path = Path.Combine(directory, "LogErrors.txt");
                if (!File.Exists(path))
                {
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(" .");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }

                }
                // Create the file, or overwrite if the file exists.
               

                using (StreamWriter sr = File.AppendText(path)) //new StreamWriter("result.txt", Encoding. ))
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