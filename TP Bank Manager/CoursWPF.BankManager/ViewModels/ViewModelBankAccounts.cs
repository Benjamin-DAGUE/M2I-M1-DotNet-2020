using CoursWPF.BankManager.Models;
using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     Vue-modèle de la liste des comptes bancaires.
    /// </summary>
    public class ViewModelBankAccounts : ViewModelList<BankAccount, IDataContext>, IViewModelBankAccounts
    {
        #region Properties

        /// <summary>
        ///     Obtient le titre du vue-modèle.
        /// </summary>
        public string Title => "Comptes";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelBankAccounts"/>.
        /// </summary>
        /// <param name="dataContext">Contexte de données.</param>
        public ViewModelBankAccounts(IDataContext dataContext)
            : base(dataContext)
        {
            this.LoadData();
        }

        #endregion

        #region Methods

        #region AddCommand

        /// <summary>
        ///     Méthode d'exécution de la commande <see cref="AddCommand"/>.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        protected override void Add(object parameter)
        {
            base.Add(parameter);

            this.SelectedItem.Identifier = this.DataContext.GetItems<BankAccount>().Max(c => c.Identifier) + 1;
        }

        #endregion

        #endregion
    }
}
