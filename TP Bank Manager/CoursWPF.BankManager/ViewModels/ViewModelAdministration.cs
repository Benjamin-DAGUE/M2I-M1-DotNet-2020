using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels;
using CoursWPF.MVVM.ViewModels.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     Vue-modèle de la page d'administration.
    /// </summary>
    public class ViewModelAdministration : ViewModelList<IObservableObject, IDataContext>, IViewModelAdministration
    {
        #region Fields

        /// <summary>
        ///     Vue-modèle de la liste des comptes bancaires.
        /// </summary>
        private IViewModelBankAccounts _ViewModelBankAccounts;
        
        /// <summary>
        ///     Vue-modèle de la liste des catégories.
        /// </summary>
        private IViewModelCategories _ViewModelCategories;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le vue-modèle de la liste des comptes bancaires.
        /// </summary>
        public IViewModelBankAccounts ViewModelBankAccounts { get => this._ViewModelBankAccounts; private set => this.SetProperty(nameof(this.ViewModelBankAccounts), ref this._ViewModelBankAccounts, value); }

        /// <summary>
        ///     Obtient le vue-modèle de la liste des catégories.
        /// </summary>
        public IViewModelCategories ViewModelCategories { get => this._ViewModelCategories; private set => this.SetProperty(nameof(this.ViewModelCategories), ref this._ViewModelCategories, value); }

        public override RelayCommand AddCommand => (this.SelectedItem as IViewModelList<IDataContext>)?.AddCommand ?? base.AddCommand;

        public override RelayCommand DeleteCommand => (this.SelectedItem as IViewModelList<IDataContext>)?.DeleteCommand ?? base.DeleteCommand;

        /// <summary>
        ///     Obtient le titre du vue-modèle
        /// </summary>
        public string Title => "Administration";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelAdministration"/>
        /// </summary>
        public ViewModelAdministration(IServiceProvider serviceProvider)
            : base(serviceProvider.GetService<IDataContext>())
        {
            this._ViewModelBankAccounts = serviceProvider.GetService<IViewModelBankAccounts>();
            this._ViewModelCategories = serviceProvider.GetService<IViewModelCategories>();

            this.LoadData();
        }

        #endregion

        #region Methods

        public override void LoadData()
        {
            this.ItemsSource = new ObservableCollection<IObservableObject>(new IObservableObject[] { this._ViewModelBankAccounts, this.ViewModelCategories });
            this.SelectedItem = this._ViewModelBankAccounts;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.SelectedItem):
                    this.OnPropertyChanged(nameof(this.AddCommand));
                    this.OnPropertyChanged(nameof(this.DeleteCommand));
                    break;
                default:
                    break;
            }
        }

        #region AddCommand

        protected override bool CanAdd(object parameter) => false;

        #endregion

        #endregion
    }
}