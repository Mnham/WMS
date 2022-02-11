using System;

using Windows.UI.Xaml.Data;

namespace WMS.Manager.UWP.Infrastructure.Converters
{
    /// <summary>
    /// Представляет инвертированный конвертер для <see cref="bool"/> значений.
    /// </summary>
    public class InvertBoolConverter : IValueConverter
    {
        // TODO: коммент
        public object Convert(object value, Type targetType, object parameter, string language) =>
            !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, string language) =>
            throw new NotImplementedException();
    }
}