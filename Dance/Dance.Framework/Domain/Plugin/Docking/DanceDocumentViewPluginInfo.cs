using Dance.Framework.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 文档视图插件信息
    /// </summary>
    /// <param name="key">插件Key</param>
    /// <param name="name">名称</param>
    /// <param name="viewType">视图类型</param>
    /// <param name="viewModelType">视图模型类型</param>
    /// <param name="extensions">支持的文件扩展名</param>
    public class DanceDocumentViewPluginInfo(DancePluginKey key, string name, Type viewType, Type viewModelType, params string[] extensions) : DanceDockingItemPluginInfoBase(key, name, viewType)
    {
        /// <summary>
        /// 视图模型类型
        /// </summary>
        public Type ViewModelType { get; } = viewModelType;

        /// <summary>
        /// 支持的文件扩展名
        /// </summary>
        public List<string> Extensions { get; } = new(extensions);
    }
}
