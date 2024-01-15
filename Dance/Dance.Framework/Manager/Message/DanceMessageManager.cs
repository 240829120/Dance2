using Dance.Wpf;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Framework
{
    /// <summary>
    /// 消息管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceMessageManager))]
    public class DanceMessageManager : IDanceMessageManager
    {
        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IDanceWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="button">按钮</param>
        /// <param name="image">图标</param>
        /// <param name="owner">所属</param>
        /// <returns>点击返回</returns>
        public MessageBoxResult Show(string title, string text, MessageBoxButton button, ImageSource? image, Window? owner)
        {
            MessageBoxResult result = MessageBoxResult.None;

            DanceXamlHelper.Invoke(() =>
            {
                ThemedMessageBoxParameters parameters = new(image)
                {
                    ShowActivated = true,
                    Owner = owner ?? WindowManager?.MainWindow ?? Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                result = ThemedMessageBox.Show(title, text, button, MessageBoxResult.None, parameters);
            });

            return result;
        }

        /// <summary>
        /// 显示提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="button">按钮</param>
        /// <param name="image">图标</param>
        /// <param name="owner">所属</param>
        /// <returns>点击的按钮</returns>
        public MessageBoxResult Show(string title, string text, MessageBoxButton button, MessageBoxImage image, Window? owner)
        {
            return this.Show(title, text, button, image.GetMessageBoxIcon(), owner);
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">文本</param>
        /// <param name="image">图标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public void Notify(string title, string text, ImageSource? image, double width, double height)
        {
            DanceXamlHelper.Invoke(() =>
            {
                DanceMessageNotificationModel model = new()
                {
                    Title = title,
                    Text = text,
                    Image = image,
                    Width = width,
                    Height = height
                };

                INotification? notification = DanceMessageNotification.Instance?.NotificationService?.CreateCustomNotification(model);
                notification?.ShowAsync();
            });
        }
    }
}
