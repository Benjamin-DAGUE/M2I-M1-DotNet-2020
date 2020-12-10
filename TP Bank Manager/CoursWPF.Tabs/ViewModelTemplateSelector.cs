using CoursWPF.Tabs.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CoursWPF.Tabs
{
    class ViewModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template = base.SelectTemplate(item, container);

            if (item is ViewModelHello)
            {
                template = Application.Current.Resources["ViewModelHelloTemplate"] as DataTemplate;
            }

            return template;
        }
    }
}
