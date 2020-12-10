using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoursWPF.Tabs.ViewModels
{
    public class ViewModelMain : ObservableObject
    {
        #region Fields

        private ViewModelHello _ViewModelHello;
        private ViewModelWorld _ViewModelWorld;
        private ObservableCollection<ObservableObject> _ViewModels;

        #endregion

        #region Properties
        public ViewModelHello ViewModelHello
        {
            get => this._ViewModelHello;
            private set => this.SetProperty(nameof(this.ViewModelHello), ref this._ViewModelHello, value);
        }

        public ViewModelWorld ViewModelWorld
        {
            get => this._ViewModelWorld;
            private set => this.SetProperty(nameof(this.ViewModelWorld), ref this._ViewModelWorld, value);
        }

        public ObservableCollection<ObservableObject> ViewModels
        {
            get => this._ViewModels;
            private set => this.SetProperty(nameof(this.ViewModels), ref this._ViewModels, value);
        }

        #endregion

        public ViewModelMain()
        {
            this.ViewModels = new ObservableCollection<ObservableObject>();
            this.ViewModelHello = new ViewModelHello();
            this.ViewModelWorld = new ViewModelWorld();

            this.ViewModels.Add(ViewModelHello);
            this.ViewModels.Add(ViewModelWorld);
        }
    }
}
