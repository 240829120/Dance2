using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dance.Wpf
{
    /// <summary>
    /// 转化器基类
    /// </summary>
    public abstract class DanceConverterBase : IValueConverter
    {
        /// <summary>
        /// 标记
        /// </summary>
        public object? Tag { get; set; }

        /// <summary>
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        /// </summary>
        public abstract object? Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// <inheritdoc cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
        /// </summary>
        public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
