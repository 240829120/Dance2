using CommunityToolkit.Mvvm.ComponentModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 模型
    /// </summary>
    public class DanceModel : DanceModelBase, IDisposable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// <inheritdoc cref="IDanceHistoryManager"/>
        /// </summary>
        public IDanceHistoryManager? HistoryManager { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>是否成功执行</returns>
        public virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            if (this.HistoryManager != null && !this.HistoryManager.IsExecuting && !string.IsNullOrWhiteSpace(propertyName))
            {
                this.HistoryManager.Append(new DancePropertyChangedHistoryStep(this, propertyName, field, value));
            }

            this.NotifyPropertyChanging(propertyName);
            field = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}