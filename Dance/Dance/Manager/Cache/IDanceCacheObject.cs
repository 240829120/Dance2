using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 缓存对象
    /// </summary>
    public interface IDanceCacheObject : IDisposable
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        DanceCacheType Type { get; }

        /// <summary>
        /// 键
        /// </summary>
        object Key { get; }

        /// <summary>
        /// 获取目标
        /// </summary>
        /// <returns>目标</returns>
        object? GetTarget();
    }
}
