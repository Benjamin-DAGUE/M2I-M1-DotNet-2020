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
            get => this._LastName;
            set => this._LastName = value;
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(this.FirstName)
                                ||
                                string.IsNullOrWhiteSpace(this.LastName);

        #endregion

        #region Methods

        public void ToUpper()
        {
            if (this.FirstName != null)
            {
                this.FirstName = this.FirstName.ToUpper();
            }
            //L'opérateur ?. permet de faire un test de null avant l'accès.
            //En cas de valeur null, retour null.
            //L'opérateur ?? permet d'affecter une valeur de remplacement si la partie 
            //de gauche est null.
            this.LastName = (this.LastName?.ToUpper()) ?? "DEFAULT";
        }

        //public bool IsEmpty() => string.IsNullOrWhiteSpace(this.FirstName) 
        //                            ||
        //                        string.IsNullOrWhiteSpace(this.LastName);

        #endregion
    }
}
