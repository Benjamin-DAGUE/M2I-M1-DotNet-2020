using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.MVVM;
using CoursWPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels
{
    /// <summary>
    ///     ViewModel pour gérer une liste de <see cref="Vehicule"/>.
    /// </summary>
    public class ViewModelVehicules : ViewModelList<Vehicule>, IViewModelVehicules
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelVehicules"/>.
        /// </summary>
        public ViewModelVehicules()
        {
            this.ItemsSource.Add(new Vehicule()
            {
                LicensePlate = "xx-001-xx"
            });
            this.ItemsSource.Add(new Vehicule()
            {
                LicensePlate = "xx-002-xx"
            });
        }

        #endregion
    }
}
