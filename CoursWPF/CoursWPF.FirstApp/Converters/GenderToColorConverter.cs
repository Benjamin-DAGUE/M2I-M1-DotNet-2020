using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace CoursWPF.FirstApp.Converters
{
    /// <summary>
    ///     Lorsque dans un Binding il est nécessaire de convertir une données d'un type en un autre type, il faut utiliser un Converter.
    ///     Pour créer un Converter, il faut créer un classe qui implémente l'interface IValueConverter.
    /// </summary>
    public class GenderToColorConverter : IValueConverter
    {
        /// <summary>
        ///     Permet de convertir un booléen nullable en une couleur (brush).
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = Brushes.Gray;

            if (value is bool? && ((bool?)value).HasValue)
            {
                if ((bool?)value == true)
                {
                    brush = Brushes.CornflowerBlue;
                }
                else
                {
                    brush = Brushes.LightPink;
                }
            }

            return brush;
        }

        /// <summary>
        ///     Permet de convertir une couleur en booléen (non utilisé).
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
