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
    public class ViewModelPeople : ViewModelList<Person>, IViewModelPeople
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="ViewModelPeople"/>.
        /// </summary>
        public ViewModelPeople()
        {
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
    }
}
