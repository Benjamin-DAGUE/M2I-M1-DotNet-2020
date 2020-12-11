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
    public class ViewModelAccounting : ViewModelList<BankAccount>, IAddItemDeleteItem
    {
        #region Fields

        private ViewModelBankAccountLines _ViewModelBankAccountLines;

        #endregion

        #region Properties

        public ViewModelBankAccountLines ViewModelBankAccountLines
        {
            get => this._ViewModelBankAccountLines;
            private set => this.SetProperty(nameof(this._ViewModelBankAccountLines), ref this._ViewModelBankAccountLines, value);
        }

        #endregion

        #region Constructors

        public ViewModelAccounting()
        {
            this.Title = "Comptes";
            this.ItemsSource = App.DataStore.BankAccounts;
            this.ViewModelBankAccountLines = new ViewModelBankAccountLines();
        }

        #endregion

        #region Methods

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    this.ViewModelBankAccountLines.SelectedBankAccount = this.SelectedItem;
                    break;
                default:
                    break;
            }
        }

        #region AddItem

        protected override bool CanExecuteAddItem(object param) => this.ViewModelBankAccountLines?.AddItem?.CanExecute(param) == true;

        protected override void ExecuteAddItem(object param) => this.ViewModelBankAccountLines?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        protected override bool CanExecuteDeleteItem(object param) => this.ViewModelBankAccountLines?.DeleteItem?.CanExecute(param) == true;

        protected override void ExecuteDeleteItem(object param) => this.ViewModelBankAccountLines?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
