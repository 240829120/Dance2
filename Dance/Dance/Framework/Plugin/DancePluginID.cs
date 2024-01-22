using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件ID
    /// </summary>
    public partial class DancePluginID
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="group">分组</param>
        /// <param name="id">编号</param>
        public DancePluginID(string nameSpace, string group, string id)
        {
            if (!FormatRegex().IsMatch(nameSpace))
                throw new ArgumentException("nameSpace format error.", nameof(nameSpace));

            if (!FormatRegex().IsMatch(group))
                throw new ArgumentException("group format error.", nameof(nameSpace));

            if (!FormatRegex().IsMatch(id))
                throw new ArgumentException("id format error.", nameof(nameSpace));

            this.NameSpace = nameSpace;
            this.Group = group;
            this.ID = id;
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
        /// 键
        /// </summary>
        public string Key
        {
            get
            {
                return $"{this.NameSpace}_{this.Group}_{this.ID}";
            }
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        public override string ToString()
        {
            return this.Key;
        }

        /// <summary>
        /// 格式化正则表达式
        /// </summary>
        [GeneratedRegex("^[a-zA-Z]+[a-zA-Z1-9]*$")]
        private static partial Regex FormatRegex();
    }
}
