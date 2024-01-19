using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目领域
    /// </summary>
    /// <param name="workPath">工作路径</param>
    /// <param name="projectPath">项目路径</param>
    /// <param name="pluginInfo">插件信息</param>
    public class ProjectDomain(string workPath, string projectPath, ProjectPluginInfo pluginInfo) : DanceDomainBase
    {
        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 工作路径
        /// </summary>
        public string WorkPath { get; } = workPath;

        /// <summary>
        /// 项目路径
        /// </summary>
        public string ProjectPath { get; } = projectPath;

        /// <summary>
        /// 插件信息
        /// </summary>
        public ProjectPluginInfo PluginInfo { get; } = pluginInfo;

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.NotifyPropertyChanged(); }
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
            set { detail = value; this.NotifyPropertyChanged(); }
        }

        #endregion
    }
}
