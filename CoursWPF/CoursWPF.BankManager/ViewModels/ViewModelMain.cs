using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     ViewModel principal de l'application.
    /// </summary>
    public class ViewModelMain : ViewModelList<IAddItemDeleteItem>, IAddItemDeleteItem
    {
        #region Fields

        /// <summary>
        ///     ViewModel pour la gestion des comptes bancaires.
        /// </summary>
        private readonly ViewModelAccounting _ViewModelAccounting;

        /// <summary>
        ///     ViewModel pour les statistiques.
        /// </summary>
        private readonly ViewModelStatistics _ViewModelStatistics;

        /// <summary>
        ///     ViewModel pour l'onglet Administration dans le Tab principal.
        /// </summary>
        private readonly ViewModelAdmin _ViewModelAdmin;

        /// <summary>
        ///     Commande pour quitter l'application.
        /// </summary>
        private readonly RelayCommand _Exit;

        /// <summary>
        ///     Commande pour sauvegarder les données.
        /// </summary>
        private readonly RelayCommand _Save;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient le ViewModel pour la gestion des comptes bancaires.
        /// </summary>
        public ViewModelAccounting ViewModelAccounting => this._ViewModelAccounting;

        /// <summary>
        ///     Obtient le ViewModel pour l'onglet Administration dans le Tab principal.
        /// </summary>
        public ViewModelStatistics ViewModelStatistics => this._ViewModelStatistics;

        /// <summary>
        ///     Obtient le ViewModel pour l'onglet Administration dans le Tab principal.
        /// </summary>
        public ViewModelAdmin ViewModelAdmin => this._ViewModelAdmin;

        /// <summary>
        ///     Obtient la commande pour quitter l'application.
        /// </summary>
        public RelayCommand Exit => this._Exit;

        /// <summary>
        ///     Obtient la commande pour sauvegarder l'application.
        /// </summary>
        public RelayCommand Save => this._Save;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelMain"/>.
        /// </summary>
        public ViewModelMain()
        {
            this._ViewModelAccounting = new ViewModelAccounting();
            this._ViewModelStatistics = new ViewModelStatistics();
            this._ViewModelAdmin = new ViewModelAdmin();

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

        /// <summary>
        ///     Test si la commande <see cref="AddItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteAddItem(object param) => this.SelectedItem?.AddItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="AddItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteAddItem(object param) => this.SelectedItem?.AddItem?.Execute(param);

        #endregion

        #region DeleteItem

        /// <summary>
        ///     Test si la commande <see cref="DeleteItem"/> peut être exécutée.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        protected override bool CanExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.CanExecute(param) == true;

        /// <summary>
        ///     Exécute la commande <see cref="DeleteItem"/>.
        /// </summary>
        /// <param name="param">Paramètre de la commande.</param>
        protected override void ExecuteDeleteItem(object param) => this.SelectedItem?.DeleteItem?.Execute(param);

        #endregion

        #endregion
    }
}
