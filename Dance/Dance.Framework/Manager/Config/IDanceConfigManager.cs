﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public interface IDanceConfigManager
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        DanceConfigContext Context { get; }
    }
}
