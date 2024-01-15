using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using log4net;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 缓存
    /// </summary>
    public class DanceDomainBuilder_Cache : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "缓存";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {

        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceCacheManager cacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();
            if (cacheManager == null)
                return;

            cacheManager.Dispose();
        }
    }
}
