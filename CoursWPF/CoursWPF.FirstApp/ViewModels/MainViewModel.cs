using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels
{
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        ///     Événément déclenché lorsqu'une propriété de <see cref="Person"/> change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        /// <summary>
        ///     Liste des personnes.
        /// </summary>
        private ObservableCollection<Person> _People;

        /// <summary>
        ///     Personne sélectionnée.
        /// </summary>
        private Person _SelectedPerson;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la liste des personnes.
        /// </summary>
        public ObservableCollection<Person> People
        {
            get => this._People;
            private set
            {
                // On affecte la nouvelle valeur de l'attribut.
                this._People = value;
                // On déclenche l'événement PropertyChanged pour indiquer au moteur de Binding que la propriété a changée.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.People)));
            }
        }

        /// <summary>
        ///     Obtient ou définit la personne sélectionnée.
        /// </summary>
        public Person SelectedPerson
        {
            get => this._SelectedPerson;
            set
            {
                // On affecte la nouvelle valeur de l'attribut.
                this._SelectedPerson = value;
                // On déclenche l'événement PropertyChanged pour indiquer au moteur de Binding que la propriété a changée.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedPerson)));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            this._People = new ObservableCollection<Person>();
            this.People.Add(new Person()
            {
                FirstName = "Benjamin",
                LastName = "DAGUÉ"
            });
            this.People.Add(new Person()
            {
                FirstName = "Peter",
                LastName = "BAUDRY"
            });
        }

        #endregion
    }
}
