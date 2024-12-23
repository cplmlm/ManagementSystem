using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model
{
    public static class EnumHelper
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
    }
}
