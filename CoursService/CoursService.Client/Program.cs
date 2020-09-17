using CoursService.Core;
using M2I.Diagnostics;
using M2I.Diagnostics.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Loggers.AvaillableLoggers.Add(new ConsoleLogger());
            Loggers.AvaillableLoggers.Add(new FileLogger()
            {
                Source = ".\\logfile.log"
            });
            Loggers.AvaillableLoggers.Add(new EventLogger()
            {
                Name = "Image Resizer Service",
                Source = "Image Resizer Service"
            });
            MonService service = new MonService(@"C:\TMP\INPUT", "C:\\TMP\\OUTPUT");

            service.Start();

            Console.ReadLine();

            service.Stop();

            Console.ReadLine();
        }
    }
}
