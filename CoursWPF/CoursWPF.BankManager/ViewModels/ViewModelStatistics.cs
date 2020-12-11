using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    public class ViewModelStatistics : ObservableObject, IAddItemDeleteItem
    {
        public string Title { get; set; }

        public RelayCommand AddItem => null;

        public RelayCommand DeleteItem => null;

        public ViewModelStatistics()
        {
            this.Title = "Statistiques";
        }
    }
}
