using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace CoursWPF.AddressBook.Client.Converters
{
    /// <summary>
    ///     Conversion du genre en couleur
    /// </summary>
    public class GenderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = Brushes.LightGray;

            bool? valueBool = value as bool?;

            if (valueBool.HasValue)
            {
                brush = valueBool.Value ? Brushes.CornflowerBlue : Brushes.LightPink;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
