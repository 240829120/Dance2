using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Plugin.Plugin
{
    /// <summary>
    /// 插件管理窗口模型
    /// </summary>
    public class PluginManageWindowModel : DanceViewModel
    {
        public PluginManageWindowModel()
        {
            this.LoadedCommand = new(COMMAND_GROUP, "加载", this.Loaded);
            this.EnterCommand = new(COMMAND_GROUP, "确定", this.Enter);
        }

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "插件管理";

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region PluginInfos -- 插件信息集合

        private DanceObservableCollection<IDancePluginInfo> pluginInfos = new();

        /// <summary>
        /// 插件信息集合
        /// </summary>
        public DanceObservableCollection<IDancePluginInfo> PluginInfos
        {
            get { return pluginInfos; }
            set { this.SetProperty(ref pluginInfos, value); }
        }

        #endregion

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public DanceCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private async Task Loaded()
        {
            this.PluginInfos.AddRange(DanceDomain.Current.PluginBuilder.PluginDomains.Select(p => p.PluginInfo));

            await Task.CompletedTask;
        }

        #endregion

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
            if (this.View is not Window window)
                return;

            window.DialogResult = true;
            window.Close();

            await Task.CompletedTask;
        }

        #endregion
    }
}
