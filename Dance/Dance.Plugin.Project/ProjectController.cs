using CommunityToolkit.Mvvm.Messaging;
using Dance.Framework;
using Dance.Wpf;
using DevExpress.Charts.Native;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    /// ---------------------------------------------------------
    ///     项目(P)
    ///       | -- 新建项目
    ///       | -- 打开项目
    ///       | -- 最近项目 
    ///               | -- 测试项目1
    ///               | -- 测试项目2
    ///               | -- 测试项目3
    ///       | -- 关闭项目
    /// ---------------------------------------------------------
    public class ProjectController : DanceObject
    {
        public ProjectController()
        {
            DanceDomain.Current.Messenger.Register<ProjectClosedMsg>(this, this.ProjectClosed);
            DanceDomain.Current.Messenger.Register<ProjectOpendMsg>(this, this.ProjectOpend);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "项目菜单";

        /// <summary>
        /// 配置管理器
        /// </summary>
        private readonly IDanceConfigManager ConfigManager = DanceDomain.Current.LifeScope.Resolve<IDanceConfigManager>();

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IDanceWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly IDanceMessageManager MessageManager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();

        /// <summary>
        /// 项目管理器
        /// </summary>
        private readonly IProjectManager ProjectManager = DanceDomain.Current.LifeScope.Resolve<IProjectManager>();

        /// <summary>
        /// 主菜单
        /// </summary>
        private readonly DanceBarSubItemModel MainSubItem = new();

        /// <summary>
        /// 新建项目菜单
        /// </summary>
        private readonly DanceBarButtonItemModel CreateProjectItem = new();

        /// <summary>
        /// 打开项目菜单
        /// </summary>
        private readonly DanceBarButtonItemModel OpenProjectItem = new();

        /// <summary>
        /// 打开最近使用的项目菜单
        /// </summary>
        private readonly DanceBarSubItemModel RecentlyUsedProjectItem = new();

        /// <summary>
        /// 关闭项目菜单
        /// </summary>
        private readonly DanceBarButtonItemModel CloseProjectItem = new();

        /// <summary>
        /// 项目名项
        /// </summary>
        private readonly BarProjectNameItemModel ProjectNameItem = new();

        // ===================================================================================================
        // **** Message ****
        // ===================================================================================================

        #region ProjectClosedMsg -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void ProjectClosed(object sender, ProjectClosedMsg msg)
        {
            this.ProjectNameItem.Content = string.Empty;
        }

        #endregion

        #region ProjectOpendMsg -- 项目打开消息

        /// <summary>
        /// 项目打开消息
        /// </summary>
        private void ProjectOpend(object sender, ProjectOpendMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            this.ProjectNameItem.Content = project;

            var collection = this.ConfigManager.Context.GetRecentlyUsedProjects();
            RecentlyUsedProjectEntity entity = new()
            {
                ProjectPath = project.ProjectPath,
                LastOpenTime = DateTime.Now
            };

            collection.DeleteMany(p => p.ProjectPath == entity.ProjectPath);
            collection.Insert(entity);

            var deletes = collection.FindAll().OrderByDescending(p => p.LastOpenTime).Skip(ProjectOptions.RecentlyUsedProjectCount);
            foreach (var delete in deletes)
            {
                collection.Delete(delete.ID);
            }

            DanceBarButtonItemModel item = new()
            {
                Content = project.ProjectPath,
                Tag = entity,
                ClickCommand = new DanceCommand(COMMAND_GROUP, "最近项目", async () => await this.OpenProject(project.ProjectPath))
            };

            var oldItem = this.RecentlyUsedProjectItem.Items.FirstOrDefault(p => p.Tag is RecentlyUsedProjectEntity tag && tag.ProjectPath == entity.ProjectPath);
            if (oldItem != null)
            {
                this.RecentlyUsedProjectItem.Items.Remove(oldItem);
            }

            this.RecentlyUsedProjectItem.Items.Insert(0, item);
            if (this.RecentlyUsedProjectItem.Items.Count > ProjectOptions.RecentlyUsedProjectCount)
            {
                this.RecentlyUsedProjectItem.Items.RemoveAt(this.RecentlyUsedProjectItem.Items.Count - 1);
            }
        }

        #endregion

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public List<DanceBarItemModelBase> CreateMainMenu()
        {
            List<DanceBarItemModelBase> result = [];

            // 新建项目
            this.CreateProjectItem.Content = "新建项目";
            this.CreateProjectItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Project;component/Themes/Icons/create_project.svg");
            this.CreateProjectItem.ClickCommand = new(COMMAND_GROUP, "新建项目", this.CreateProject);

            // 打开项目
            this.OpenProjectItem.Content = "打开项目";
            this.OpenProjectItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Project;component/Themes/Icons/open_project.svg");
            this.OpenProjectItem.ClickCommand = new(COMMAND_GROUP, "打开项目", this.OpenProject);

            // 最近项目
            this.RecentlyUsedProjectItem.Content = "最近项目";
            var recentlyUsedProjects = this.ConfigManager.Context.GetRecentlyUsedProjects();
            var projects = recentlyUsedProjects.FindAll().OrderByDescending(p => p.LastOpenTime).Take(ProjectOptions.RecentlyUsedProjectCount);
            foreach (var project in projects)
            {
                if (project == null || string.IsNullOrWhiteSpace(project.ProjectPath))
                    continue;

                this.RecentlyUsedProjectItem.Items.Add(new DanceBarButtonItemModel()
                {
                    Content = project.ProjectPath,
                    Tag = project,
                    ClickCommand = new DanceCommand(COMMAND_GROUP, "最近项目", async () => await this.OpenProject(project.ProjectPath))
                });
            }

            // 关闭项目
            this.CloseProjectItem.Content = "关闭项目";
            this.CloseProjectItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Project;component/Themes/Icons/close_project.svg");
            this.CloseProjectItem.ClickCommand = new(COMMAND_GROUP, "关闭项目", this.CloseProject);

            // 主菜单
            this.MainSubItem.Content = "项目(_P)";
            this.MainSubItem.Order = DefaultMainMenuOrders.Project;

            this.MainSubItem.Items.Add(this.CreateProjectItem);
            this.MainSubItem.Items.Add(this.OpenProjectItem);
            this.MainSubItem.Items.Add(this.RecentlyUsedProjectItem);
            this.MainSubItem.Items.Add(this.CloseProjectItem);

            result.Add(this.MainSubItem);

            result.Add(new DanceBarSeparatorItemModel() { Order = DefaultMainMenuOrders.MenuSeparator });
            this.ProjectNameItem.Order = DefaultMainMenuOrders.ProjectName;
            result.Add(this.ProjectNameItem);

            return result;
        }


        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 新建项目
        /// </summary>
        private async Task CreateProject()
        {
            try
            {
                // 如果当前项目未关闭，那么提示
                if (this.ProjectManager.Current != null)
                {
                    if (this.MessageManager.Show("关闭项目", "是否关闭当前项目?", MessageBoxButton.YesNo, MessageBoxImage.Question, null) != MessageBoxResult.Yes)
                        return;

                    if (!this.CloseProjectCore())
                        return;
                }

                // 创建项目窗口
                CreateProjectWindow window = new()
                {
                    Owner = this.WindowManager.MainWindow
                };

                if (window.ShowDialog() != true || window.DataContext is not CreateProjectWindowModel vm)
                    return;

                if (string.IsNullOrWhiteSpace(vm.ProjectName) || vm.SelectedProjectCategory == null || vm.SelectedProjectCategory.PluginInfo == null)
                    return;

                if (string.IsNullOrWhiteSpace(vm.WorkPath) || !Directory.Exists(vm.WorkPath))
                    return;

                // 保存项目文件
                string path = Path.Combine(vm.WorkPath, $"{vm.ProjectName}{ProjectOptions.ProjectExtension}");
                ProjectConfig config = new()
                {
                    Name = vm.ProjectName,
                    Detail = vm.ProjectDetail,
                    PluginKey = vm.SelectedProjectCategory.PluginInfo.ID.Key,
                    PluginName = vm.SelectedProjectCategory.PluginInfo.Name
                };

                config.ToJsonFile(path);

                // 打开项目
                this.OpenProjectCore(vm.WorkPath, path, config);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                this.MessageManager.ShowError(ex.Message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打开项目
        /// </summary>
        private async Task OpenProject()
        {
            // 如果当前项目未关闭，那么提示
            if (this.ProjectManager.Current != null)
            {
                if (this.MessageManager.Show("关闭项目", "是否关闭当前项目?", MessageBoxButton.YesNo, MessageBoxImage.Question, null) != MessageBoxResult.Yes)
                    return;

                if (!this.CloseProjectCore())
                    return;
            }

            OpenFileDialog ofd = new()
            {
                Multiselect = false,
                Filter = $"project (*{ProjectOptions.ProjectExtension})|*{ProjectOptions.ProjectExtension}"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            ProjectConfig? config = FromJsonFile<ProjectConfig>(ofd.FileName);
            if (config == null || Path.GetDirectoryName(ofd.FileName) is not string workPath)
            {
                log.Error($"读取项目配置文件失败, 路径: {ofd.FileName}");
                this.MessageManager.ShowError("读取项目配置文件失败");
                return;
            }

            this.OpenProjectCore(workPath, ofd.FileName, config);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打开项目
        /// </summary>
        /// <param name="projectPath">项目路径</param>
        /// <returns>是否成功打开</returns>
        private async Task<bool> OpenProject(string projectPath)
        {
            // 如果当前项目未关闭，那么提示
            if (this.ProjectManager.Current != null)
            {
                if (this.ProjectManager.Current.ProjectPath == projectPath)
                    return false;

                if (this.MessageManager.Show("关闭项目", "是否关闭当前项目?", MessageBoxButton.YesNo, MessageBoxImage.Question, null) != MessageBoxResult.Yes)
                    return false;

                if (!this.CloseProjectCore())
                    return false;
            }

            ProjectConfig? config = FromJsonFile<ProjectConfig>(projectPath);
            if (config == null || Path.GetDirectoryName(projectPath) is not string workPath)
            {
                log.Error($"读取项目配置文件失败, 路径: {projectPath}");
                this.MessageManager.ShowError("读取项目配置文件失败");
                return false;
            }

            this.OpenProjectCore(workPath, projectPath, config);

            await Task.CompletedTask;
            return true;
        }

        /// <summary>
        /// 关闭项目
        /// </summary>
        private async Task CloseProject()
        {
            this.CloseProjectCore();

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打开项目
        /// </summary>
        /// <param name="workPath">工作路径</param>
        /// <param name="projectPath">项目路径</param>
        /// <param name="config">项目配置文件</param>
        /// <returns>是否成功打开</returns>
        private bool OpenProjectCore(string workPath, string projectPath, ProjectConfig config)
        {
            if (this.ProjectManager.Current != null)
                return false;

            DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(p => p.PluginInfo.ID.Key == config.PluginKey);
            if (pluginDomain == null || pluginDomain.PluginInfo is not ProjectPluginInfo pluginInfo)
            {
                log.Error($"未找到项目插件: {config.PluginKey}");
                return false;
            }

            ProjectOpeningMsg openingMsg = new(workPath);
            DanceDomain.Current.Messenger.Send(openingMsg);
            if (openingMsg.IsCancel)
                return false;

            ProjectDomain project = new(workPath, projectPath, pluginInfo)
            {
                Name = config.Name,
                Detail = config.Detail
            };

            this.ProjectManager.Current = project;

            ProjectOpendMsg opendMsg = new();
            DanceDomain.Current.Messenger.Send(opendMsg);

            return true;
        }

        /// <summary>
        /// 关闭项目
        /// </summary>
        /// <returns>是否成功关闭</returns>
        private bool CloseProjectCore()
        {
            if (this.ProjectManager.Current == null)
                return true;

            ProjectClosingMsg closingMsg = new();
            DanceDomain.Current.Messenger.Send(closingMsg);
            if (closingMsg.IsCancel)
                return false;

            ProjectClosedMsg closedMsg = new(this.ProjectManager.Current);
            this.ProjectManager.Current = null;
            DanceDomain.Current.Messenger.Send(closedMsg);

            return true;
        }
    }
}
