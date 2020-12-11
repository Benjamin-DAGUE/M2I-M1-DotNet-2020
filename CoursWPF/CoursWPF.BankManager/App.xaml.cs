using CoursWPF.BankManager.Models;
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
        #region Fields

        private static DataStore _DataStore;

        #endregion

        #region Properties

        public static DataStore DataStore => _DataStore;

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            _DataStore = DataStore.Load();

        }
    }
}
