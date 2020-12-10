using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels.Abstracts
{
    /// <summary>
    ///     Vue-modèle pour la page d'administration.
    /// </summary>
    public interface IViewModelAdministration : IViewModelList<IObservableObject, IDataContext>
    {
        #region Properties

        /// <summary>
        ///     Obtient le vue-modèle de la liste des comptes bancaires.
        /// </summary>
        IViewModelBankAccounts ViewModelBankAccounts { get; }

        /// <summary>
        ///     Obtient le vue-modèle de la liste des catégories.
        /// </summary>
        IViewModelCategories ViewModelCategories { get; }

        #endregion
    }
}
