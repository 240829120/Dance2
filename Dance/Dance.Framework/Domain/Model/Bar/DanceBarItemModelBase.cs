﻿using DevExpress.Map.Kml.Model;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Bars;

namespace Dance.Framework
{
    /// <summary>
    /// 菜单项模型基类
    /// </summary>
    public class DanceBarItemModelBase : DanceModel
    {
        #region CategoryName -- 类型名称

        private string? categoryName;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName
        {
            get { return categoryName; }
            set { this.SetProperty(ref categoryName, value); }
        }

        #endregion

        #region Glyph -- 图标

        private ImageSource? glyph;

        public ImageSource? Glyph
        {
            get { return glyph; }
            set { this.SetProperty(ref glyph, value); }
        }

        #endregion

        #region IsEnabled -- 是否可用

        private bool isEnabled = true;
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.SetProperty(ref isEnabled, value); }
        }

        #endregion

        #region Content -- 内容

        private object? content;
        /// <summary>
        /// 内容
        /// </summary>
        public object? Content
        {
            get { return content; }
            set { this.SetProperty(ref content, value); }
        }

        #endregion

        #region ToolTip -- 提示

        private object? toolTip;
        /// <summary>
        /// 提示
        /// </summary>
        public object? ToolTip
        {
            get { return toolTip ?? this.content; }
            set { this.SetProperty(ref toolTip, value); }
        }

        #endregion

        #region KeyGesture -- 快捷键

        private KeyGesture? keyGesture;
        /// <summary>
        /// 快捷键
        /// </summary>
        public KeyGesture? KeyGesture
        {
            get { return keyGesture; }
            set { this.SetProperty(ref keyGesture, value); }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { this.SetProperty(ref description, value); }
        }

        #endregion

        #region Alignment -- 对齐

        private BarItemAlignment alignment;
        /// <summary>
        /// 对齐
        /// </summary>
        public BarItemAlignment Alignment
        {
            get { return alignment; }
            set { this.SetProperty(ref alignment, value); }
        }

        #endregion

        #region Order -- 排序

        private int order;

        /// <summary>
        /// 排序
        /// </summary>
        public int Order
        {
            get { return order; }
            set { this.SetProperty(ref order, value); }
        }

        #endregion

        #region Tag -- 标志

        private object? tag;
        /// <summary>
        /// 标志
        /// </summary>
        public object? Tag
        {
            get { return tag; }
            set { this.SetProperty(ref tag, value); }
        }

        #endregion
    }
}
