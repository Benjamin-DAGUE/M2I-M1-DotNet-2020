using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface du vue-modèle principal de l'application.
    /// </summary>
    public interface IViewModelMain : IViewModelList<IObservableObject, IDataContext>
    {
        #region Properties

        /// <summary>
        ///     Obtient le vue-modèle de la page de gestion des comptes.
        /// </summary>
        public IViewModelAccounting ViewModelAccounting { get; }

        /// <summary>
        ///     Obtient le vue-modèle de la page des statistiques.
        /// </summary>
        public IViewModelStatistics ViewModelStatistics { get; }

        /// <summary>
        ///     Obtient le vue-modèle de la page d'administration
        /// </summary>
        public IViewModelAdministration ViewModelAdministration { get; }

        #endregion
    }
}
