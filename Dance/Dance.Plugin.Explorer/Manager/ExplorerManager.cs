﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器管理器
    /// </summary>
    [DanceSingleton(typeof(IExplorerManager))]
    public class ExplorerManager : IExplorerManager
    {
        /// <summary>
        /// 资源管理器信息
        /// </summary>
        public List<ExplorerInfo> ExplorerInfos { get; } = [];
    }
}
