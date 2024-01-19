using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 验证类型信息
    /// </summary>
    /// <param name="type">类型</param>
    public class DanceValidateTypeInfo(Type type)
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; } = type;

        /// <summary>
        /// 可以验证的属性集合
        /// </summary>
        public Dictionary<string, PropertyInfo> Properties { get; } = [];
    }
}
