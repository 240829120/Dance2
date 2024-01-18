using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 创建项目窗口模型
    /// </summary>
    public class CreateProjectWindowModel : DanceViewModel
    {
        public CreateProjectWindowModel()
        {
            this.EnterCommand = new(COMMAND_GROUP, "确定", this.Enter);
            this.CancelCommand = new(COMMAND_GROUP, "取消", this.Cancel);
        }

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "创建项目";

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region ProjectPath -- 项目路径

        private string? projectPath;

        /// <summary>
        /// 项目路径
        /// </summary>
        public string? ProjectPath
        {
            get { return projectPath; }
            set { this.SetProperty(ref projectPath, value); }
        }

        #endregion

        #region ProjectName -- 项目名称

        private string? projectName;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? ProjectName
        {
            get { return projectName; }
            set { this.SetProperty(ref projectName, value); }
        }

        #endregion

        #region ProjectDetail -- 项目描述

        private string? projectDetail;

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDetail
        {
            get { return projectDetail; }
            set { this.SetProperty(ref projectDetail, value); }
        }

        #endregion

        #region ProjectPluginGroups -- 项目插件分组集合

        private DanceObservableCollection<ProjectPluginGroupModel>? projectPluginGroups;
        /// <summary>
        /// 项目插件分组集合
        /// </summary>
        public DanceObservableCollection<ProjectPluginGroupModel>? ProjectPluginGroups
        {
            get { return projectPluginGroups; }
            set { this.SetProperty(ref projectPluginGroups, value); }
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
