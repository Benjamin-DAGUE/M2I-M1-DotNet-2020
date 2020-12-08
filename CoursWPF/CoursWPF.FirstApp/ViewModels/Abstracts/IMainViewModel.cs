using CoursWPF.FirstApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.FirstApp.ViewModels.Abstracts
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Obtient la liste des personnes
        /// </summary>
        ObservableCollection<Person> People { get; }

        /// <summary>
        ///     Obtient ou définit la personne sélectionnée.
        /// </summary>
        Person SelectedPerson { get; set; }
    }
}
