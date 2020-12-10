using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    public class ViewModelMain : ObservableObject, IAddItemDeleteItem
    {
        #region Fields

        private ViewModelAccounting _ViewModelAccounting;

        private ViewModelStatistics _ViewModelStatistics;

        private ViewModelAdmin _ViewModelAdmin;

        private IAddItemDeleteItem _SelectedViewModel;

        private RelayCommand _Exit;

        private RelayCommand _AddItem;

        private RelayCommand _DeleteItem;

        #endregion

        #region Properties

        public ViewModelAccounting ViewModelAccounting 
        { 
            get => this._ViewModelAccounting; 
            set => this._ViewModelAccounting = value; 
        }

        public ViewModelStatistics ViewModelStatistics 
        {
            get => this._ViewModelStatistics;
            set => this._ViewModelStatistics = value; 
        }
        
        public ViewModelAdmin ViewModelAdmin 
        { 
            get => this._ViewModelAdmin; 
            set => this._ViewModelAdmin = value; 
        }

        public IAddItemDeleteItem SelectedViewModel
        {
            get => this._SelectedViewModel;
            set => this.SetProperty(nameof(this.SelectedViewModel), ref this._SelectedViewModel, value);
        }

        public RelayCommand Exit => this._Exit;

        public RelayCommand AddItem => this._AddItem;

        public RelayCommand DeleteItem => this._DeleteItem;

        #endregion

        #region Constructors

        public ViewModelMain()
        {
            this.ViewModelAccounting = new ViewModelAccounting();
            this.ViewModelStatistics = new ViewModelStatistics();
            this.ViewModelAdmin = new ViewModelAdmin();
            this._Exit = new RelayCommand((param) => Environment.Exit(0));
            this._AddItem = new RelayCommand(this.ExecuteAddItem, this.CanExecuteAddItem);
            this._DeleteItem = new RelayCommand(this.ExecuteDeleteItem, this.CanExecuteDeleteItem);

        }

        #endregion

        #region Methods

        #region AddItem

        private bool CanExecuteAddItem(object param) => this.SelectedViewModel?.AddItem?.CanExecute(param) == true;

        private void ExecuteAddItem(object param) => this.SelectedViewModel?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        private bool CanExecuteDeleteItem(object param) => this.SelectedViewModel?.DeleteItem?.CanExecute(param) == true;

        private void ExecuteDeleteItem(object param) => this.SelectedViewModel?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
