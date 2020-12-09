using CoursWPF.FirstApp.ViewModels;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using CoursWPF.FirstApp.Views.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoursWPF.FirstApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private static ServiceProvider _ServiceProvider;

        #endregion

        #region Properties

        public static ServiceProvider ServiceProvider => _ServiceProvider;

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection service = new ServiceCollection();

            service.AddTransient<IViewModelPeople>(sp => new ViewModelPeople());
            service.AddTransient<IViewModelVehicules>(sp => new ViewModelVehicules());
            service.AddSingleton<IViewModelMainStatic>(sp => new ViewModelMainStatic());
            service.AddSingleton<IViewModelMainDynamic>(sp => new ViewModelMainDynamic());
            service.AddSingleton<IStaticMainWindow>(sp => new StaticMainWindow());
            service.AddSingleton<IDynamicMainWindow>(sp => new DynamicMainWindow());

            _ServiceProvider = service.BuildServiceProvider();

            this.MainWindow = _ServiceProvider.GetService<IStaticMainWindow>() as Window;
            //this.MainWindow = _ServiceProvider.GetService<IDynamicMainWindow>() as Window;

            this.MainWindow.Show();
        }

        #endregion
    }
}
