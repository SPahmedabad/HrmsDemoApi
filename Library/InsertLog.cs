using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class InsertLog
    {
        /*It help us to insert log in .txt file. 
         * It creates new log file on daily basis*/
        public static void WriteErrorLog(string exception)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog\\ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            string directory = path.Substring(0, path.LastIndexOf('\\'));

            //If directory not exist it creates directory.
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //If file not exist it creates directory.
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine("--------------------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------------");
            tw.WriteLine(exception);
            tw.Close();

        }
    }
}
