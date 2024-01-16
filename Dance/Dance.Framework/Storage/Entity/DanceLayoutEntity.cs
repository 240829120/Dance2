using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 布局实体
    /// </summary>
    public class DanceLayoutEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [BsonId(true)]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string? Content { get; set; }
    }
}
