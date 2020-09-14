using AddressBook.Client.Models;
using System;

namespace AddressBook.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Person p = new Person
            { 
                FirstName = "Benjamin"
            };

            p.ToUpper();

            Console.WriteLine(p.FirstName + " " + p.LastName);

            Console.WriteLine("Appuyez sur une touche pour terminer...");
            Console.ReadKey();
        }
    }
}
