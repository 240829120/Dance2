using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 验证文件夹存在
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DanceDirectoryExistsValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// <inheritdoc cref="ValidationAttribute.IsValid(object?)"/>
        /// </summary>
        public override bool IsValid(object? value)
        {
            if (value is not string path)
                return false;

            return Directory.Exists(path);
        }
    }
}
