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
    /// 算法
    /// </summary>
    public enum DanceSimpleMathConverterType
    {
        /// <summary>
        /// 加法
        /// </summary>
        Add,

        /// <summary>
        /// 减法
        /// </summary>
        Subtract,

        /// <summary>
        /// 乘法
        /// </summary>
        Multiply,
    }

    /// <summary>
    /// 简单数学算法转化器
    /// </summary>
    public class DanceSimpleMathConverter : DanceConverterBase
    {
        /// <summary>
        /// 转化方法
        /// </summary>
        public DanceSimpleMathConverterType ConverterType { get; set; } = DanceSimpleMathConverterType.Add;

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _ = double.TryParse(value?.ToString(), out double v1);
            _ = double.TryParse(parameter?.ToString(), out double v2);

            return this.ConverterType switch
            {
                DanceSimpleMathConverterType.Add => v1 + v2,
                DanceSimpleMathConverterType.Subtract => v1 - v2,
                DanceSimpleMathConverterType.Multiply => v1 * v2,
                _ => (object)(v1 + v2),
            };
        }
    }
}
