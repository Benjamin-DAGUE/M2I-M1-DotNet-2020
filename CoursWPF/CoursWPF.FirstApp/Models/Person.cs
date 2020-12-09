using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.Models
{
    public class Person : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Prénom d'une personne.
        /// </summary>
        private string _FirstName;
        
        /// <summary>
        ///     Nom d'une personne.
        /// </summary>
        private string _LastName;

        /// <summary>
        ///     Genre de la personne (Vrai pour une femme, Faux pour un homme, Null pour un non binaire)
        /// </summary>
        private bool? _Gender;

        #endregion

        /// <summary>
        ///     Obtient ou définit le prénom d'une personne.
        /// </summary>
        public string FirstName 
        {
            get => this._FirstName;
            set => this.SetProperty(nameof(this.FirstName), ref this._FirstName, value);
        }

        /// <summary>
        ///     Obtient ou définit le nom d'une personne.
        /// </summary>
        public string LastName
        {
            get => this._LastName;
            set => this.SetProperty(nameof(this.LastName), ref this._LastName, value);
        }
    
        /// <summary>
        ///     Obtient ou définit le genre de la personne (Vrai pour une femme, Faux pour un homme, Null pour un non binaire).
        /// </summary>
        public bool? Gender
        {
            get => this._Gender;
            set => this.SetProperty(nameof(this.Gender), ref this._Gender, value);
        }
    }
}
