using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
        // ===================================================================================================
        // **** Static ****
        // ===================================================================================================

        /// <summary>
        /// 调度检测是否可用
        /// </summary>
        public static Func<bool>? DispatcherCheckAccess { get; set; }

        /// <summary>
        /// 调度执行
        /// </summary>
        public static Action<Action>? DispatcherInvoke { get; set; }

        /// <summary>
        /// 日志执行
        /// </summary>
        public static Action<string>? RecordInvoke { get; set; }

        // ===================================================================================================
        // **** Event ****
        // ===================================================================================================

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

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
