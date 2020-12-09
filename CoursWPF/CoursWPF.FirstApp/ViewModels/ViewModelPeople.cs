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
    ///     ViewModel pour gérer une liste de <see cref="Person"/>.
    /// </summary>
    public class ViewModelPeople : ViewModelList<Person>, IViewModelPeople
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelPeople"/>.
        /// </summary>
        public ViewModelPeople()
        {
            this.Title = "Personnes";
            this.ItemsSource.Add(new Person()
            {
                FirstName = "Benjamin",
                LastName = "DAGUÉ"
            });
            this.ItemsSource.Add(new Person()
            {
                FirstName = "Peter",
                LastName = "BAUDRY"
            });
        }

        #endregion

        #region Methods

        protected override Person CreateInstance(object param) => new Person();

        #endregion
    }
}
