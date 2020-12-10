using CoursWPF.AddressBook.Client.Models;
using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models.Abstracts;
using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.AddressBook.Client.ViewModels.Abstracts
{
    /// <summary>
    ///     Interface du vue-modèle pour la liste des personnes.
    /// </summary>
    public interface IViewModelPeople : IViewModelList<Person, IDataContext>
    {

    }
}
