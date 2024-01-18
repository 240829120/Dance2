using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// Docking项视图模型基类
    /// </summary>
    public class DanceDockItemViewModelBase : DanceViewModel
    {
        #region ID -- 编号

        private string? id;
        /// <summary>
        /// 编号
        /// </summary>
        public string? ID
        {
            get { return id; }
            set { this.SetProperty(ref id, value); }
        }

        #endregion

        #region Caption -- 标题

        private string? caption;
        /// <summary>
        /// 标题
        /// </summary>
        public string? Caption
        {
            get { return caption; }
            set { this.SetProperty(ref caption, value); }
        }

        #endregion

        #region ToolTip -- 提示

        private string? toolTip;
        /// <summary>
        /// 提示
        /// </summary>
        public string? ToolTip
        {
            get { return toolTip; }
            set { this.SetProperty(ref toolTip, value); }
        }

        #endregion

        #region IsClosed -- 是否关闭

        private bool isClosed;
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClosed
        {
            get { return isClosed; }
            set { this.SetProperty(ref isClosed, value); }
        }

        #endregion

        #region IsActive -- 是否被激活

        private bool isActive;
        /// <summary>
        /// 是否被激活
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { this.SetProperty(ref isActive, value); }
        }

        #endregion

        #region AllowClose -- 是否允许关闭

        private bool allowClose = true;
        /// <summary>
        /// 是否允许关闭
        /// </summary>
        public bool AllowClose
        {
            get { return allowClose; }
            set { this.SetProperty(ref allowClose, value); }
        }

        #endregion

        #region ViewType -- 视图类型

        private Type? viewType;
        /// <summary>
        /// 视图类型
        /// </summary>
        public Type? ViewType
        {
            get { return viewType; }
            set { this.SetProperty(ref viewType, value); }
        }

        #endregion
    }
}
