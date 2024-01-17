using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dance
{
    /// <summary>
    /// 命令
    /// </summary>
    public sealed class DanceCommand : DanceModel, IRelayCommand, ICommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">执行</param>
        /// <param name="canExecute">是否可以执行</param>
        /// <param name="allowMultiExecute">是否运行多次执行</param>
        public DanceCommand(string group, string name, Func<Task> execute, Func<bool>? canExecute, bool allowMultiExecute = true)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentNullException.ThrowIfNull(execute, nameof(execute));
            this.Group = group;
            this.Name = name;
            this.AllowMultiExecute = allowMultiExecute;
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">执行</param>
        /// <param name="allowMultiExecute">是否运行多次执行</param>
        public DanceCommand(string group, string name, Func<Task> execute, bool allowMultiExecute = true) : this(group, name, execute, null, allowMultiExecute)
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">行为</param>
        public DanceCommand(string group, string name, Func<Task> execute) : this(group, name, execute, null, true)
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        private readonly Func<Task> execute;

        /// <summary>
        /// 是否可以执行
        /// </summary>
        private readonly Func<bool>? canExecute;

        /// <summary>
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否允许多实例执行
        /// </summary>
        public bool AllowMultiExecute { get; } = true;

        #region IsEnabled -- 是否可用

        private bool isEnabeld = true;
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabeld; }
            set
            {
                this.NotifyPropertyChanging(nameof(IsEnabled));
                isEnabeld = value;
                this.NotifyPropertyChanged(nameof(IsEnabled));
            }
        }

        #endregion

        /// <summary>
        /// 通知是否可以执行改变
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以执行</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(object? parameter)
        {
            if (this.AllowMultiExecute)
                return this.canExecute?.Invoke() ?? true;
            else
                return this.IsEnabled && (this.canExecute?.Invoke() ?? true);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter">参数</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async void Execute(object? parameter)
        {
            this.Record(parameter);

            try
            {
                if (this.AllowMultiExecute)
                {
                    await this.execute();
                    return;
                }

                this.IsEnabled = false;
                this.NotifyCanExecuteChanged();
                await this.execute();
                this.IsEnabled = true;
                this.NotifyCanExecuteChanged();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="parameter">参数</param>
        private void Record(object? parameter)
        {
            try
            {
                if (RecordInvoke == null)
                    return;

                if (parameter == null || !parameter.GetType().IsPrimitive)
                {
                    RecordInvoke.Invoke($"[{this.Group} -- {this.Name}]");
                    return;
                }

                RecordInvoke.Invoke($"[{this.Group} -- {this.Name}] ===> {JsonConvert.SerializeObject(parameter)}");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }

    /// <summary>
    /// 命令基类
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    public sealed class DanceCommand<T> : DanceModel, IRelayCommand<T>, IRelayCommand, ICommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">执行</param>
        /// <param name="canExecute">是否可以执行</param>
        /// <param name="allowMultiExecute">是否运行多次执行</param>
        public DanceCommand(string group, string name, Func<T?, Task> execute, Func<T?, bool>? canExecute, bool allowMultiExecute = true)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentNullException.ThrowIfNull(execute, nameof(execute));
            this.Group = group;
            this.Name = name;
            this.AllowMultiExecute = allowMultiExecute;
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">执行</param>
        /// <param name="allowMultiExecute">是否运行多次执行</param>
        public DanceCommand(string group, string name, Func<T?, Task> execute, bool allowMultiExecute = true) : this(group, name, execute, null, allowMultiExecute)
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="name">名称</param>
        /// <param name="execute">行为</param>
        public DanceCommand(string group, string name, Func<T?, Task> execute) : this(group, name, execute, null, true)
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        private readonly Func<T?, Task> execute;

        /// <summary>
        /// 是否可以执行
        /// </summary>
        private readonly Func<T?, bool>? canExecute;

        /// <summary>
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否允许多实例执行
        /// </summary>
        public bool AllowMultiExecute { get; } = true;

        #region IsEnabled -- 是否可用

        private bool isEnabeld = true;
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabeld; }
            private set
            {
                this.NotifyPropertyChanging(nameof(IsEnabled));
                isEnabeld = value;
                this.NotifyPropertyChanged(nameof(IsEnabled));
            }
        }

        #endregion

        /// <summary>
        /// 通知是否可以执行改变
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以执行</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(object? parameter)
        {
            if (!TryGetCommandArgument(parameter, out T? result))
                ThrowArgumentExceptionForInvalidCommandArgument(parameter);

            return this.CanExecute(result);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter">参数</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(object? parameter)
        {
            this.Record(parameter);

            if (!TryGetCommandArgument(parameter, out T? result))
                ThrowArgumentExceptionForInvalidCommandArgument(parameter);

            this.Execute(result);
        }

        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以执行</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(T? parameter)
        {
            if (this.AllowMultiExecute)
                return this.canExecute?.Invoke(parameter) ?? true;
            else
                return this.IsEnabled && (this.canExecute?.Invoke(parameter) ?? true);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter">参数</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async void Execute(T? parameter)
        {
            this.Record(parameter);

            try
            {
                if (this.AllowMultiExecute)
                {
                    await this.execute(parameter);
                    return;
                }

                this.IsEnabled = false;
                this.NotifyCanExecuteChanged();
                await this.execute(parameter);
                this.IsEnabled = true;
                this.NotifyCanExecuteChanged();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="parameter">参数</param>
        private void Record(object? parameter)
        {
            try
            {
                if (RecordInvoke == null)
                    return;

                if (parameter == null || !parameter.GetType().IsPrimitive)
                {
                    RecordInvoke.Invoke($"[Command: {this.Name}]");
                    return;
                }

                RecordInvoke.Invoke($"[Command: {this.Name}] parameter: {JsonConvert.SerializeObject(parameter)}");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 尝试获取命令参数
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="result">转化后的参数</param>
        /// <returns>是否成功获取</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool TryGetCommandArgument(object? parameter, out T? result)
        {
            if (parameter == null && default(T) == null)
            {
                result = default;
                return true;
            }

            if (parameter is T val)
            {
                result = val;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// 抛出命令参数类型错误异常
        /// </summary>
        /// <param name="parameter">参数</param>
        internal static void ThrowArgumentExceptionForInvalidCommandArgument(object? parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentException($"Parameter \"{"parameter"}\" (object) must not be null, as the command type requires an argument of type {typeof(T)}.", nameof(parameter));
            }

            throw new ArgumentException($"Parameter \"{"parameter"}\" (object) cannot be of type {parameter.GetType()}, as the command type requires an argument of type {typeof(T)}.", nameof(parameter));
        }
    }
}
