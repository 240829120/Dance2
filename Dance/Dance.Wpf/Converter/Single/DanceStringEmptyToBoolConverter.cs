using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 字符串为空或空白 =====> Bool 转化器
    /// </summary>
    public class DanceStringEmptyToBoolConverter : DanceConverterBase
    {
        /// <summary>
        /// 为真时的返回值
        /// </summary>
        public bool TrueResult { get; set; } = true;

        /// <summary>
        /// 为假时的返回值
        /// </summary>
        public bool FalseResult { get; set; } = false;

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string str)
                return this.TrueResult;

            return string.IsNullOrWhiteSpace(str) ? this.TrueResult : this.FalseResult;
        }
    }
}
