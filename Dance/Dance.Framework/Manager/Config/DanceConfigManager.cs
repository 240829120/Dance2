using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceConfigManager))]
    public class DanceConfigManager : IDanceConfigManager
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        public DanceConfigContext Context { get; } = new();
    }
}
