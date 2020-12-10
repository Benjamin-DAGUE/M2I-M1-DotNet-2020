using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.ViewModels
{
    public interface IAddItemDeleteItem
    {
        RelayCommand AddItem { get; }
        RelayCommand DeleteItem { get; }
    }
}
