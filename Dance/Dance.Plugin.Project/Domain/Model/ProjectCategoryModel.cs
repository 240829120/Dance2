using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目类别模型
    /// </summary>
    public class ProjectCategoryModel : DanceModel
    {
        #region PluginInfo -- 插件信息

        private ProjectPluginInfo? pluginInfo;

        /// <summary>
        /// 插件信息
        /// </summary>
        public ProjectPluginInfo? PluginInfo
        {
            get { return pluginInfo; }
            set { this.SetProperty(ref pluginInfo, value); }
        }

        #endregion

        #region Icon -- 图标

        private ImageSource? icon;
        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource? Icon
        {
            get { return icon; }
            set { this.SetProperty(ref icon, value); }
        }

        #endregion

        #region Name -- 名称

        private string? name;

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { this.SetProperty(ref name, value); }
        }

        #endregion

        #region Detail -- 描述

        private string? detail;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Detail
        {
            get { return detail; }
            set { this.SetProperty(ref detail, value); }
        }

        #endregion

        #region Tags -- 标签

        private List<string>? tags;
        /// <summary>
        /// 标签
        /// </summary>
        public List<string>? Tags
        {
            get { return tags; }
            set { this.SetProperty(ref tags, value); }
        }

        #endregion
    }
}
