using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.Models
{
    public class Person : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        ///     Événément déclenché lorsqu'une propriété de <see cref="Person"/> change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        /// <summary>
        ///     Prénom d'une personne.
        /// </summary>
        private string _FirstName;
        
        /// <summary>
        ///     Nom d'une personne.
        /// </summary>
        private string _LastName;

        #endregion

        /// <summary>
        ///     Obtient ou définit le prénom d'une personne.
        /// </summary>
        public string FirstName 
        {
            get => this._FirstName;
            set
            {
                // On affecte la nouvelle valeur de l'attribut.
                this._FirstName = value;
                // On déclenche l'événement PropertyChanged pour indiquer au moteur de Binding que la propriété a changée.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }

        /// <summary>
        ///     Obtient ou définit le nom d'une personne.
        /// </summary>
        public string LastName
        {
            get => this._LastName;
            set
            {
                this._LastName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LastName)));
            }
        }
    }
}
