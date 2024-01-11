using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 文档视图模型
    /// </summary>
    public class DanceDocumentViewModel : DanceDockingItemViewModelBase
    {
        #region PluginInfo -- 插件信息

        private DanceDocumentViewPluginInfo? pluginInfo;
        /// <summary>
        /// 插件信息
        /// </summary>
        public DanceDocumentViewPluginInfo? PluginInfo
        {
            get { return pluginInfo; }
            set { this.SetProperty(ref pluginInfo, value); }
        }

        #endregion
    }
}
