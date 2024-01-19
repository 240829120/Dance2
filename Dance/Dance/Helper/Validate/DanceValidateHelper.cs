using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 验证辅助类
    /// </summary>
    internal static class DanceValidateHelper
    {
        /// <summary>
        /// 验证类型缓存
        /// </summary>
        private static readonly ConcurrentDictionary<Type, DanceValidateTypeInfo> TypeCache = [];

        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="target">验证目标</param>
        /// <param name="propertyName">属性</param>
        /// <returns>验证信息</returns>
        public static DanceValidatePropertyInfo? ValidateProperty(DanceModel target, string? propertyName)
        {
            if (target == null || string.IsNullOrWhiteSpace(propertyName))
                return null;

            Type type = target.GetType();

            DanceValidateTypeInfo typeInfo = GetValidateTypeInfo(type);
            if (!typeInfo.Properties.TryGetValue(propertyName, out PropertyInfo? property))
                return null;

            return ValidatePropertyCore(type, target, property);
        }

        /// <summary>
        /// 验证对象
        /// </summary>
        /// <param name="target">验证结果</param>
        public static List<DanceValidatePropertyInfo> Validate(DanceModel target)
        {
            Type type = target.GetType();
            DanceValidateTypeInfo typeInfo = GetValidateTypeInfo(type);

            List<DanceValidatePropertyInfo> result = [];

            foreach (var property in typeInfo.Properties)
            {
                DanceValidatePropertyInfo? propertyInfo = ValidatePropertyCore(type, target, property.Value);
                if (propertyInfo == null)
                    continue;

                result.Add(propertyInfo);
            }

            return result;
        }

        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="target">验证目标</param>
        /// <param name="property">属性</param>
        /// <returns>错误信息</returns>
        private static DanceValidatePropertyInfo? ValidatePropertyCore(Type type, DanceModel target, PropertyInfo property)
        {
            ValidationContext context = new(target, null, null)
            {
                MemberName = property.Name
            };

            List<ValidationResult> results = [];
            Validator.TryValidateProperty(property.GetValue(target, null), context, results);

            if (results.Count == 0)
            {
                target.ErrorDic[property.Name] = null;
                target.NotifyPropertyChanged(nameof(DanceModel.Error));
                return null;
            }

            DanceValidatePropertyInfo propertyInfo = new(type, property);
            propertyInfo.Errors.AddRange(results.Select(r => r.ErrorMessage ?? string.Empty));
            target.ErrorDic[property.Name] = propertyInfo;
            target.NotifyPropertyChanged(nameof(DanceModel.Error));

            return propertyInfo;
        }

        /// <summary>
        /// 获取验证类型信息
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns>验证类型信息</returns>
        private static DanceValidateTypeInfo GetValidateTypeInfo(Type type)
        {
            if (TypeCache.TryGetValue(type, out DanceValidateTypeInfo? info))
                return info;

            info = new(type);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                object[] attributes = property.GetCustomAttributes(true);

                if (attributes.Any(p => p is ValidationAttribute))
                {
                    info.Properties[property.Name] = property;
                }
            }

            TypeCache.AddOrUpdate(type, info, (t, o) =>
            {
                info = o;
                return o;
            });

            return info;
        }
    }
}