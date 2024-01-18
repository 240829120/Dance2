using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class ProjectController
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
            CreateProjectWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };

            if (window.ShowDialog() != true)
                return;

            await Task.CompletedTask;
        }

        /// <summary>
        /// 打开项目
        /// </summary>
        private async Task OpenProject()
        {

            await Task.CompletedTask;
        }

        /// <summary>
        /// 关闭项目
        /// </summary>
        private async Task CloseProject()
        {
            await Task.CompletedTask;
        }
    }
}
