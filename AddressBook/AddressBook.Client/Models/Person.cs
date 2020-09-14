using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Client.Models
{
    /// <summary>
    ///     Représente une personne du carnet d'adresse.
    /// </summary>
    public class Person
    {
        #region Fields

        /// <summary>
        ///     Prénom de la personne.
        /// </summary>
        private string _FirstName;

        /// <summary>
        ///     Nom de la personne.
        /// </summary>
        private string _LastName;

        /// <summary>
        ///     Date de naissance de la personne.
        /// </summary>
        private DateTime _Birthdate;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le prénom de la personne.
        /// </summary>
        public string FirstName
        {
            get { return this._FirstName; }
            set { this._FirstName = value; }
        }

        /// <summary>
        ///     Obtient ou définit le nom de la personne.
        /// </summary>
        public string LastName
        {
            get => this._LastName; //Syntaxe de propriété sans corp
            set => this._LastName = value;
        }

        /// <summary>
        ///     Obtient ou définit la date de naissance de la personne.
        /// </summary>
        public DateTime Birthdate
        {
            get => this._Birthdate;
            set => this._Birthdate = value;
        }

        /// <summary>
        ///     Obtient l'âge de la personne.
        /// </summary>
        public int Age
        {
            get
            {
                int age;
                DateTime today = DateTime.Today;
                age = today.Year - this.Birthdate.Year;
                if (this.Birthdate.AddYears(age) >= today)
                {
                    age--;
                }
                return age;
            }
        }

        //On peut créer une propriété en lecture seule sans corps
        //public bool IsEmpty => string.IsNullOrWhiteSpace(this.FirstName)
        //                        ||
        //                        string.IsNullOrWhiteSpace(this.LastName);

        #endregion

        #region Methods

        //public void ToUpper()
        //{
        //    if (this.FirstName != null)
        //    {
        //        this.FirstName = this.FirstName.ToUpper();
        //    }
        //    //L'opérateur ?. permet de faire un test de null avant l'accès.
        //    //En cas de valeur null, retour null.
        //    //L'opérateur ?? permet d'affecter une valeur de remplacement si la partie 
        //    //de gauche est null.
        //    this.LastName = (this.LastName?.ToUpper()) ?? "DEFAULT";
        //}

        //public bool IsEmpty() => string.IsNullOrWhiteSpace(this.FirstName) 
        //                            ||
        //                        string.IsNullOrWhiteSpace(this.LastName);

        #endregion
    }
}
