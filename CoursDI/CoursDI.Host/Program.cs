using CoursDI.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CoursDI.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            List<I> modules = new List<I>();
            foreach (string file in Directory.GetFiles(@".\Plugins", "*.dll", SearchOption.AllDirectories))
            {
                //Transforme le chemin relatif en chemin absolu
                string asmPath = Path.GetFullPath(file);
                Console.WriteLine($"Loading assembly : {asmPath}");

                //On load l'assembly
                Assembly asm = Assembly.LoadFile(asmPath);

                foreach (Type type in asm.GetTypes().Where(t => t.GetInterface(nameof(I)) != null))
                {
                    Console.WriteLine($"Create instance of type : {type.FullName}");

                    modules.Add(Activator.CreateInstance(type) as I);
                }
            }

            new MainMenu(modules);

            Console.ReadLine();
        }
    }
}