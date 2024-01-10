using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Dance.Wpf
{
    /// <summary>
    /// 元素事件触发器
    /// </summary>
    public static partial class DanceFrameworkElementEventTrigger
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = LogManager.GetLogger(typeof(DanceFrameworkElementEventTrigger));

        #region FrameworkElementEventTriggerCache -- 元素事件触发器缓存

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.FrameworkElementEventTriggerCacheProperty"></inheritdoc>/>
        public static Dictionary<string, object?> GetFrameworkElementEventTriggerCache(DependencyObject obj)
        {
            return (Dictionary<string, object?>)obj.GetValue(FrameworkElementEventTriggerCacheProperty);
        }

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.FrameworkElementEventTriggerCacheProperty"></inheritdoc>/>
        public static void SetFrameworkElementEventTriggerCache(DependencyObject obj, Dictionary<string, object?> value)
        {
            obj.SetValue(FrameworkElementEventTriggerCacheProperty, value);
        }

        /// <summary>
        /// 元素事件触发器缓存
        /// </summary>
        public static readonly DependencyProperty FrameworkElementEventTriggerCacheProperty =
            DependencyProperty.RegisterAttached("FrameworkElementEventTriggerCache", typeof(Dictionary<string, object?>), typeof(DanceFrameworkElementEventTrigger), new PropertyMetadata(null));


        /// <summary>
        /// 获取元素事件触发器缓存值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="element">元素</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        private static T? GetFrameworkElementEventTriggerCacheValue<T>(FrameworkElement element, string key)
        {
            Dictionary<string, object?>? cache;
            lock (element)
            {
                cache = GetFrameworkElementEventTriggerCache(element);

                if (cache == null)
                {
                    cache = [];
                    SetFrameworkElementEventTriggerCache(element, cache);
                }
            }

            cache.TryGetValue(key, out object? value);

            return value == null ? default : (T)value;
        }

        /// <summary>
        /// 设置元素事件触发器缓存值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="element">元素</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        private static void SetFrameworkElementEventTriggerCacheValue<T>(FrameworkElement element, string key, T value)
        {
            Dictionary<string, object?>? cache;
            lock (element)
            {
                cache = GetFrameworkElementEventTriggerCache(element);

                if (cache == null)
                {
                    cache = [];
                    SetFrameworkElementEventTriggerCache(element, cache);
                }
            }

            cache[key] = value;
        }

        #endregion

        #region Loaded -- 加载

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.LoadedCommandProperty"></inheritdoc>
        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedCommandProperty);
        }

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.LoadedCommandProperty"></inheritdoc>
        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        /// <summary>
        /// 加载命令
        /// </summary>
        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(DanceFrameworkElementEventTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Loaded -= Execute_LoadedCommand;
                element.Loaded += Execute_LoadedCommand;

            })));

        /// <summary>
        /// 执行加载命令
        /// </summary>
        private static void Execute_LoadedCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetLoadedCommand(element) is not ICommand command)
                    return;

                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region UnloadedCommand -- 卸载命令

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.UnloadedCommandProperty"></inheritdoc>
        public static ICommand GetUnloadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UnloadedCommandProperty);
        }

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.UnloadedCommandProperty"></inheritdoc>
        public static void SetUnloadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(UnloadedCommandProperty, value);
        }

        /// <summary>
        /// 卸载命令
        /// </summary>
        public static readonly DependencyProperty UnloadedCommandProperty =
            DependencyProperty.RegisterAttached("UnloadedCommand", typeof(ICommand), typeof(DanceFrameworkElementEventTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Unloaded -= Execute_UnloadedCommand;
                element.Unloaded += Execute_UnloadedCommand;
            })));

        /// <summary>
        /// 执行卸载命令
        /// </summary>
        private static void Execute_UnloadedCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetUnloadedCommand(element) is not ICommand command)
                    return;

                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region LoadedOnceCommand -- 加载一次命令

        /// <summary>
        /// 是否已经执行过加载
        /// </summary>
        public const string LOADED_ONCE_COMMAND__IS_ALREADY_LOADED = "LOADED_ONCE_COMMAND__IS_ALREADY_LOADED";

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.LoadedOnceCommand"></inheritdoc>
        public static ICommand GetLoadedOnceCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedOnceCommandProperty);
        }

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.LoadedOnceCommand"></inheritdoc>
        public static void SetLoadedOnceCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedOnceCommandProperty, value);
        }

        /// <summary>
        /// 加载一次命令
        /// </summary>
        public static readonly DependencyProperty LoadedOnceCommandProperty =
            DependencyProperty.RegisterAttached("LoadedOnceCommand", typeof(ICommand), typeof(DanceFrameworkElementEventTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Loaded -= Execute_LoadedOnceCommand;
                element.Loaded += Execute_LoadedOnceCommand;
            })));

        /// <summary>
        /// 执行卸载命令
        /// </summary>
        private static void Execute_LoadedOnceCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetLoadedOnceCommand(element) is not ICommand command)
                    return;

                if (GetFrameworkElementEventTriggerCacheValue<bool>(element, LOADED_ONCE_COMMAND__IS_ALREADY_LOADED))
                    return;

                SetFrameworkElementEventTriggerCacheValue(element, LOADED_ONCE_COMMAND__IS_ALREADY_LOADED, true);
                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region UnloadedOnceCommand -- 卸载一次命令

        /// <summary>
        /// 是否已经执行过卸载
        /// </summary>
        public const string LOADED_ONCE_COMMAND__IS_ALREADY_UNLOADED = "LOADED_ONCE_COMMAND__IS_ALREADY_UNLOADED";

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.UnloadedOnceCommandProperty"></inheritdoc>
        public static ICommand GetUnloadedOnceCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UnloadedOnceCommandProperty);
        }

        /// <inheritdoc cref="DanceFrameworkElementEventTrigger.UnloadedOnceCommandProperty"></inheritdoc>
        public static void SetUnloadedOnceCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(UnloadedOnceCommandProperty, value);
        }

        /// <summary>
        /// 卸载一次命令
        /// </summary>
        public static readonly DependencyProperty UnloadedOnceCommandProperty =
            DependencyProperty.RegisterAttached("UnloadedOnceCommand", typeof(ICommand), typeof(DanceFrameworkElementEventTrigger), new PropertyMetadata(null, new PropertyChangedCallback((s, e) =>
            {
                if (s is not FrameworkElement element)
                    return;

                element.Unloaded -= Execute_UnloadedOnceCommand;
                element.Unloaded += Execute_UnloadedOnceCommand;
            })));

        /// <summary>
        /// 执行卸载一次命令
        /// </summary>
        private static void Execute_UnloadedOnceCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not FrameworkElement element || GetUnloadedOnceCommand(element) is not ICommand command)
                    return;

                if (GetFrameworkElementEventTriggerCacheValue<bool>(element, LOADED_ONCE_COMMAND__IS_ALREADY_UNLOADED))
                    return;

                SetFrameworkElementEventTriggerCacheValue(element, LOADED_ONCE_COMMAND__IS_ALREADY_UNLOADED, true);
                command.Execute(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion
    }
}
