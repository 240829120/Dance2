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
    public class DanceModel : DanceObject, IDisposable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// <inheritdoc cref="IDanceHistoryManager"/>
        /// </summary>
        public IDanceHistoryManager? HistoryManager { get; set; }

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 通知属性改变之前
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public virtual void NotifyPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            if (this.PropertyChanging == null)
                return;

            if (DispatcherCheckAccess == null || DispatcherCheckAccess())
            {
                this.PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
            }
            else
            {
                DispatcherInvoke?.Invoke(() =>
                {
                    this.PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
                });
            }
        }

        /// <summary>
        /// 通知属性改变之后
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (DispatcherCheckAccess == null || DispatcherCheckAccess())
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                DispatcherInvoke?.Invoke(() =>
                {
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }

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