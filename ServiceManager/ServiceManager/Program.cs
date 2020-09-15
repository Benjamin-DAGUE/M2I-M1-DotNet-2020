using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceController.GetServices() permet de lister les services
            //Il prend en paramètre une chaîne qui correspond au nom de la machine cible
            List<ServiceController> currentServices = ServiceController.GetServices().ToList();
            foreach (ServiceController service in currentServices)
            {
                Console.WriteLine(service.ServiceName);
                Console.WriteLine(service.DisplayName);
                Console.WriteLine(service.Status);
                Console.WriteLine("------------------------");
                //Permet de libérer les ressources non managés utilisés par la classe ServiceController
                //Ici, on utilise des ressources non managés pour accéder à la liste des services windows.
                service.Dispose();
                //Attention, après Dispose(), il ne faut plus manipuler l'instance...
            }

            string userChoice = Console.ReadLine();

            //Split permet de retourner une liste des différents mots saisis par l'utilisateur
            userChoice.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            Console.ReadLine();
        }
    }
}
