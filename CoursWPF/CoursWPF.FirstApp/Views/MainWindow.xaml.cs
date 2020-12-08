using CoursWPF.FirstApp.Models;
using CoursWPF.FirstApp.ViewModels;
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

namespace CoursWPF.FirstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            //On initialise le DataContext à la création de la MainWindow.
            this.DataContext = new MainViewModel();
        }

        #endregion

        #region Methods

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel viewModel)
            {
                //TODO : Sauvegarder en base avant d'effacer.
                Person p = new Person();
                viewModel.People.Add(p);
                viewModel.SelectedPerson = p;
            }
        }

        #endregion
    }
}
