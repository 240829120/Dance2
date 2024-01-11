﻿using DevExpress.Xpf.Editors.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Framework
{
    /// <summary>
    /// 编辑项模型基类
    /// </summary>
    public abstract class DanceBarEditItemModelBase : DanceBarItemModelBase
    {
        // ======================================================================
        // Property
        // ======================================================================

        #region EditValue -- 编辑值

        private object? editValue;
        /// <summary>
        /// 编辑值
        /// </summary>
        public object? EditValue
        {
            get { return editValue; }
            set
            {
                object? oldValue = editValue;
                object? newValue = value;

                this.SetProperty(ref editValue, value);
                this.OnEditValueChanged(oldValue, newValue);
            }
        }

        #endregion

        #region EditSettings -- 编辑设置

        private BaseEditSettings? editSettings;
        /// <summary>
        /// 编辑设置
        /// </summary>
        public BaseEditSettings? EditSettings
        {
            get { return editSettings; }
            set { this.SetProperty(ref editSettings, value); }
        }

        #endregion

        #region EditTemplate -- 编辑模板

        private DataTemplate? editTemplate;
        /// <summary>
        /// 编辑模板
        /// </summary>
        public DataTemplate? EditTemplate
        {
            get { return editTemplate; }
            set { this.SetProperty(ref editTemplate, value); }
        }

        #endregion

        #region EditWidth -- 编辑器宽度

        private double editWidth = 60d;
        /// <summary>
        /// 编辑器宽度
        /// </summary>
        public double EditWidth
        {
            get { return editWidth; }
            set { this.SetProperty(ref editWidth, value); }
        }

        #endregion

        #region EditHeight -- 编辑器高度

        private double editHeight = 24d;
        /// <summary>
        /// 编辑器高度
        /// </summary>
        public double EditHeight
        {
            get { return editHeight; }
            set { this.SetProperty(ref editHeight, value); }
        }

        #endregion

        // ======================================================================
        // Private Function
        // ======================================================================

        /// <summary>
        /// 当值改变时触发
        /// </summary>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        protected abstract void OnEditValueChanged(object? oldValue, object? newValue);
    }
}
