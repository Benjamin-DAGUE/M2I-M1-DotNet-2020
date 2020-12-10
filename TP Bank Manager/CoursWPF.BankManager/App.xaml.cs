using CoursWPF.BankManager.Models;
using CoursWPF.BankManager.ViewModels;
using CoursWPF.BankManager.ViewModels.Abstracts;
using CoursWPF.BankManager.Views;
using CoursWPF.MVVM.Models;
using CoursWPF.MVVM.Models.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace CoursWPF.BankManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            ServiceCollection serviceCollection = new ServiceCollection();

            //Création du contexte de données de l'application.
            serviceCollection.AddSingleton<IDataContext, BankManagerContext>(sp => FileDataContext.Load(@"C:\Temp\data.json", new BankManagerContext(@"C:\Temp\data.json")));

            //Création du vue-modèle principal.
            serviceCollection.AddTransient<IViewModelMain, ViewModelMain>(sp => new ViewModelMain(sp));
            serviceCollection.AddTransient<IViewModelAccounting, ViewModelAccounting>(sp => new ViewModelAccounting(sp));
            serviceCollection.AddTransient<IViewModelStatistics, ViewModelStatistics>(sp => new ViewModelStatistics());
            serviceCollection.AddTransient<IViewModelAdministration, ViewModelAdministration>(sp => new ViewModelAdministration(sp));
            serviceCollection.AddTransient<IViewModelBankAccounts, ViewModelBankAccounts>(sp => new ViewModelBankAccounts(sp.GetService<IDataContext>()));
            serviceCollection.AddTransient<IViewModelBankAccountLines, ViewModelBankAccountLines>(sp => new ViewModelBankAccountLines(sp.GetService<IDataContext>()));
            serviceCollection.AddTransient<IViewModelCategories, ViewModelCategories>(sp => new ViewModelCategories(sp.GetService<IDataContext>()));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            MainWindow window = new MainWindow();
            window.DataContext = serviceProvider.GetService<IViewModelMain>();
            window.Show();
        }
    }
}
