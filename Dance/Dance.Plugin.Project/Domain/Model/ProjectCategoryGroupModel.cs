using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目分类分组模型
    /// </summary>
    public class ProjectCategoryGroupModel : DanceModel
    {
        #region GroupName -- 分组名称

        private string? groupName;

        /// <summary>
        /// 分组名称
        /// </summary>
        public string? GroupName
        {
            get { return groupName; }
            set { this.SetProperty(ref groupName, value); }
        }

        #endregion

        #region Items -- 子项集合

        /// <summary>
        /// 子项集合
        /// </summary>
        public DanceObservableCollection<ProjectCategoryModel> Items { get; } = [];

        #endregion
    }
}
