﻿using CoursWPF.FirstApp.Models;
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
using CoursWPF.FirstApp.Views.Abstracts;

namespace CoursWPF.FirstApp
{
    /// <summary>
    /// Interaction logic for DynamicMainWindow.xaml
    /// </summary>
    public partial class DynamicMainWindow : MetroWindow, IDynamicMainWindow
    {
        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="StaticMainWindow"/>
        /// </summary>
        public DynamicMainWindow()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}
