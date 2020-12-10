using CoursWPF.BankManager.Models;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface du vue-modèle de la liste des comptes bancaire.
    /// </summary>
    public interface IViewModelBankAccounts : IViewModelList<BankAccount, IDataContext>
    {
    }
}
