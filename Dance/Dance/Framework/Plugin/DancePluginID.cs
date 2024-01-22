using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件ID
    /// </summary>
    /// <param name="nameSpace">命名空间</param>
    /// <param name="group">分组</param>
    /// <param name="iD">编号</param>
    public class DancePluginID(string nameSpace, string group, string iD)
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; } = nameSpace;

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = group;

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; } = iD;

        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get
            {
                return $"[{this.NameSpace}]<{this.Group}>{this.ID}";
            }
        }
    }
}
