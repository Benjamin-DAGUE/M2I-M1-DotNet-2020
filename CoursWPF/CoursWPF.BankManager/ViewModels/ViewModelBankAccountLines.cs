using CoursWPF.BankManager.Models;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     ViewModel pour la gestion des lignes d'écritures dans un compte bancaire.
    /// </summary>
    public class ViewModelBankAccountLines : ViewModelList<BankAccountLine>
    {
        #region Fields

        /// <summary>
        ///     Compte bancaire sélectionné pour lequel on doit filtrer les lignes d'écritures.
        /// </summary>
        BankAccount _SelectedBankAccount;

        /// <summary>
        ///     Mois et année pour laquelle les lignes doivent être filtrées.
        /// </summary>
        DateTime _CurrentDate;

        /// <summary>
        ///     Commande pour passer au mois suivant ou au mois précédent.
        /// </summary>
        RelayCommand _ChangePeriod;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le compte bancaire sélectionné pour lequel on doit filtrer les lignes d'écritures.
        /// </summary>
        public BankAccount SelectedBankAccount
        {
            get => this._SelectedBankAccount;
            set => this.SetProperty(nameof(this.SelectedBankAccount), ref this._SelectedBankAccount, value);
        }

        /// <summary>
        ///     Obtient ou définit le mois et année pour laquelle les lignes doivent être filtrées.
        /// </summary>
        public DateTime CurrentDate
        {
            get => this._CurrentDate;
            private set => this.SetProperty(nameof(this.CurrentDate), ref this._CurrentDate, value);
        }

        /// <summary>
        ///     Obtient ou définit la commande pour passer au mois suivant ou au mois précédent.
        /// </summary>
        public RelayCommand ChangePeriod => this._ChangePeriod;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelBankAccountLines"/>.
        /// </summary>
        public ViewModelBankAccountLines()
        {
            this.ItemsSource = null;
            this.CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this._ChangePeriod = new RelayCommand(this.ExecuteChangePeriod, this.CanExecuteChangePeriod);
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
                case nameof(this.SelectedBankAccount):
                case nameof(this.CurrentDate):
                    //On met à jour la collection graphique en fonction du compte sélectionné et de la date du filtre.
                    //On passe par une collection temporraire pour filtrer la vue graphique.
                    this.ItemsSource = this.SelectedBankAccount == null ? null : new ObservableCollection<BankAccountLine>(this.SelectedBankAccount.BankAccountLines.Where(bal => bal.Date.Year == this.CurrentDate.Year && bal.Date.Month == this.CurrentDate.Month));
                    break;
                default:
                    break;
            }
        }

        #region ChangePeriod

        /// <summary>
        ///     Test si la commande <see cref="ChangePeriod"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        private bool CanExecuteChangePeriod(object param) => param is string paramString && (paramString == "+" || paramString == "-");

        /// <summary>
        ///     Exécute la commande <see cref="ChangePeriod"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        private void ExecuteChangePeriod(object param)
        {
            if (param is string paramString)
            {
                if (paramString == "+")
                {
                    this.CurrentDate = this.CurrentDate.AddMonths(1);
                }
                else if(paramString == "-")
                {
                    this.CurrentDate = this.CurrentDate.AddMonths(-1);
                }
            }
        }

        #endregion



        #region AddItem

        /// <summary>
        ///     Test si la commande <see cref="AddItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteAddItem(object param) => this.SelectedBankAccount != null;

        /// <summary>
        ///     Retourne une nouvelle instance (appelée lors de l'exécution de la commande <see cref="AddItem"/>).
        /// </summary>
        /// <param name="param">Paramètre de la commande <see cref="AddItem"/>.</param>
        /// <returns>Nouvelle instance.</returns>
        protected override BankAccountLine CreateInstance(object param)
        {
            BankAccountLine bal = new BankAccountLine()
            {
                Date = this.CurrentDate,
                Identifier = Guid.NewGuid(),
                IdentifierBankAccount = this.SelectedBankAccount.Identifier
            };

            this.SelectedBankAccount.BankAccountLines.Add(bal);
            App.DataStore.BankAccountLines.Add(bal);

            return bal;
        }

        #endregion

        #region DeleteItem

        /// <summary>
        ///     Test si la commande <see cref="DeleteItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteDeleteItem(object param) => this.SelectedBankAccount != null && this.SelectedItem != null;

        /// <summary>
        ///     Exécute la commande <see cref="DeleteItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteDeleteItem(object param)
        {
            this.SelectedBankAccount.BankAccountLines.Remove(this.SelectedItem);
            App.DataStore.BankAccountLines.Remove(this.SelectedItem);
            base.ExecuteDeleteItem(param);
        }

        #endregion




        #endregion
    }
}
