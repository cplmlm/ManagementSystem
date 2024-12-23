using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 系统参数配置表
    /// </summary>
    [SugarTable("SystemConfig", "系统参数配置表")]
    public class SystemConfig : RootEntityTkey<long>, IDeleteFilter
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        [SugarColumn(ColumnDescription = "父级Id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(ColumnDescription = "序号")]
        public byte SerialNumber { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        [SugarColumn(Length = 100, ColumnDescription = "参数名称")]
        public string? Name { get; set; }
        /// <summary>
        /// 参数编码
        /// </summary>
        [SugarColumn(Length = 50, ColumnDescription = "参数编码")]
        public string? Code { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [SugarColumn(Length = 150, ColumnDescription = "参数值")]
        public string? Value { get; set; }
        /// <summary>
        /// 参数说明
        /// </summary>
        [SugarColumn(Length = 200, ColumnDescription = "参数说明")]
        public string? Description { get; set; }
        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(ColumnDescription = "是否删除")]
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建Id")]
        public long? CreateId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新者id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新者id")]
        public long? ModifyId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新时间")]
        public DateTime? ModifyTime { get; set; }
    }
}
