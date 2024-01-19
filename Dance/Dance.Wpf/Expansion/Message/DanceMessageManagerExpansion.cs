using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// 消息管理器扩展
    /// </summary>
    public static class DanceMessageManagerExpansion
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="manager">消息管理器</param>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="button">按钮</param>
        /// <param name="owner">所属</param>
        /// <returns>点击的按钮</returns>
        public static MessageBoxResult Show(this IDanceMessageManager manager, string title, string text, MessageBoxButton button, Window? owner = null)
        {
            return manager.Show(title, text, button, null, owner);
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="manager">消息管理器</param>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>点击的按钮</returns>
        public static MessageBoxResult Show(this IDanceMessageManager manager, string title, string text, Window? owner = null)
        {
            return manager.Show(title, text, MessageBoxButton.OK, MessageBoxImage.Information, owner);
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="manager">消息管理器</param>
        /// <param name="text">文本</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>点击的按钮</returns>
        public static MessageBoxResult Show(this IDanceMessageManager manager, string text, Window? owner = null)
        {
            return manager.Show("消息", text, MessageBoxButton.OK, MessageBoxImage.Information, owner);
        }

        /// <summary>
        /// 显示错误
        /// </summary>
        /// <param name="manager">消息管理器</param>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>点击的按钮</returns>
        public static MessageBoxResult ShowError(this IDanceMessageManager manager, string title, string text, Window? owner = null)
        {
            return manager.Show(title, text, MessageBoxButton.OK, MessageBoxImage.Error, owner);
        }

        /// <summary>
        /// 显示错误
        /// </summary>
        /// <param name="manager">消息管理器</param>
        /// <param name="text">文本</param>
        /// <param name="owner">所属窗口</param>
        /// <returns>点击的按钮</returns>
        public static MessageBoxResult ShowError(this IDanceMessageManager manager, string text, Window? owner = null)
        {
            return manager.Show("错误", text, MessageBoxButton.OK, MessageBoxImage.Error, owner);
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        public static void Notify(this IDanceMessageManager manager, string title, string text)
        {
            manager.Notify(title, text, null, 300, 100);
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="text">文本</param>
        public static void Notify(this IDanceMessageManager manager, string text)
        {
            manager.Notify("消息", text, null, 300, 100);
        }
    }
}
