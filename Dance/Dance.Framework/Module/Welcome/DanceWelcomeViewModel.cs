using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using log4net.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 欢迎视图模型
    /// </summary>
    [DanceSingleton]
    public class DanceWelcomeViewModel : DanceViewModel
    {
        public DanceWelcomeViewModel()
        {
            this.LoadedCommand = new(COMMAND_GROUP, "加载", this.Loaded);
        }

        // =======================================================================================
        // Field

        /// <summary>
        /// 命令分组
        /// </summary>
        private const string COMMAND_GROUP = "欢迎";

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IDanceWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();

        // =======================================================================================
        // Property

        #region ProgressValue -- 进度值

        private double progressValue;
        /// <summary>
        /// 进度值
        /// </summary>
        public double ProgressValue
        {
            get { return progressValue; }
            set { this.SetProperty(ref progressValue, value); }
        }

        #endregion

        #region ProgressMessage -- 进度消息

        private string? progressMessage;
        /// <summary>
        /// 进度消息
        /// </summary>
        public string? ProgressMessage
        {
            get { return progressMessage; }
            set { this.SetProperty(ref progressMessage, value); }
        }

        #endregion

        // =======================================================================================
        // Command

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
            this.WindowManager.WelcomeWindow.Closed -= Window_Closed;
            this.WindowManager.WelcomeWindow.Closed += Window_Closed;
            this.WindowManager.MainWindow.Closed += Window_Closed;
            this.WindowManager.MainWindow.Closed += Window_Closed;

            this.ProgressValue = 0;
            this.ProgressMessage = "准备初始化";

            // 加载插件
            foreach (Assembly assembly in DanceDomain.Current.PluginBuilder.PluginAssemblies)
            {
                DanceDomain.Current.PluginBuilder.LoadPlugin(assembly);
            }

            for (int i = 0; i < DanceDomain.Current.PluginBuilder.PluginDomains.Count; i++)
            {
                DancePluginDomain pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains[i];
                this.ProgressValue = (double)i / DanceDomain.Current.PluginBuilder.PluginDomains.Count;
                this.ProgressMessage = $"正在加载: {pluginDomain.PluginInfo.Name}";

                DanceDomain.Current.PluginBuilder.InitializePlugin(pluginDomain);

                await Task.Delay(50);
            }

            // 准备启动主界面
            this.ProgressValue = 1;
            this.ProgressMessage = "准备启动";

            await Task.Delay(2000);

            this.WindowManager.WelcomeWindow.Closed -= Window_Closed;
            WindowManager.WelcomeWindow.Close();
            WindowManager.MainWindow.Show();
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void Window_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        #endregion
    }
}
