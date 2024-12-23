using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 行政地区表
    /// </summary>
    [SugarTable("Area", "行政地区表")]
    public class Area :  IDeleteFilter
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 地区父级id
        /// </summary>
        [SugarColumn(Length = 50, ColumnDescription = "地区父级id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        [SugarColumn(Length = 50, ColumnDescription = "地区名称")]
        public string? Name { get; set; }
        /// <summary>
        ///地区全称
        /// </summary>
        [SugarColumn(Length = 200, ColumnDescription = "地区全称")]
        public string? FullName { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        [SugarColumn(Length = 100, ColumnDescription = "经纬度")]
        public string? Geo { get; set; }
        /// <summary>
        /// 行政区划级别： 0-省1-市2-县 3-乡 4-村
        /// </summary>
        [SugarColumn(ColumnDescription = "行政区划级别")]
        public int Deep { get; set; }
        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(ColumnDescription = "是否禁用")]
        public bool IsDeleted { get; set; } = false;
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
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改前的Id
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public long? OldId { get; set; }

    }
}
