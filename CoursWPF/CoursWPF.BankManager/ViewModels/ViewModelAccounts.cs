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
            this.ItemsSource = App.DataStore.BankAccounts;
        }

        #endregion

        #region Methods

        #region AddItem

        protected override BankAccount CreateInstance(object param) => new BankAccount()
        {
            Identifier = Guid.NewGuid()
        };

        #endregion

        #endregion
    }
}
