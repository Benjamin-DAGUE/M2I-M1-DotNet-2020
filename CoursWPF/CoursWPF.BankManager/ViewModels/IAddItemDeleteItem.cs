using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    /// <summary>
    ///     Interface d'une classe qui prend en charge une commande d'ajout et une commande de suppression.
    /// </summary>
    public interface IAddItemDeleteItem
    {
        /// <summary>
        ///     Commande pour ajouter un élément.
        /// </summary>
        RelayCommand AddItem { get; }

        /// <summary>
        ///     Commande pour supprimer une élément.
        /// </summary>
        RelayCommand DeleteItem { get; }
    }
}
