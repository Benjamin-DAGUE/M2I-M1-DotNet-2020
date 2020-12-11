using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     ViewModel pour les statistiques.
    /// </summary>
    public class ViewModelStatistics : ObservableObject, IAddItemDeleteItem
    {
        #region Properties

        /// <summary>
        ///     Titre du ViewModel.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Commande pour ajouter un élément.
        /// </summary>
        public RelayCommand AddItem => null;

        /// <summary>
        ///     Commande pour supprimer un élément.
        /// </summary>
        public RelayCommand DeleteItem => null;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelStatistics"/>.
        /// </summary>
        public ViewModelStatistics()
        {
            this.Title = "Statistiques";
        }

        #endregion
    }
}
