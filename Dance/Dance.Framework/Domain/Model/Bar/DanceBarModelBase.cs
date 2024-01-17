using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// Bar模型基类
    /// </summary>
    public class DanceBarModelBase : DanceModel
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DanceBarItemModelBase> Items { get; } = [];

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

        #region Content -- 内容

        private string? content;

        /// <summary>
        /// 内容
        /// </summary>
        public string? Content
        {
            get { return content; }
            set { this.SetProperty(ref content, value); }
        }

        #endregion
    }
}
