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
    public class ViewModelCategories : ViewModelList<Category>, IAddItemDeleteItem
    {
        #region Constructors

        public ViewModelCategories()
        {
            this.Title = "Catégories";
            this.ItemsSource.Add(new Category() { Label = "Loyer" });
            this.ItemsSource.Add(new Category() { Label = "Loisirs" });
        }

        #endregion

        #region Methods

        #region AddItem

        protected override Category CreateInstance(object param) => new Category();

        #endregion

        #endregion
    }
}
