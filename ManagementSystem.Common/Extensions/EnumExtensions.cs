using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ManagementSystem.Common.Extensions
{
    /// <summary>
    /// 枚举的扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取到对应枚举的描述-没有描述信息，返回枚举值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string EnumDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            string name = Enum.GetName(type, @enum);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            if (!(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute))
            {
                return name;
            }
            return attribute?.Description;
        }
        public static int ToEnumInt(this Enum e)
        {
            try
            {
                return e.GetHashCode();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
