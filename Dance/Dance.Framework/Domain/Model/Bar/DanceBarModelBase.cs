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

        #region Caption -- 说明

        private string? caption;

        /// <summary>
        /// 说明
        /// </summary>
        public string? Caption
        {
            get { return caption; }
            set { this.SetProperty(ref caption, value); }
        }

        #endregion
    }
}
