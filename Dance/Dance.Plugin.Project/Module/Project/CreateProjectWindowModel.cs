using Dance.Wpf;
using DevExpress.Mvvm;
using DevExpress.Xpf.Dialogs;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            this.LoadedCommand = new(COMMAND_GROUP, "加载", this.Loaded);
            this.ChooseProjectPathCommand = new(COMMAND_GROUP, "选择项目目录", this.ChooseProjectPath);
            this.EnterCommand = new(COMMAND_GROUP, "确定", this.Enter) { IsEnabled = false };
            this.CancelCommand = new(COMMAND_GROUP, "取消", this.Cancel);

            this.Validate();
        }

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "创建项目";

        /// <summary>
        /// 延时键 -- 校验是否可以创建项目
        /// </summary>
        public const string DELAY_KEY__CHECK_CAN_CREATE_PROJECT = "Dance.Plugin.Project.CreateProjectWindowModel.CheckCanCreateProject";

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region ProjectPath -- 项目路径

        private string? projectPath;

        /// <summary>
        /// 项目路径
        /// </summary>
        [Required(ErrorMessage = "项目路径不能为空")]
        [DanceDirectoryExistsValidation(ErrorMessage = "项目路径不存在")]
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
        [Required(ErrorMessage = "项目名称不能为空"),]
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

        #region ProjectCategorys -- 项目类型集合

        private DanceObservableCollection<ProjectCategoryModel> projectCategorys = [];
        /// <summary>
        /// 项目类型集合
        /// </summary>
        public DanceObservableCollection<ProjectCategoryModel> ProjectCategorys
        {
            get { return projectCategorys; }
            set { this.SetProperty(ref projectCategorys, value); }
        }

        #endregion

        #region SelectedProjectCategory -- 选中的项目类型

        private ProjectCategoryModel? selectedProjectCategory;
        /// <summary>
        /// 选中的项目类型
        /// </summary>
        [ProjectCategoryNotNull(ErrorMessage = "项目类型不能为空")]
        public ProjectCategoryModel? SelectedProjectCategory
        {
            get { return selectedProjectCategory; }
            set { this.SetProperty(ref selectedProjectCategory, value); this.ValidateProperty(); }
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
            var groups = DanceDomain.Current.PluginBuilder.PluginDomains.Where(p => p.PluginInfo is ProjectPluginInfo)
                                                                        .Select(p => (ProjectPluginInfo)p.PluginInfo)
                                                                        .GroupBy(p => p.Group);

            foreach (var group in groups)
            {
                ProjectCategoryModel groupModel = new()
                {
                    Name = group.Key,
                    Detail = group.Key
                };

                foreach (var item in group)
                {
                    ProjectCategoryModel itemModel = new()
                    {
                        Name = item.Name,
                        Icon = item.Icon,
                        Detail = item.Detail,
                        PluginInfo = item
                    };

                    groupModel.Items.Add(itemModel);
                }

                this.ProjectCategorys.Add(groupModel);
            }

            await Task.CompletedTask;
        }

        #endregion

        #region ChooseProjectPathCommand -- 选择项目路径命令

        /// <summary>
        /// 选择项目路径命令
        /// </summary>
        public DanceCommand ChooseProjectPathCommand { get; private set; }

        /// <summary>
        /// 选择项目路径
        /// </summary>
        private async Task ChooseProjectPath()
        {
            FolderBrowserDialog dialog = new();
            if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                return;

            this.ProjectPath = dialog.SelectedPath;

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
