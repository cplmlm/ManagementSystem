using ManagementSystem.Model.Enums;
using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;
using System.Runtime.Intrinsics.Arm;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 字典类型表
    /// </summary>
    [SugarTable("DictionaryType", "字典类型表")]
    public class DictionaryType : RootEntityTkey<long>, IDeleteFilter
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(ColumnDescription = "项目名称", Length = 50)]
        public string? Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [SugarColumn(ColumnDescription = "编码", Length = 50, IsNullable = true)]
        public string? Code { get; set; }
        /// <summary>
        /// 创建Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建Id")]
        public long? CreateId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(Length = 50, ColumnDescription = "创建者")]
        public string? CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新者id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新者id")]
        public long? ModifyId { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新者")]
        public string? ModifyBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新时间")]
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(ColumnDescription = "获取或设置是否禁用，逻辑上的删除，非物理删除")]
        public bool IsDeleted { get; set; } = false;
    }
}
