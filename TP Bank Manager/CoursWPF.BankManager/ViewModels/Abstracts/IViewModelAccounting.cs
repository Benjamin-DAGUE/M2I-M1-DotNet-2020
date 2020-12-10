using CoursWPF.BankManager.Models;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface du vue-modèle de la page de gestion des comptes.
    /// </summary>
    public interface IViewModelAccounting : IViewModelList<BankAccount, IDataContext>
    {

    }
}
