using CoursWPF.BankManager.Models;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     ViewModel pour l'administration des comptes bancaires.
    /// </summary>
    public class ViewModelAccounts : ViewModelList<BankAccount>, IAddItemDeleteItem
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelAccounts"/>.
        /// </summary>
        public ViewModelAccounts()
        {
            this.Title = "Comptes";
            this.ItemsSource = App.DataStore.BankAccounts;
        }

        #endregion

        #region Methods

        #region AddItem

        /// <summary>
        ///     Retourne une nouvelle instance (appelée lors de l'exécution de la commande <see cref="AddItem"/>).
        /// </summary>
        /// <param name="param">Paramètre de la commande <see cref="AddItem"/>.</param>
        /// <returns>Nouvelle instance.</returns>
        protected override BankAccount CreateInstance(object param) => new BankAccount()
        {
            Identifier = Guid.NewGuid()
        };

        #endregion

        #endregion
    }
}
