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
    /// 模型基类
    /// </summary>
    public abstract class DanceModelBase : DanceObject, IDisposable, INotifyPropertyChanging, INotifyPropertyChanged
    {
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
    }
}
