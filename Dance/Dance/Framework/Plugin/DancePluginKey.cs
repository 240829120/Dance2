using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件键
    /// </summary>
    public partial class DancePluginKey
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="group">分组</param>
        /// <param name="id">编号</param>
        public DancePluginKey(string nameSpace, string group, string id)
        {
            this.NameSpace = nameSpace;
            this.Group = group;
            this.ID = id;
            this.MD5 = DanceEncryptHelper.MD5($"{nameSpace}{group}{id}");
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// MD5
        /// </summary>
        public string MD5 { get; }
    }
}
