﻿using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Collections;
using Dance;
using DevExpress.Xpf.Core;

namespace Dance.Plugin.LayoutManage
{
    /// <summary>
    /// Interaction logic for LayoutInputNameWindow.xaml
    /// </summary>
    public partial class LayoutInputNameWindow : ThemedWindow
    {
        public LayoutInputNameWindow()
        {
            InitializeComponent();

            if (DanceXamlHelper.IsInDesignMode)
                return;

            this.DataContext = new LayoutInputNameWindowModel() { View = this };
        }
    }
}