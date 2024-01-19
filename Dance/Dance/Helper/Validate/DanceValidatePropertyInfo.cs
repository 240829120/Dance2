using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 验证属性信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="property">属性</param>
    public class DanceValidatePropertyInfo(Type type, PropertyInfo property)
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; } = type;

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo Property { get; } = property;

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors { get; } = [];

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage => string.Join(Environment.NewLine, this.Errors);
    }
}
