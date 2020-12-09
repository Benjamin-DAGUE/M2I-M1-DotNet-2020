using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels;
using CoursWPF.FirstApp.ViewModels.Abstracts;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace CoursWPF.FirstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Constructors

        public MainWindow()
        {
            this.InitializeComponent();

            //On initialise le DataContext à la création de la MainWindow.
            //this.DataContext = App.ServiceProvider.GetService<IMainViewModel>();
        }

        #endregion

        #region Methods

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.DataContext is IMainViewModel viewModel)
        //    {
        //        //TODO : A mettre dans le ViewModel
        //        Person p = new Person();
        //        viewModel.People.Add(p);
        //        viewModel.SelectedPerson = p;
        //    }
        //}

        #endregion
    }
}
