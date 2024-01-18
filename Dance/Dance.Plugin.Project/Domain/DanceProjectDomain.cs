using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目领域
    /// </summary>
    /// <param name="workpath">工作路径</param>
    public class DanceProjectDomain(string workpath) : DanceDomainBase
    {
        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 工作路径
        /// </summary>
        public string Workpath { get; } = workpath;

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
