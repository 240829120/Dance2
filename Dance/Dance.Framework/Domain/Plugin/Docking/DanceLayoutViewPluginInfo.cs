﻿using Dance.Framework.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 面板视图插件信息
    /// </summary>
    /// <param name="id">插件编号</param>
    /// <param name="name">名称</param>
    /// <param name="viewType">视图类型</param>
    public class DanceLayoutViewPluginInfo(DancePluginKey id, string name, Type viewType) : DanceDockingItemPluginInfoBase(id, name, viewType)
    {

    }
}
