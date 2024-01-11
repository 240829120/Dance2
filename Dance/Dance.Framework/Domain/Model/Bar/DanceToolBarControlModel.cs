using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework.Domain.Model.Bar
{
    /// <summary>
    /// 工具项模型
    /// </summary>
    public class DanceToolBarControlModel : DanceBarItemModelBase
    {
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DanceBarItemModelBase> Items { get; } = [];
    }
}
