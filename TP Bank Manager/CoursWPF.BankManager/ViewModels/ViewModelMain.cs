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
    ///     Vue-modèle principal de l'application.
    /// </summary>
    public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
    {
        #region Fields

        /// <summary>
        ///     Fournisseur de service de l'application.
        /// </summary>
        private readonly IServiceProvider _ServiceProvider;

        /// <summary>
        ///     Vue-modèle de la page de gestion des comptes.
        /// </summary>
        private IViewModelAccounting _ViewModelAccounting;

        /// <summary>
        ///     Vue-modèle de la page des statistiques.
        /// </summary>
        private IViewModelStatistics _ViewModelStatistics;

        /// <summary>
        ///     Vue-modèle de la page d'administration.
        /// </summary>
        private IViewModelAdministration _ViewModelAdministration;

        /// <summary>
        ///     Commande pour fermer l'application.
        /// </summary>
        private readonly RelayCommand _ExitCommand;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le vue-modèle de la page de gestion des comptes.
        /// </summary>
        public IViewModelAccounting ViewModelAccounting { get => this._ViewModelAccounting; private set => this.SetProperty(nameof(this.ViewModelAccounting), ref this._ViewModelAccounting, value); }

        /// <summary>
        ///     Obtient ou définit le vue-modèle de la page des statistiques.
        /// </summary>
        public IViewModelStatistics ViewModelStatistics { get => this._ViewModelStatistics; private set => this.SetProperty(nameof(this.ViewModelStatistics), ref this._ViewModelStatistics, value); }

        /// <summary>
        ///     Obtient ou définit le vue-modèle de la page d'administration.
        /// </summary>
        public IViewModelAdministration ViewModelAdministration { get => this._ViewModelAdministration; private set => this.SetProperty(nameof(this.ViewModelAdministration), ref this._ViewModelAdministration, value); }

        /// <summary>
        ///     Obtient la commande pour fermer l'application.
        /// </summary>
        public RelayCommand ExitCommand => this._ExitCommand;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelMain"/>.
        /// </summary>
        /// <param name="serviceProvider">Fournisseur de service de l'application.</param>
        public ViewModelMain(IServiceProvider serviceProvider)
            : base(serviceProvider.GetService<IDataContext>())
        {
            this._ServiceProvider = serviceProvider;

            this._ViewModelAccounting = this._ServiceProvider.GetService<IViewModelAccounting>();
            this._ViewModelStatistics = this._ServiceProvider.GetService<IViewModelStatistics>();
            this._ViewModelAdministration = this._ServiceProvider.GetService<IViewModelAdministration>();

            this._ExitCommand = new RelayCommand(this.Exit, this.CanExit);
            this.LoadData();
        }

        #endregion

        #region Methods

        public override void LoadData()
        {
            this.ItemsSource = new ObservableCollection<IObservableObject>(new IObservableObject[] { this._ViewModelAccounting, this._ViewModelStatistics, this._ViewModelAdministration });
            this.SelectedItem = this._ViewModelAccounting;
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
                case nameof(this.SelectedItem):
                    (this.SelectedItem as IViewModelList<IDataContext>)?.LoadData();
                    break;
                default:
                    break;
            }
        }

        #region ExitCommand

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="ExitCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected virtual bool CanExit(object parameter) => true;

        /// <summary>
        ///     Méthode d'exécution de la commande <see cref="ExitCommand"/>.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        protected virtual void Exit(object parameter)
        {
            App.Current.Shutdown(0);
        }

        #endregion

        #region AddCommand

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="AddCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanAdd(object parameter) => false;

        #endregion

        #region DeleteCommand

        /// <summary>
        ///     Methode qui détermine si la commande <see cref="DeleteCommand"/> peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanDelete(object parameter) => false;

        #endregion

        #endregion
    }
}
