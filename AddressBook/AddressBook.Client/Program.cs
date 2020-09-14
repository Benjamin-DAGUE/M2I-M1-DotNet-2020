using AddressBook.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Client
{
    public class Program
    {
        #region Fields

        private static List<Person> _People;

        #endregion

        #region Methods

        static void Main(string[] args)
        {
            _People = new List<Person>();

#if DEBUG
            _People.Add(new Person
            {
                FirstName = "Sofiane",
                LastName = "BOUCARD",
                Birthdate = new DateTime(1998, 3, 24)
            });
            _People.Add(new Person
            {
                FirstName = "Emilie",
                LastName = "GOUGEON",
                Birthdate = new DateTime(1992, 12, 3)
            });
            _People.Add(new Person
            {
                FirstName = "Joris",
                LastName = "GUERRIER",
                Birthdate = new DateTime(1997, 2, 1)
            });
            _People.Add(new Person
            {
                FirstName = "Gabin",
                LastName = "SOUTIF",
                Birthdate = new DateTime(1998, 1, 9)
            });
#endif

            string userChoice = null;
            bool exit = false;

            do
            {
                #region Menu
                Console.Clear();
                Console.WriteLine("1 - Ajouter");
                Console.WriteLine("2 - Parcourir");
                Console.WriteLine("3 - Rechercher");
                Console.WriteLine("0 - Quitter");
                #endregion

                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        AddPerson();
                        break;
                    case "2":
                        ShowPeople(_People);
                        break;
                    case "3":
                        Search();
                        break;
                    case "0": //Quitter
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Choix impossible.");
                        Console.ReadKey();
                        break;
                }
            } while (!exit);

            Console.ReadKey();
        }

        /// <summary>
        /// Ajoute une personne dans le carnet d'adresses.
        /// </summary>
        private static void AddPerson()
        {
            Console.Clear();

            Person p = new Person();
            do
            {
                Console.Write("Prénom : ");
                p.FirstName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(p.FirstName));

            do
            {
                Console.Write("Nom : ");
                p.LastName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(p.LastName));

            DateTime dateTime;

            do
            {
                Console.Write("Date de naissance (dd/mm/yyyy) : ");
            } while (DateTime.TryParse(Console.ReadLine(), out dateTime) == false);

            p.Birthdate = dateTime;

            _People.Add(p);

            Console.Clear();
            ShowPerson(p);

            Console.ReadLine();
        }

        /// <summary>
        /// Affichage des éléments du carnet d'adresse.
        /// </summary>
        private static void ShowPeople(List<Person> people, bool isSearch = false)
        {
            if (people.Any())
            {
                int currentIndex = 0;
                string userChoice = string.Empty;

                do
                {
                    Console.Clear();
                    ShowPerson(people[currentIndex]);

                    Console.WriteLine("1 - Suivant");
                    Console.WriteLine("2 - Précédent");
                    if (!isSearch)
                    {
                        Console.WriteLine("3 - Supprimer");
                    }
                    Console.WriteLine("0 - Retour");

                    userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            currentIndex = (currentIndex + 1 < people.Count ? currentIndex + 1 : 0);
                            break;
                        case "2":
                            currentIndex = (currentIndex - 1 >= 0 ? currentIndex - 1 : people.Count - 1);
                            break;
                        case "3":
                            if (!isSearch)
                            {
                                people.RemoveAt(currentIndex);
                                currentIndex = (currentIndex - 1 >= 0 ? currentIndex - 1 : people.Count - 1);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Choix non valide");
                                Console.ReadLine();
                            }
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Choix non valide");
                            Console.ReadLine();
                            break;
                    }

                } while (userChoice != "0" && people.Any());
            }
            else
            {
                if (!isSearch)
                {
                    Console.WriteLine("Le carnet est vide... Commencez par ajouter un contact.");
                }
                else
                {
                    Console.WriteLine("Aucun contact correspondant à votre recherche.");
                }
                
                Console.ReadLine();
            }
        }

        /// <summary>
        ///     Affiche la personne passée en paramètre.
        /// </summary>
        /// <param name="person">Personne à afficher.</param>
        private static void ShowPerson(Person person)
        {
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine($"Nom : {person.LastName}");
            Console.WriteLine($"Prénom : {person.FirstName}");
            Console.WriteLine($"Date de naissance : {person.Birthdate.ToLongDateString()}");
            Console.WriteLine($"Age : {person.Age}");
            Console.WriteLine($"------------------------------------------");
        }

        /// <summary>
        ///     Moteur de recherche.
        /// </summary>
        private static void Search()
        {
            List<Person> result = new List<Person>();
            Console.Clear();
            Console.Write("Recherche : ");

            string search = Console.ReadLine().ToLower();

            //Utiisation de Linq pour requêter la liste des personnes.
            _People
                .Where(p => p.FirstName.ToLower().Contains(search) || p.LastName.ToLower().Contains(search))
                .ToList()
                .ForEach(p => result.Add(p));

            ShowPeople(result, true);
        }

        #endregion
    }
}
