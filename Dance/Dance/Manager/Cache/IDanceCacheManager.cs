using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public interface IDanceCacheManager : IDisposable
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="pool">池子</param>
        /// <param name="type">缓存类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void SetCache(object pool, DanceCacheType type, object key, object? value);

        /// <summary>
        /// 获取缓存池
        /// </summary>
        /// <param name="pool">缓存池</param>
        /// <returns>缓存池</returns>
        DanceCachePool GetCachePool(object pool);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="pool">池子</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object? GetCache(object pool, object key);
    }
}
