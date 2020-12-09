using CoursWPF.FirstApp.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoursWPF.FirstApp.TemplateSelectors
{
    public class TabItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template = base.SelectTemplate(item, container);

            if (item is IViewModelPeople)
            {
                template = App.Current.FindResource("ViewModelPeopleTemplate") as DataTemplate;
            }
            else if (item is IViewModelVehicules)
            {
                template = App.Current.FindResource("ViewModelVehiculesTemplate") as DataTemplate;
            }

            return template;
        }
    }
}
