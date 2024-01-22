using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 空对象 =====> Visibility 转化器 
    /// </summary>
    public class DanceNullToVisibilityConverter : DanceConverterBase
    {
        /// <summary>
        /// 为空时的转化器
        /// </summary>
        public Visibility NullResult { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// 不为空时的转化器
        /// </summary>
        public Visibility NotNullResult { get; set; } = Visibility.Visible;

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? this.NullResult : this.NotNullResult;
        }
    }
}
