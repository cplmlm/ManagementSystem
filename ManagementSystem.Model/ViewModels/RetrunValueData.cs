using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model
{
    /// <summary>
    /// 有返回值数据
    /// </summary>
    public class RetrunValueData<T>
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public bool Success { get; set; }=false;
    }
}
