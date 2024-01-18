using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目分类不能为空验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ProjectCategoryNotNullAttribute : ValidationAttribute
    {
        /// <summary>
        /// <inheritdoc cref="ValidationAttribute.IsValid(object?)"/>
        /// </summary>
        public override bool IsValid(object? value)
        {
            if (value is not ProjectCategoryModel model)
                return false;

            return model.PluginInfo != null;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
