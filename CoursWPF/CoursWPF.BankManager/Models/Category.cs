using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.Models
{
    public class Category : ObservableObject
    {
        #region Fields

        private string _Label;

        #endregion

        #region Properties

        public string Label
        {
            get => this._Label;
            set => this.SetProperty(nameof(this.Label), ref this._Label, value);
        }

        #endregion

    }
}
