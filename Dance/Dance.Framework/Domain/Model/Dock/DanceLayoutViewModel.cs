using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Framework
{
    /// <summary>
    /// 布局模型
    /// </summary>
    public class DanceLayoutViewModel : DanceDockItemViewModelBase
    {
        #region PluginInfo -- 插件信息

        private DanceLayoutViewPluginInfo? pluginInfo;
        /// <summary>
        /// 插件信息
        /// </summary>
        public DanceLayoutViewPluginInfo? PluginInfo
        {
            get { return pluginInfo; }
            set { this.SetProperty(ref pluginInfo, value); }
        }

        #endregion
    }
}
