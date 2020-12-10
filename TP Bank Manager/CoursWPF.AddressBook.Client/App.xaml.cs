using CoursWPF.AddressBook.Client.Models;
using CoursWPF.AddressBook.Client.ViewModels;
using CoursWPF.AddressBook.Client.ViewModels.Abstracts;
using CoursWPF.AddressBook.Client.Views;
using CoursWPF.MVVM.Models;
using CoursWPF.MVVM.Models.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CoursWPF.AddressBook.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            //Création du contexte de données de l'application.
            serviceCollection.AddSingleton<IDataContext, AddressBookContext>(sp => FileDataContext.Load(@"C:\Temp\data.json", new AddressBookContext(@"C:\Temp\data.json")));
            
            //Création du vue-modèle de personnes.
            serviceCollection.AddTransient<IViewModelPeople, ViewModelPeople>(sp => new ViewModelPeople(sp.GetService<IDataContext>()));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            MainWindow window = new MainWindow(serviceProvider.GetService<IViewModelPeople>());
            window.Show();
        }
    }
}
