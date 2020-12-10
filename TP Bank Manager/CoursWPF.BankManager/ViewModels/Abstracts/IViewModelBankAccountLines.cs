using CoursWPF.BankManager.Models;
using CoursWPF.MVVM;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoursWPF.BankManager.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface du vue-modèle de la liste des écritures bancaire d'un compte bancaire associé.
    /// </summary>
    public interface IViewModelBankAccountLines : IViewModelList<BankAccountLine, IDataContext>
    {
        #region Properties

        /// <summary>
        ///     Obtient ou définit le compte bancaire associé.
        /// </summary>
        BankAccount BankAccount { get; set; }

        /// <summary>
        ///     Obtient la date de la vue filtrée.
        /// </summary>
        DateTime CurrentDate { get; set; }

        /// <summary>
        ///     Obtient la collection des catégories disponibles.
        /// </summary>
        ObservableCollection<Category> Categories { get; }

        /// <summary>
        ///     Obtient la commande de changement de période.
        /// </summary>
        RelayCommand ChangePeriodCommand { get; }

        #endregion
    }
}
