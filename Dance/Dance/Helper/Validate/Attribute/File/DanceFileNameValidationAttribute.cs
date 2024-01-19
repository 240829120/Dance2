using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 验证文件名是否合法
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DanceFileNameValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// <inheritdoc cref="ValidationAttribute.IsValid(object?)"/>
        /// </summary>
        public override bool IsValid(object? value)
        {
            if (value is not string fileName)
                return false;

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in fileName)
            {
                if (Array.IndexOf(invalidChars, c) != -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
