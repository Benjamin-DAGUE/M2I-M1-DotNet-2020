using CoursWPF.BankManager.Models;
using CoursWPF.BankManager.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoursWPF.BankManager.Views
{
    /// <summary>
    /// Logique d'interaction pour AccountingView.xaml
    /// </summary>
    public partial class AccountingView : UserControl
    {
        public AccountingView()
        {
            this.InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0 && sender is DatePicker dp && dp.DataContext is BankAccountLine bankAccountLine && DataGridAccountLines.DataContext is IViewModelBankAccountLines viewModel)
            {
                viewModel.CurrentDate = new DateTime(bankAccountLine.Date.Year, bankAccountLine.Date.Month, 1);

                DataGridAccountLines.Items.SortDescriptions.Clear();
                DataGridAccountLines.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Date", System.ComponentModel.ListSortDirection.Descending));
                DataGridAccountLines.Items.IsLiveSorting = true;
                DataGridAccountLines.Items.Refresh();
            }
        }
    }
}
