using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    [DanceSingleton(typeof(IDanceCacheManager))]
    public class DanceCacheManager : DanceObject, IDanceCacheManager
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly Dictionary<object, DanceCachePool> CachePools = [];

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="pool">池子</param>
        /// <param name="type">缓存类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetCache(object pool, DanceCacheType type, object key, object? value)
        {
            DanceCachePool? cachePool = null;

            lock (this.CachePools)
            {
                if (!this.CachePools.TryGetValue(pool, out cachePool))
                {
                    this.CachePools[pool] = cachePool = new(pool);
                }
            }

            cachePool.SetCache(type, key, value);
        }

        /// <summary>
        /// 获取缓存池
        /// </summary>
        /// <param name="pool">缓存池</param>
        /// <returns>缓存池</returns>
        public DanceCachePool GetCachePool(object pool)
        {
            DanceCachePool? cachePool = null;

            lock (this.CachePools)
            {
                if (!this.CachePools.TryGetValue(pool, out cachePool))
                {
                    this.CachePools[pool] = cachePool = new(pool);
                }
            }

            return cachePool;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="pool">池子</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object? GetCache(object pool, object key)
        {
            this.CachePools.TryGetValue(pool, out DanceCachePool? cachePool);
            if (cachePool == null)
                return default;

            return cachePool.GetCache(key);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            lock (this.CachePools)
            {
                List<DanceCachePool> items = [.. this.CachePools.Values];
                foreach (DanceCachePool item in items)
                {
                    item.Dispose();
                }
            }
        }
    }
}