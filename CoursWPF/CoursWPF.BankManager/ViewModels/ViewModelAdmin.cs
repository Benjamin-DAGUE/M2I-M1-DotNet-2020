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
    ///     ViewModel pour l'onglet Administration dans le Tab principal.
    /// </summary>
    public class ViewModelAdmin : ViewModelList<IAddItemDeleteItem>, IAddItemDeleteItem
    {
        #region Fields

        /// <summary>
        ///     ViewModel pour l'administration des comptes bancaires.
        /// </summary>
        private readonly ViewModelAccounts _ViewModelAccounts;

        /// <summary>
        ///     ViewModel pour l'adminisation des catégories.
        /// </summary>
        private readonly ViewModelCategories _ViewModelCategories;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le ViewModel pour l'administration des comptes bancaires.
        /// </summary>
        public ViewModelAccounts ViewModelAccounts => this._ViewModelAccounts;

        /// <summary>
        ///     Obtient ou définit le ViewModel pour l'adminisation des catégories.
        /// </summary>
        public ViewModelCategories ViewModelCategories => this._ViewModelCategories;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelAdmin"/>.
        /// </summary>
        public ViewModelAdmin()
        {
            this.Title = "Administration";
            this._ViewModelAccounts = new ViewModelAccounts();
            this._ViewModelCategories = new ViewModelCategories();

            this.ItemsSource.Add(this.ViewModelAccounts);
            this.ItemsSource.Add(this.ViewModelCategories);
        }

        #endregion

        #region Methods

        #region AddItem

        /// <summary>
        ///     Test si la commande <see cref="AddItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteAddItem(object param) => this.SelectedItem?.AddItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="AddItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteAddItem(object param) => this.SelectedItem?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        /// <summary>
        ///     Test si la commande <see cref="DeleteItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="DeleteItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
