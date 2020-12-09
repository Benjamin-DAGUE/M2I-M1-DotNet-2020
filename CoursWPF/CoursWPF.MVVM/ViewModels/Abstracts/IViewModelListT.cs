using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.MVVM.ViewModels.Abstracts
{
    public interface IViewModelList<T>
    {
        ObservableCollection<T> ItemsSource { get; }

        T SelectedItem { get; set; }

        RelayCommand AddItem { get; }

        RelayCommand DeleteItem { get; }
    }
}
