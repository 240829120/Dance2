using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 布局模型
    /// </summary>
    /// <param name="entity">实体</param>
    public class DanceLayoutModel(DanceLayoutEntity entity) : DanceModel
    {
        /// <summary>
        /// <inheritdoc cref="DanceLayoutEntity"/>
        /// </summary>
        public DanceLayoutEntity Entity { get; } = entity;

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

        #region Content -- 内容

        private string? content;
        /// <summary>
        /// 内容
        /// </summary>
        public string? Content
        {
            get { return content; }
            set { this.SetProperty(ref content, value); }
        }

        #endregion
    }
}
