using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Plugin.LayoutManage
{
    /// <summary>
    /// 布局输入名称窗口模型
    /// </summary>
    public class LayoutInputNameWindowModel : DanceViewModel
    {
        public LayoutInputNameWindowModel()
        {
            this.EnterCommand = new("布局管理", "确定输入布局名称", this.Enter);
            this.CancelCommand = new("布局管理", "取消输入布局名称", this.Cancel);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly IDanceMessageManager MessageManager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region LayoutName -- 布局名称

        private string? layoutName;

        /// <summary>
        /// 布局名称
        /// </summary>
        public string? LayoutName
        {
            get { return layoutName; }
            set { this.SetProperty(ref layoutName, value); }
        }

        #endregion

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public DanceCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private async Task Enter()
        {
            if (string.IsNullOrWhiteSpace(this.LayoutName))
            {
                this.MessageManager.Show("请输入布局名称");
                return;
            }

            if (this.View is not Window window)
                return;

            window.DialogResult = true;
            window.Close();

            await Task.CompletedTask;
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public DanceCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private async Task Cancel()
        {
            if (this.View is not Window window)
                return;

            window.DialogResult = false;
            window.Close();

            await Task.CompletedTask;
        }

        #endregion


    }
}
