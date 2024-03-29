﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Wpf
{
    /// <summary>
    /// Xaml辅助类
    /// </summary>
    public static class DanceXamlHelper
    {
        /// <summary>
        /// 虚对象
        /// </summary>
        private class VISUAL : FrameworkElement
        {
            public VISUAL()
            {
                this.DpiScale = VisualTreeHelper.GetDpi(this);
            }

            /// <summary>
            /// Dpi信息
            /// </summary>
            public DpiScale DpiScale { get; private set; }

            protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
            {
                base.OnDpiChanged(oldDpi, newDpi);

                this.DpiScale = newDpi;
            }
        }

        /// <summary>
        /// 虚拟对象
        /// </summary>
        private static readonly VISUAL Visual = new();

        /// <summary>
        /// 是否处于设计模式中
        /// </summary>
        /// <returns>是否处于设计模式中</returns>
        public static bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(Visual);
            }
        }

        /// <summary>
        /// DPI
        /// </summary>
        public static DpiScale DpiScale
        {
            get
            {
                return Visual.DpiScale;
            }
        }

        /// <summary>
        /// 使用可视化树查找父级控件
        /// </summary>
        /// <typeparam name="T">要查找的控件类型</typeparam>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static T? GetVisualTreeParent<T>(this DependencyObject element) where T : DependencyObject
        {
            return GetVisualTreeParent(element, typeof(T)) as T;
        }

        /// <summary>
        /// 使用可视化树查找父级控件
        /// </summary>
        /// <param name="type">父元素类型</param>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static object? GetVisualTreeParent(this DependencyObject element, Type type)
        {
            if (element == null)
                return null;

            if (type.IsAssignableFrom(element.GetType()))
                return element;

            return GetVisualTreeParent(VisualTreeHelper.GetParent(element), type);
        }

        /// <summary>
        /// 使用可视化树查找子控件
        /// </summary>
        /// <param name="element">查找的开始控件</param>
        /// <param name="type">子控件类型</param>
        /// <returns>查找结果</returns>
        public static List<DependencyObject> GetVisualTreeDescendants(this DependencyObject? element, Type type)
        {
            List<DependencyObject> result = [];

            if (element == null)
                return result;

            if (type.IsAssignableFrom(element.GetType()))
            {
                result.Add(element);
                return result;
            }

            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                result.AddRange(GetVisualTreeDescendants(child, type));
            }

            return result;
        }

        /// <summary>
        /// 使用可视化树查找子控件
        /// </summary>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="element">查找的开始控件</param>
        /// <returns>查找结果</returns>
        public static List<T> GetVisualTreeDescendants<T>(this DependencyObject? element) where T : DependencyObject
        {
            return GetVisualTreeDescendants(element, typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 使用可视化树查找子控件
        /// </summary>
        /// <param name="element">查找的开始控件</param>
        /// <param name="action">遍历行为</param>
        public static void TraversalVisualTree(this DependencyObject? element, Action<DependencyObject>? action)
        {
            if (element == null || action == null)
                return;

            action.Invoke(element);

            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);

                TraversalVisualTree(child, action);
            }
        }

        /// <summary>
        /// 创建视图的数据模板
        /// </summary>
        /// <param name="viewType">视图类型</param>
        /// <returns>创建数据模板</returns>
        public static DataTemplate? CreateDataTemplate(Type viewType)
        {
            DataTemplate? template = new()
            {
                VisualTree = new FrameworkElementFactory(viewType)
            };
            template.Seal();

            return template;
        }

        /// <summary>
        /// 尝试切换至UI线程执行
        /// </summary>
        /// <param name="action">行为</param>
        public static void Invoke(Action action)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(action);
            }
        }
    }
}