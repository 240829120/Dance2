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
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        public const string COMMAND_GROUP = "项目菜单";

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
        private readonly DanceBarSubItemModel OpenUsedProjectItem = new();

        /// <summary>
        /// 关闭项目菜单
        /// </summary>
        private readonly DanceBarButtonItemModel CloseProjectItem = new();

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 创建主菜单
        /// </summary>
        /// <returns>主菜单</returns>
        public DanceBarSubItemModel CreateMainMenu()
        {
            // 新建项目
            this.CreateProjectItem.Content = "新建项目";
            this.CreateProjectItem.Glyph = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Project;component/Themes/Icons/project.svg");
            this.CreateProjectItem.ClickCommand = new(COMMAND_GROUP, "新建项目", this.CreateProject);

            // 打开项目
            this.OpenProjectItem.Content = "打开项目";
            this.OpenProjectItem.ClickCommand = new(COMMAND_GROUP, "打开项目", this.OpenProject);

            // 最近项目
            this.OpenUsedProjectItem.Content = "最近项目";

            // 关闭项目
            this.CloseProjectItem.Content = "关闭项目";
            this.CloseProjectItem.ClickCommand = new(COMMAND_GROUP, "关闭项目", this.CloseProject);

            // 主菜单
            this.MainSubItem.Content = "项目(_P)";
            this.MainSubItem.Order = -1000;

            this.MainSubItem.Items.Add(this.CreateProjectItem);
            this.MainSubItem.Items.Add(this.OpenProjectItem);
            this.MainSubItem.Items.Add(this.OpenUsedProjectItem);
            this.MainSubItem.Items.Add(this.CloseProjectItem);

            return this.MainSubItem;
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

                if (string.IsNullOrWhiteSpace(vm.ProjectPath) || !Directory.Exists(vm.ProjectPath))
                    return;

                // 保存项目文件
                string path = Path.Combine(vm.ProjectPath, $"{vm.ProjectName}{ProjectOptions.ProjectExtension}");
                ProjectConfig config = new()
                {
                    Name = vm.ProjectName,
                    Detail = vm.ProjectDetail,
                    PluginID = vm.SelectedProjectCategory.PluginInfo.ID,
                    PluginName = vm.SelectedProjectCategory.PluginInfo.Name
                };

                config.ToJsonFile(path);

                // 打开项目
                this.OpenProjectCore(vm.ProjectPath, config);
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
            if (config == null || Path.GetDirectoryName(ofd.FileName) is not string workpath)
            {
                log.Error($"读取项目配置文件失败, 路径: {ofd.FileName}");
                this.MessageManager.ShowError("读取项目配置文件失败");
                return;
            }

            this.OpenProjectCore(workpath, config);

            await Task.CompletedTask;
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
        /// <param name="config">项目配置文件</param>
        /// <returns>是否成功打开</returns>
        private bool OpenProjectCore(string workPath, ProjectConfig config)
        {
            if (this.ProjectManager.Current != null)
                return false;

            DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(p => p.PluginInfo.ID == config.PluginID);
            if (pluginDomain == null || pluginDomain.PluginInfo is not ProjectPluginInfo pluginInfo)
            {
                log.Error($"未找到项目插件: {config.PluginID}, {config.PluginName}");
                return false;
            }

            ProjectOpeningMsg openingMsg = new(workPath);
            DanceDomain.Current.Messenger.Send(openingMsg);
            if (openingMsg.IsCancel)
                return false;

            ProjectDomain project = new(workPath, pluginInfo)
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
