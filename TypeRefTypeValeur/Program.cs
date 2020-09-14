using System;

namespace TypeRefTypeValeur
{

    class Person
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person()
            {
                Name = "Benjamin DAGUÉ"
            };

            ToUpper(person1);
        }

        private static void ToUpper(Person pers)
        {
            pers.Name = pers.Name.ToUpper();
        }
    }
}
