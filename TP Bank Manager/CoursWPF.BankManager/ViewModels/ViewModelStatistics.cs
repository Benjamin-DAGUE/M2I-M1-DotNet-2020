using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.BankManager.ViewModels
{
    public class ViewModelStatistics : ObservableObject, IViewModelStatistics
    {
        /// <summary>
        ///     Obtient le titre du vue-modèle
        /// </summary>
        public string Title => "Statistiques";
    }
}
