using Dance.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 布局模型
    /// </summary>
    /// <param name="entity">实体</param>
    public class LayoutModel(LayoutEntity entity) : DanceModel
    {
        /// <summary>
        /// <inheritdoc cref="LayoutEntity"/>
        /// </summary>
        public LayoutEntity Entity { get; } = entity;

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
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
