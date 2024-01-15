using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 缓存池
    /// </summary>
    /// <param name="key">键</param>
    public class DanceCachePool(object key) : DanceObject
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly Dictionary<object, IDanceCacheObject> Caches = [];

        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; } = key;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="type">缓存类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetCache(DanceCacheType type, object key, object? value)
        {
            lock (this.Caches)
            {
                if (type == DanceCacheType.Weak)
                {
                    IDanceCacheObject obj = new DanceWeakCacheObject(key, value);
                    this.Caches[key] = obj;
                }
                else
                {
                    IDanceCacheObject obj = new DanceStrongCacheObject(key, value);
                    this.Caches[key] = obj;
                }
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object? GetCache(object key)
        {
            this.Caches.TryGetValue(key, out IDanceCacheObject? value);
            return value?.GetTarget();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            lock (this.Caches)
            {
                List<IDanceCacheObject> items = [.. this.Caches.Values];
                foreach (IDanceCacheObject item in items)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch
                    {
                        // nothing todo.
                    }
                }
            }
        }
    }
}