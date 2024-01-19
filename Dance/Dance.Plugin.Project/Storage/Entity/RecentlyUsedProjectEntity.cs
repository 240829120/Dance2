using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 最近使用项目实体
    /// </summary>
    public class RecentlyUsedProjectEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [BsonId(true)]
        public int ID { get; set; }

        /// <summary>
        /// 项目文件地址
        /// </summary>
        public string? ProjectPath { get; set; }

        /// <summary>
        /// 最后打开时间
        /// </summary>
        public DateTime LastOpenTime { get; set; }
    }
}
