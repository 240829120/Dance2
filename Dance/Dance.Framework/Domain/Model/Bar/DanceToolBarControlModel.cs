using DevExpress.Diagram.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 工具项模型
    /// </summary>
    public class DanceToolBarControlModel : DanceModel
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DanceBarItemModelBase> Items { get; } = [];

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
