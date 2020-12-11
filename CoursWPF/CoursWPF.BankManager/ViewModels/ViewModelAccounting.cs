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
    ///     ViewModel pour la gestion des comptes bancaires.
    /// </summary>
    public class ViewModelAccounting : ViewModelList<BankAccount>, IAddItemDeleteItem
    {
        #region Fields

        /// <summary>
        ///     ViewModel pour la gestion des lignes d'écritures du compte sélectionné.
        /// </summary>
        private ViewModelBankAccountLines _ViewModelBankAccountLines;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le ViewModel pour la gestion des lignes d'écritures du compte sélectionné.
        /// </summary>
        public ViewModelBankAccountLines ViewModelBankAccountLines
        {
            get => this._ViewModelBankAccountLines;
            private set => this.SetProperty(nameof(this._ViewModelBankAccountLines), ref this._ViewModelBankAccountLines, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelAccounting"/>.
        /// </summary>
        public ViewModelAccounting()
        {
            this.Title = "Comptes";
            this.ItemsSource = App.DataStore.BankAccounts;
            this.ViewModelBankAccountLines = new ViewModelBankAccountLines();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Déclenche l'événement <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changée.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    //Lorsque le compte sélectionné change, on donne le compte sélectionné au ViewModel qui affiche les écritures.
                    this.ViewModelBankAccountLines.SelectedBankAccount = this.SelectedItem;
                    break;
                default:
                    break;
            }
        }

        #region AddItem

        /// <summary>
        ///     Test si la commande <see cref="AddItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteAddItem(object param) => this.ViewModelBankAccountLines?.AddItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="AddItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteAddItem(object param) => this.ViewModelBankAccountLines?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        /// <summary>
        ///     Test si la commande <see cref="DeleteItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteDeleteItem(object param) => this.ViewModelBankAccountLines?.DeleteItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="DeleteItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteDeleteItem(object param) => this.ViewModelBankAccountLines?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
