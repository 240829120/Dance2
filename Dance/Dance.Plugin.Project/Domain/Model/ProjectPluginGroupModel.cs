using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目插件分组模型
    /// </summary>
    public class ProjectPluginGroupModel : DanceModel
    {
        #region GroupName -- 分组名

        private string? groupName;

        /// <summary>
        /// 分组名
        /// </summary>
        public string? GroupName
        {
            get { return groupName; }
            set { this.SetProperty(ref groupName, value); }
        }

        #endregion

        #region Items -- 子项集合

        private DanceObservableCollection<ProjectPluginInfo>? items;

        /// <summary>
        /// 子项集合
        /// </summary>
        public DanceObservableCollection<ProjectPluginInfo>? Items
        {
            get { return items; }
            set { this.SetProperty(ref items, value); }
        }

        #endregion
    }
}
