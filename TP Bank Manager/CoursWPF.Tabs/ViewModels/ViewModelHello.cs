using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.Tabs.ViewModels
{
    public class ViewModelHello : ObservableObject
    {
        #region Fields

        private string _Title;
        private string _Data;

        #endregion

        #region Properties

        public string Title
        {
            get => this._Title;
            set => this.SetProperty(nameof(this.Title), ref this._Title, value);
        }

        public string Data
        {
            get => this._Data;
            set => this.SetProperty(nameof(this.Data), ref this._Data, value);
        }

        #endregion

        public ViewModelHello()
        {
            this.Title = "Hello";
            this.Data = "World";
        }
    }
}
