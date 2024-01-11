using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Framework
{
    /// <summary>
    /// 布局模型
    /// </summary>
    public class DanceLayoutViewModel : DanceViewModel
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

        #region IsClosed -- 是否已经被关闭

        private bool isClosed;
        /// <summary>
        /// 是否已经被关闭
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
