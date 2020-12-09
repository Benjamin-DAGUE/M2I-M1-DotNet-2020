using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CoursWPF.MVVM.Abstracts;

namespace CoursWPF.FirstApp.ViewModels
{
    /// <summary>
    ///     ViewModel pour gérer l'application (version onglets dynamiques).
    /// </summary>
    public class ViewModelMainDynamic : ViewModelList<IObservableObject>, IViewModelMainDynamic
    {
        #region Fields

        /// <summary>
        ///     Commande pour sauvegarder les données.
        /// </summary>
        private readonly RelayCommand _Save;

        /// <summary>
        ///     Commande pour quitter l'application.
        /// </summary>
        private readonly RelayCommand _Exit;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la commande pour sauvegarder les donnnées.
        /// </summary>
        public RelayCommand Save => this._Save;

        /// <summary>
        ///     Obtient la commande pour quitter l'application.
        /// </summary>
        public RelayCommand Exit => this._Exit;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelMainDynamic"/>.
        /// </summary>
        public ViewModelMainDynamic()
        {
            this._Save = new RelayCommand(this.ExecuteSave, this.CanExecuteSave);
            this._Exit = new RelayCommand(this.ExecuteExit, this.CanExecuteExit);
        }

        #endregion

        #region Methods

        #region AddItem

        protected override bool CanExecuteAddItem(object param) => param is Type;

        protected override IObservableObject CreateInstance(object param) => App.ServiceProvider.GetService((Type)param) as IObservableObject;

        #endregion

        #region Save

        /// <summary>
        ///     Test si la commande <see cref="Save"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteSave(object param) => false;

        /// <summary>
        ///     Exécute la commande <see cref="Save"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteSave(object param)
        {

        }

        #endregion

        #region Exit

        /// <summary>
        ///     Test si la commande <see cref="Exit"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExecuteExit(object param) => true;

        /// <summary>
        ///     Exécute la commande <see cref="Exit"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected virtual void ExecuteExit(object param) => Environment.Exit(0);

        #endregion

        #endregion
    }
}
