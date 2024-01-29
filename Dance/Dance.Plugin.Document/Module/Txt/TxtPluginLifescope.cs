using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Document
{
    /// <summary>
    /// 文本插件生命周期
    /// </summary>
    public class TxtPluginLifescope : DanceObject, IDancePluginLifescope
    {
        // ===================================================================================================
        // **** IDancePluginLifescope ****
        // ===================================================================================================

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IDancePluginInfo Register()
        {
            return new DanceDocumentViewPluginInfo(new("Dance", "Document", "Txt"), "文本文档", typeof(TxtView), typeof(TxtViewModel), ".txt");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {

        }
    }
}