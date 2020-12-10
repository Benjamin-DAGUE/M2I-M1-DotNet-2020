using CoursWPF.BankManager.Models;
using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     Vue-modèle pour la liste des écritures d'un compte bancaire.
    /// </summary>
    public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
    {
        #region Fields

        /// <summary>
        ///     Compte bancaire associé.
        /// </summary>
        private BankAccount _BankAccount;

        /// <summary>
        ///     Date de la vue filtrée.
        /// </summary>
        private DateTime _CurrentDate;

        /// <summary>
        ///     Commande de changement de période.
        /// </summary>
        private RelayCommand _ChangePeriodCommand;

        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Obtient ou définit le comtpe bancaire associé.
        /// </summary>
        public BankAccount BankAccount { get => this._BankAccount; set => this.SetProperty(nameof(this.BankAccount), ref this._BankAccount, value); }

        /// <summary>
        ///     Obtient la date de la vue filtrée.
        /// </summary>
        public DateTime CurrentDate { get => this._CurrentDate; set => this.SetProperty(nameof(this.CurrentDate), ref this._CurrentDate, value); }

        /// <summary>
        ///     Obtient la collection des catégories disponibles.
        /// </summary>
        public ObservableCollection<Category> Categories => this.DataContext.GetItems<Category>();

        /// <summary>
        ///     Obtient la commande de changement de période.
        /// </summary>
        public RelayCommand ChangePeriodCommand => this._ChangePeriodCommand;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelBankAccountLines"/>.
        /// </summary>
        /// <param name="dataContext">Contexte de données à utiliser.</param>
        public ViewModelBankAccountLines(IDataContext dataContext)
            : base(dataContext)
        {
            this._CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this._ChangePeriodCommand = new RelayCommand(this.ChangePeriod, this.CanChangePeriod);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Méthode de chargement des données.
        /// </summary>
        public override void LoadData()
        {
            this.ItemsSource = this.BankAccount == null ? new ObservableCollection<BankAccountLine>() : new ObservableCollection<BankAccountLine>(this.DataContext.GetItems<BankAccountLine>().Where(bal => bal.IdentifierBankAccount == this.BankAccount.Identifier && bal.Date >= this.CurrentDate && bal.Date < this.CurrentDate.AddMonths(1)));
        }

        /// <summary>
        ///     Déclenche l'événement <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changée.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.CurrentDate):
                case nameof(this.BankAccount):
                    this.LoadData();
                    break;
                default:
                    break;
            }
        }

        #region AddCommand

        /// <summary>
        ///     Méthode d'exécution de la commande <see cref="AddCommand"/>.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        protected override void Add(object parameter)
        {
            base.Add(parameter);

            this.SelectedItem.Date = this.CurrentDate;
            this.BankAccount.BankAccountLines.Add(this.SelectedItem);
            this.SelectedItem.Identifier = this.DataContext.GetItems<BankAccountLine>().Max(bal => bal.Identifier) + 1;
            this.SelectedItem.IdentifierBankAccount = this.BankAccount.Identifier;
            this.SelectedItem.BankAccount = this.BankAccount;
        }

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="AddCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanAdd(object parameter) => this.BankAccount != null;

        #endregion

        #region DeleteCommand

        protected override void Delete(object parameter)
        {
            this.BankAccount.BankAccountLines.Remove(this.SelectedItem);
            base.Delete(parameter);
        }

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="DeleteCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanDelete(object parameter) => this.BankAccount != null && base.CanDelete(parameter);

        #endregion

        #region ChangePeriodCommand

        /// <summary>
        ///     Méthode d'exécution de la commande <see cref="ChangePeriodCommand"/>.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        protected virtual void ChangePeriod(object parameter)
        {
            if (parameter as string == "-1")
            {
                this.CurrentDate = this.CurrentDate.AddMonths(-1);
            }
            else if (parameter as string == "1")
            {
                this.CurrentDate = this.CurrentDate.AddMonths(1);
            }
        }

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="ChangePeriodCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanChangePeriod(object parameter) => this.BankAccount != null && (parameter as string == "-1" || parameter as string == "1");

        #endregion

        #endregion
    }
}
