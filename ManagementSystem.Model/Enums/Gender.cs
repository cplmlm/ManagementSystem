using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.Enums
{
    public enum Gender
    {
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男性")]
        Male=1,
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Female=2,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown=3
    }
}
