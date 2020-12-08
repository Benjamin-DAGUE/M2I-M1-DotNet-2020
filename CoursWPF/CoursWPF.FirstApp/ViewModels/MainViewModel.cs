using CoursWPF.FirstApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        ///     Événément déclenché lorsqu'une propriété de <see cref="Person"/> change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        /// <summary>
        ///     Personne 1.
        /// </summary>
        private Person _P1;

        /// <summary>
        ///     Personne 2.
        /// </summary>
        private Person _P2;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit la personne 1.
        /// </summary>
        public Person P1
        {
            get => this._P1;
            set
            {
                // On affecte la nouvelle valeur de l'attribut.
                this._P1 = value;
                // On déclenche l'événement PropertyChanged pour indiquer au moteur de Binding que la propriété a changée.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.P1)));
            }
        }

        /// <summary>
        ///     Obtient ou définit la personne 2.
        /// </summary>
        public Person P2
        {
            get => this._P2;
            set
            {
                // On affecte la nouvelle valeur de l'attribut.
                this._P2 = value;
                // On déclenche l'événement PropertyChanged pour indiquer au moteur de Binding que la propriété a changée.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.P2)));
            }
        }

        #endregion
    }
}
