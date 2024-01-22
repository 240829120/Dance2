using Dance.Wpf;
using DevExpress.Mvvm;
using DevExpress.Xpf.Dialogs;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        }

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "创建项目";

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

        #region Workpath -- 项目路径

        private string? workPath;

        /// <summary>
        /// 项目路径
        /// </summary>
        [Required(ErrorMessage = "路径不能为空")]
        [DanceDirectoryExistsValidation(ErrorMessage = "项目路径不存在")]
        public string? WorkPath
        {
            get { return workPath; }
            set { this.SetProperty(ref workPath, value); }
        }

        #endregion

        #region ProjectName -- 项目名称

        private string? projectName;

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空"),]
        [DanceFileNameValidation(ErrorMessage = "名称不合法")]
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
        [Required(ErrorMessage = "类型不能为空")]
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

            this.SelectedProjectCategory = this.ProjectCategorys.FirstOrDefault()?.Items.FirstOrDefault();

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
            FolderBrowserDialog dialog = new()
            {
                SelectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Projects")
            };
            if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath))
                return;

            this.WorkPath = dialog.SelectedPath;

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

            if (string.IsNullOrWhiteSpace(this.WorkPath) || string.IsNullOrWhiteSpace(this.ProjectName))
                return;

            string path = Path.Combine(this.WorkPath, $"{this.ProjectName}{ProjectOptions.ProjectExtension}");
            if (File.Exists(path))
            {
                if (this.MessageManager.Show("创建项目", "项目文件已经存在，是否覆盖?", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    return;
            }

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
