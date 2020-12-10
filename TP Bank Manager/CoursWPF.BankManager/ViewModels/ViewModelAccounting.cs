using CoursWPF.BankManager.Models;
using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     Vue-modèle pour l'affichage des comptes dans la page de gestion des écritures dans un compte.
    /// </summary>
    public class ViewModelAccounting : ViewModelList<BankAccount, IDataContext>, IViewModelAccounting
    {
        #region Fields

        /// <summary>
        ///     Fournisseur de service de l'application.
        /// </summary>
        private readonly IServiceProvider _ServiceProvider;

        /// <summary>
        ///     Vue-modèle pour la liste des écritures bancaires.
        /// </summary>
        private IViewModelBankAccountLines _ViewModelBankAccountLines;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le vue-modèle pour la liste des écritures associées.
        /// </summary>
        public IViewModelBankAccountLines ViewModelBankAccountLines { get => this._ViewModelBankAccountLines; private set => this.SetProperty(nameof(this.ViewModelBankAccountLines), ref this._ViewModelBankAccountLines, value); }

        /// <summary>
        ///     Obtient la commande pour ajouter un élément.
        /// </summary>
        public override RelayCommand AddCommand => this.ViewModelBankAccountLines.AddCommand;

        /// <summary>
        ///     Obtient la commande pour supprimer un élément passé en paramètre ou l'élément sélectionné.
        /// </summary>
        public override RelayCommand DeleteCommand => this.ViewModelBankAccountLines.DeleteCommand;

        /// <summary>
        ///     Obtient le titre du vue-modèle
        /// </summary>
        public string Title => "Comptes";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelAccounting"/>.
        /// </summary>
        /// <param name="serviceProvider">Fournisseur de service de l'application.</param>
        public ViewModelAccounting(IServiceProvider serviceProvider)
            : base(serviceProvider.GetService<IDataContext>())
        {
            this._ServiceProvider = serviceProvider;
            this.ViewModelBankAccountLines = this._ServiceProvider.GetService<IViewModelBankAccountLines>();
            this.LoadData();
        }

        #endregion

        #region Methods

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
                    this.ViewModelBankAccountLines.BankAccount = this.SelectedItem;
                    break;
                default:
                    break;
            }
        }

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
