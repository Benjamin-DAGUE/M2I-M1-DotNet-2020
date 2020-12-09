using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;

namespace CoursWPF.FirstApp
{
    class ProvideService : MarkupExtension
    {
        #region Properties

        public Type ServiceType { get; set; }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return App.ServiceProvider.GetService(this.ServiceType);
        }
    }
}
