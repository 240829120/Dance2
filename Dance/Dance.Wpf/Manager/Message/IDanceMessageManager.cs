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
    /// 消息框管理器
    /// </summary>
    public interface IDanceMessageManager
    {
        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="button">按钮</param>
        /// <param name="image">图标</param>
        /// <param name="owner">所属</param>
        /// <returns>点击的按钮</returns>
        MessageBoxResult Show(string title, string text, MessageBoxButton button, ImageSource? image, Window? owner);

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="button">按钮</param>
        /// <param name="image">图标</param>
        /// <param name="owner">所属</param>
        /// <returns>点击的按钮</returns>
        MessageBoxResult Show(string title, string text, MessageBoxButton button, MessageBoxImage image, Window? owner);

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="image">图标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        void Notify(string title, string text, ImageSource? image, double width, double height);
    }
}
