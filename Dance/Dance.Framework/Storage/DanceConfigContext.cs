using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 配置上下文
    /// </summary>
    public class DanceConfigContext : DanceObject
    {
        public DanceConfigContext()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.db");
            ConnectionString conn = new()
            {
                Connection = ConnectionType.Shared,
                Filename = path
            };
            this.Database = new(conn);
        }

        /// <summary>
        /// 缓存数据库
        /// </summary>
        public LiteDatabase Database { get; }
    }
}
