using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    public class ViewModelMain : ViewModelList<IAddItemDeleteItem>, IAddItemDeleteItem
    {
        #region Fields

        private ViewModelAccounting _ViewModelAccounting;

        private ViewModelStatistics _ViewModelStatistics;

        private ViewModelAdmin _ViewModelAdmin;

        private RelayCommand _Exit;

        private RelayCommand _Save;

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

        public RelayCommand Exit => this._Exit;

        public RelayCommand Save => this._Save;

        #endregion

        #region Constructors

        public ViewModelMain()
        {
            this.ViewModelAccounting = new ViewModelAccounting();
            this.ViewModelStatistics = new ViewModelStatistics();
            this.ViewModelAdmin = new ViewModelAdmin();

            this.ItemsSource.Add(this.ViewModelAccounting);
            this.ItemsSource.Add(this.ViewModelStatistics);
            this.ItemsSource.Add(this.ViewModelAdmin);

            this.SelectedItem = this.ViewModelAccounting;

            this._Exit = new RelayCommand((param) => Environment.Exit(0));
            this._Save = new RelayCommand((param) => App.DataStore.Save());
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
