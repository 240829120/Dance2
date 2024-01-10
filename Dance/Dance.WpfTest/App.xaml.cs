﻿using AvalonDock.Controls;
using Dance.Wpf;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11DarkName;
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            DanceDomain.Current = new();
            DanceDomain.Current.Build();
        }
    }
}
