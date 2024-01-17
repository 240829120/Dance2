using Dance.Framework;
using Dance.Wpf;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11DarkName;

            DanceDomain.Current = new() { IsDebugMode = true };
            DanceDomain.Current.IocBuilder.AddAssemblies(Assembly.Load("Dance.Framework"));
            DanceDomain.Current.IocBuilder.AddAssemblies(Assembly.Load("Dance.WpfTest"));
            DanceDomain.Current.PluginBuilder.AddAssemblies(Assembly.Load("Dance.WpfTest"));
            DanceDomain.Current.PluginBuilder.AddAssemblies(Assembly.Load("Dance.Plugin.LayoutManage"));
            DanceDomain.Current.Build();

            IDanceWindowManager windowManager = DanceDomain.Current.LifeScope.Resolve<IDanceWindowManager>();
            windowManager.WelcomeWindow = new WelcomeWindow();
            windowManager.MainWindow = new DanceMainWindow();
            this.MainWindow = windowManager.MainWindow;

            windowManager.WelcomeWindow.Show();
        }
    }
}
