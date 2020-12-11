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
    /// <summary>
    ///     ViewModel pour l'administration des catégories.
    /// </summary>
    public class ViewModelCategories : ViewModelList<Category>, IAddItemDeleteItem
    {
        #region Constructors
        
        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelCategories"/>
        /// </summary>
        public ViewModelCategories()
        {
            this.Title = "Catégories";
            this.ItemsSource = App.DataStore.Categories;
        }

        #endregion

        #region Methods

        #region AddItem

        /// <summary>
        ///     Retourne une nouvelle instance (appelée lors de l'exécution de la commande <see cref="AddItem"/>).
        /// </summary>
        /// <param name="param">Paramètre de la commande <see cref="AddItem"/>.</param>
        /// <returns>Nouvelle instance.</returns>
        protected override Category CreateInstance(object param) => new Category()
        {
            Identifier = Guid.NewGuid()
        };

        #endregion

        #endregion
    }
}
