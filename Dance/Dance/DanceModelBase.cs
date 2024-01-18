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
    public abstract class DanceModelBase : DanceObject, IDisposable, INotifyPropertyChanging, INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// 错误集合
        /// </summary>
        private readonly Dictionary<string, string> ErrorDic = [];

        /// <summary>
        /// 属性集合
        /// </summary>
        private readonly Dictionary<string, PropertyInfo?> PropertyDic = [];

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 对象错误信息
        /// </summary>
        public string Error => this.ErrorDic.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.Value)).Value;

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="columnName">属性名</param>
        public string this[string columnName] => this.ValidateProperty(columnName);

        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>错误信息</returns>
        public virtual string ValidateProperty([CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return string.Empty;

            if (!this.PropertyDic.TryGetValue(propertyName, out PropertyInfo? property))
            {
                property = this.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                this.PropertyDic[propertyName] = property;
            }

            if (property == null)
                return string.Empty;

            if (!property.CanRead || !property.CanWrite)
                return string.Empty;

            ValidationContext context = new(this, null, null)
            {
                MemberName = propertyName
            };

            List<ValidationResult> results = [];
            Validator.TryValidateProperty(property?.GetValue(this, null), context, results);

            if (results.Count == 0)
            {
                this.ErrorDic[propertyName] = string.Empty;
                this.NotifyPropertyChanged(nameof(Error));
                return string.Empty;
            }

            string error = string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage).ToArray());
            this.ErrorDic[propertyName] = error;
            this.NotifyPropertyChanged(nameof(Error));

            return error;
        }

        /// <summary>
        /// 验证对象
        /// </summary>
        public virtual void Validate()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && p.CanRead);
            foreach (var property in properties)
            {
                this.ValidateProperty(property.Name);
            }
        }

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
