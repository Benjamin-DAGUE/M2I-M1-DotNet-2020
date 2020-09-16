using CoursService.Core;
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
            MonService service = new MonService(@"C:\TMP\INPUT", "C:\\TMP\\OUTPUT");

            service.Start();

            Console.ReadLine();

            service.Stop();

            Console.ReadLine();
        }
    }
}
