using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// Bool类型转化器
    /// </summary>
    public class DanceBoolConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool b)
                return false;

            return !b;
        }

        /// <summary>
        /// 反转化
        /// </summary>
        public override object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool b)
                return false;

            return !b;
        }
    }
}
