using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    public class ViewModelAdmin : ViewModelList<IAddItemDeleteItem>, IAddItemDeleteItem
    {
        #region Fields

        private ViewModelAccounts _ViewModelAccounts;

        private ViewModelCategories _ViewModelCategories;

        #endregion

        #region Properties

        public ViewModelAccounts ViewModelAccounts
        {
            get => this._ViewModelAccounts;
            set => this._ViewModelAccounts = value;
        }

        public ViewModelCategories ViewModelCategories
        {
            get => this._ViewModelCategories;
            set => this._ViewModelCategories = value;
        }

        #endregion

        #region Constructors

        public ViewModelAdmin()
        {
            this.Title = "Administration";
            this.ViewModelAccounts = new ViewModelAccounts();
            this.ViewModelCategories = new ViewModelCategories();

            this.ItemsSource.Add(this.ViewModelAccounts);
            this.ItemsSource.Add(this.ViewModelCategories);
        }

        #endregion

        #region Methods

        #region AddItem

        protected override bool CanExecuteAddItem(object param) => this.SelectedItem?.AddItem?.CanExecute(param) == true;

        protected override void ExecuteAddItem(object param) => this.SelectedItem?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        protected override bool CanExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.CanExecute(param) == true;

        protected override void ExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
