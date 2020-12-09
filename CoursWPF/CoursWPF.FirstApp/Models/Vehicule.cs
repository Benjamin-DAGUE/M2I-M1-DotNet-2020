using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.Models
{
    public class Vehicule : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Plaque d'immatriculation.
        /// </summary>
        private string _LicensePlate;

        #endregion

        /// <summary>
        ///     Obtient ou définit la plaque d'immatriculation
        /// </summary>
        public string LicensePlate
        {
            get => this._LicensePlate;
            set => this.SetProperty(nameof(this.LicensePlate), ref this._LicensePlate, value);
        }
    }
}
