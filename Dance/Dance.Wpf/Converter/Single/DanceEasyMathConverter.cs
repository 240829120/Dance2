using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 简单数学转化器
    /// </summary>
    public class DanceEasyMathConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value?.ToString(), out double v1);
            double.TryParse(parameter?.ToString(), out double v2);

            return v1 + v2;

        }
    }
}
