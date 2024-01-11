using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 项容器模型
    /// </summary>
    public class DanceBarContainerModel : DanceBarItemModelBase
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DanceBarItemModelBase> Items { get; } = [];
    }
}
