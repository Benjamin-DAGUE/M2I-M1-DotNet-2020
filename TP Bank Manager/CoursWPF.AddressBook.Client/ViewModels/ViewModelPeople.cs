using CoursWPF.AddressBook.Client.Models;
using CoursWPF.AddressBook.Client.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CoursWPF.AddressBook.Client.ViewModels
{
    /// <summary>
    ///     Vue-modèle pour la liste des personnes.
    /// </summary>
    public class ViewModelPeople : ViewModelList<Person, IDataContext>, IViewModelPeople
    {
        #region Fields

        /// <summary>
        ///     Commande pour fermer l'application.
        /// </summary>
        private readonly RelayCommand _ExitCommand;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la commande pour fermer l'application.
        /// </summary>
        public RelayCommand ExitCommand => this._ExitCommand;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelPeople"/>.
        /// </summary>
        public ViewModelPeople(IDataContext dataContext)
            : base(dataContext)
        {
            this._ExitCommand = new RelayCommand(this.Exit, this.CanExit);
            this.LoadData();
        }

        #endregion

        #region Methods

        #region ExitCommand

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="ExitCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExit(object parameter) => true;

        /// <summary>
        ///     Méthode d'exécution de la commande <see cref="ExitCommand"/>.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        protected virtual void Exit(object parameter)
        {
            App.Current.Shutdown(0);
        }

        #endregion

        #endregion
    }
}
