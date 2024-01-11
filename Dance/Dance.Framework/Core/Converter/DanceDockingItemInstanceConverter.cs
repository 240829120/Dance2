using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dance.Framework
{
    /// <summary>
    /// Docking项实例转化器
    /// </summary>
    public class DanceDockingItemInstanceConverter : DanceConverterBase
    {
        /// <summary>
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not DanceDockingItemViewModelBase vm || vm.ViewType == null || string.IsNullOrWhiteSpace(vm.ViewType.FullName))
                return null;

            if (vm.View != null)
                return vm.View;

            vm.View = vm.ViewType.Assembly.CreateInstance(vm.ViewType.FullName);

            return vm.View;
        }
    }
}
