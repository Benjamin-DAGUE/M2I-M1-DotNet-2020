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
    public class ViewModelAccounts : ViewModelList<BankAccount>, IAddItemDeleteItem
    {
        #region Constructors

        public ViewModelAccounts()
        {
            this.Title = "Comptes";
            this.ItemsSource.Add(new BankAccount() { });
            this.ItemsSource.Add(new BankAccount() { });
        }

        #endregion

        #region Methods

        #region AddItem

        protected override BankAccount CreateInstance(object param) => new BankAccount();

        #endregion

        #endregion
    }
}
