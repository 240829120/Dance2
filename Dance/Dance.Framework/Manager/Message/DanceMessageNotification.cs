using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Framework
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public class DanceMessageNotification : Control
    {
        static DanceMessageNotification()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DanceMessageNotification), new FrameworkPropertyMetadata(typeof(DanceMessageNotification)));
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static DanceMessageNotification? Instance { get; private set; }

        /// <summary>
        /// 消息通知
        /// </summary>
        public DanceMessageNotification()
        {
            Instance = this;
        }

        /// <summary>
        /// 通知服务
        /// </summary>
        private NotificationService? PART_NotificationService;

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_NotificationService = this.Template.FindName(nameof(PART_NotificationService), this) as NotificationService;
        }

        /// <summary>
        /// 通知服务
        /// </summary>
        public NotificationService? NotificationService { get { return this.PART_NotificationService; } }
    }
}
