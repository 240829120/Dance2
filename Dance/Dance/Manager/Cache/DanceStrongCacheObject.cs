using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 强引用缓存对象
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="key">键</param>
    /// <param name="target">对象</param>
    public class DanceStrongCacheObject(object key, object? target) : DanceObject, IDanceCacheObject
    {
        /// <summary>
        /// 目标
        /// </summary>
        private object? Target { get; } = target;

        /// <summary>
        /// 缓存类型
        /// </summary>
        public DanceCacheType Type { get; } = DanceCacheType.Strong;

        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; } = key;

        /// <summary>
        /// 获取目标
        /// </summary>
        /// <returns>目标</returns>
        public object? GetTarget()
        {
            return this.Target;
        }
    }
}