using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoursLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> values = new List<int>();
            Random r = new Random();

            //for (int i = 0; i < 100; i++)
            //{
            //    values.Add(r.Next(0, 100));
            //}

            //Il est possible d'utiliser Linq pour générer une collection de nombre
            //Ici, Enumerable.Range génère une suite de nombres qui commence à 0 et contient 100 éléments
            //Ensuite, on utilise Select pour changer la projection. Ici on remplace le nombre par un nombre aléatoire.
            //ToList() Permet de générer la liste de résultats.
            values = Enumerable.Range(0, 100).Select(num => r.Next(0, 100)).ToList();
            
            //Il est possible de faire un foreach avec la méthode ForEach
            values.ForEach(num => Console.WriteLine(num));
            //Parallel.ForEach permet de faire un ForEach en asynchrone
            Parallel.ForEach(values, num => Thread.Sleep(1000));

            //Where permet de filtrer une liste d'éléments.
            //Ici, on filtre les nombres impaires pour conserver les nombres pairs.
            //Where attend un prédicat de type Func<T, bool>
            //On peut donc utiliser une méthode anonyme au lieu d'utiliser un méthode nommée.
            //Where retourne un IEnumerable<int> qui représente la requête mais pas son résultat.
            //C'est lorsque la collection sera énumérée que le filtre sera appliqué.
            // par exemple lors d'un foreach ou lors d'un ToList().
            IEnumerable<int> evenNumbers = values.Where((value) => value % 2 == 0);
            //IEnumerable<int> evenNumbers = values.Where(CheckEven);

            //if (true)
            //{
            //    //Il est possible de compléter une requête
            //    evenNumbers = evenNumbers.Where(...).Where(...);
            //}

            evenNumbers.ToList().ForEach(num => Console.WriteLine(num));
            //foreach (int value in evenNumbers)
            //{
            //    Console.WriteLine(value);
            //}
            //values.Where(CheckEven).Count();

        }

        private static bool CheckEven(int value) => value % 2 == 0;
    }
}
