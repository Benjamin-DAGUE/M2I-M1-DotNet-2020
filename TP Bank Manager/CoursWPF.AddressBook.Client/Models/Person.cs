using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.AddressBook.Client.Models
{
    /// <summary>
    ///     Classe de données qui représente une personne.
    /// </summary>
    public class Person : ObservableObject
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
        private DateTime? _Birthdate;

        /// <summary>
        ///     Détermine si la personne est un homme ou une femme.
        /// </summary>
        private bool? _IsMale;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le prénom de la personne.
        /// </summary>
        public string FirstName { get => this._FirstName; set => this.SetProperty(nameof(this.FirstName), ref this._FirstName, value); }

        /// <summary>
        ///     Obtient ou définit le nom de la personne.
        /// </summary>
        public string LastName { get => this._LastName; set => this.SetProperty(nameof(this.LastName), ref this._LastName, value); }

        /// <summary>
        ///     Obtient la première lettre du nom de famille.
        /// </summary>
        public string LastNameFirstLetter => this._LastName?.Substring(0,1)?.ToUpper();

        /// <summary>
        ///     Obtient le nom complet de la personne.
        /// </summary>
        public string FullName => $"{this.FirstName} {this.LastName}";

        /// <summary>
        ///     Obtient ou définit la date de naissance de la personne.
        /// </summary>
        public DateTime? Birthdate { get => this._Birthdate; set => this.SetProperty(nameof(this.Birthdate), ref this._Birthdate, value); }

        /// <summary>
        ///     Obtient l'âge de la personne.
        /// </summary>
        public int Age => (DateTime.Now.Year - this._Birthdate?.Year - (DateTime.Now.DayOfYear < this._Birthdate?.DayOfYear ? 1 : 0) ?? 0);

        /// <summary>
        ///     Obtient ou définit si la personne est un homme ou une femme.
        /// </summary>
        public bool? IsMale { get => this._IsMale; set => this.SetProperty(nameof(this.IsMale), ref this._IsMale, value); }

        #endregion

        #region Methods

        /// <summary>
        ///     Déclenche l'événement <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changé.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.FirstName):
                    this.OnPropertyChanged(nameof(this.FullName));
                    break;
                case nameof(this.LastName):
                    this.OnPropertyChanged(nameof(this.FullName));
                    this.OnPropertyChanged(nameof(this.LastNameFirstLetter));
                    break;
                case nameof(this.Birthdate):
                    this.OnPropertyChanged(nameof(this.Age));
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
