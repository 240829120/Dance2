using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dance.Framework
{
    /// <summary>
    /// 子项模型集合
    /// </summary>
    public class DanceBarSubItemModel : DanceBarItemModelBase
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DanceBarItemModelBase> Items { get; } = [];
    }
}
