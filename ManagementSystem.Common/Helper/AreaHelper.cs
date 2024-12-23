using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Common.Helper
{
    public class AreaHelper
    {
        /// <summary>
        /// 获取行政区划代码
        /// </summary>
        /// <param name="areaArr">前端传入的行政区划数组</param>
        /// <returns></returns>
        public static string GetAreaCode(string areaArrStr)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(areaArrStr) && areaArrStr.Contains(","))
            {
                string[] areaArr = areaArrStr.Split(',');
                result = areaArr[areaArr.Length - 1];
            }
            return result;
        }
    }
}
