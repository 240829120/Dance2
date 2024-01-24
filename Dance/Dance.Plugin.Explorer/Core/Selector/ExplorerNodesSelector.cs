using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点选择器
    /// </summary>
    public class ExplorerNodesSelector : IChildNodesSelector
    {
        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="item">项</param>
        /// <returns>子项</returns>
        public IEnumerable? SelectChildren(object item)
        {
            if (item is not ExplorerNodeModel model)
                return default;

            model.Expand();
            return model.Items;
        }
    }
}
