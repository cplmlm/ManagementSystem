using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.ViewModels
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class AreaTree
    {
        /// <summary>
        /// 行政区划父级Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 行政区划名称
        /// </summary>
        public string? Label { get; set; }
        /// <summary>
        /// 行政区划子集
        /// </summary>
        public List<AreaTree>? Children { get; set; }
    }
}
