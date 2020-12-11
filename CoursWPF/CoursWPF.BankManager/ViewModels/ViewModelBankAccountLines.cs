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
    public class ViewModelBankAccountLines : ViewModelList<BankAccountLine>
    {
        #region Fields

        BankAccount _SelectedBankAccount;

        DateTime _CurrentDate;

        RelayCommand _ChangePeriod;

        #endregion

        #region Properties

        public BankAccount SelectedBankAccount
        {
            get => this._SelectedBankAccount;
            set => this.SetProperty(nameof(this.SelectedBankAccount), ref this._SelectedBankAccount, value);
        }

        public DateTime CurrentDate
        {
            get => this._CurrentDate;
            private set => this.SetProperty(nameof(this.CurrentDate), ref this._CurrentDate, value);
        }

        public RelayCommand ChangePeriod => this._ChangePeriod;

        #endregion

        #region Constructors

        public ViewModelBankAccountLines()
        {
            this.ItemsSource = null;
            this.CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this._ChangePeriod = new RelayCommand(this.ExecuteChangePeriod, this.CanExecuteChangePeriod);
        }

        #endregion

        #region Methods

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedBankAccount):
                case nameof(this.CurrentDate):
                    this.ItemsSource = this.SelectedBankAccount == null ? null : new ObservableCollection<BankAccountLine>(this.SelectedBankAccount.BankAccountLines.Where(bal => bal.Date.Year == this.CurrentDate.Year && bal.Date.Month == this.CurrentDate.Month));
                    break;
                default:
                    break;
            }
        }

        #region ChangePeriod

        private bool CanExecuteChangePeriod(object param) => param is string paramString && (paramString == "+" || paramString == "-");

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

        protected override bool CanExecuteAddItem(object param) => this.SelectedBankAccount != null;

        protected override bool CanExecuteDeleteItem(object param) => this.SelectedBankAccount != null && this.SelectedItem != null;

        protected override void ExecuteDeleteItem(object param)
        {
            this.SelectedBankAccount.BankAccountLines.Remove(this.SelectedItem);
            App.DataStore.BankAccountLines.Remove(this.SelectedItem);
            base.ExecuteDeleteItem(param);
        }

        #endregion
    }
}
