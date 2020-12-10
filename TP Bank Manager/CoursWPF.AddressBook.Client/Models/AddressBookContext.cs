using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoursWPF.AddressBook.Client.Models
{
    /// <summary>
    ///     Contexte de données de l'application.
    /// </summary>
    public class AddressBookContext : FileDataContext
    {
        #region Fields

        /// <summary>
        ///     Collection de personnes.
        /// </summary>
        private ObservableCollection<Person> _People;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la collection de personnes.
        /// </summary>
        public ObservableCollection<Person> People { get => this._People; private set => this.SetProperty(nameof(this.People), ref this._People, value); }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="AddressBookContext"/>.
        /// </summary>
        /// <param name="filePath">Chemin du fichier de données.</param>
        public AddressBookContext(string filePath)
            : base(filePath)
        {
            this._People = new ObservableCollection<Person>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Créer un élément du type spécifié et l'ajoute au contexte de données.
        /// </summary>
        /// <typeparam name="T">Type de l'élément à créer.</typeparam>
        /// <returns>Retourne un nouvel élément du type spécifié.</returns>
        public override T CreateItem<T>()
        {
            IObservableObject createdItem;

            if (typeof(T) == typeof(Person))
            {
                createdItem = new Person();
                this.People.Add(createdItem as Person);
            }
            else
            {
                throw new Exception("Le type spécifié n'est pas valide");
            }

            return (T)createdItem;
        }

        /// <summary>
        ///     Obtient la collection des éléments du type spécifié.
        /// </summary>
        /// <typeparam name="T">Type de l'élément de la collection.</typeparam>
        /// <returns>Collection des éléments du type spécifié.</returns>
        public override ObservableCollection<T> GetItems<T>()
        {
            ObservableCollection<T> result;

            if (typeof(T) == typeof(Person))
            {
                result = this.People as ObservableCollection<T>;
            }
            else
            {
                throw new Exception("Le type spécifié n'est pas valide");
            }

            return result;
        }

        #endregion
    }
}
