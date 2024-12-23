using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;

namespace ManagementSystem.Model.Models.RootTkey;

[SugarIndex("index_{table}_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
public class BaseEntity : RootEntityTkey<long>, IDeleteFilter
{
    /// <summary>
    /// 中立字段，某些表可使用某些表不使用   <br/>
    /// 逻辑上的删除，非物理删除  <br/>
    /// 例如：单据删除并非直接删除
    /// </summary>
    [SugarColumn(ColumnDescription = "逻辑删除")]
    public bool IsDeleted { get; set; }=false;

    /// <summary>
    /// 创建Id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者Id")]
    public long? CreateId { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者")]
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 修改Id
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "修改Id")]
    public long? ModifyId { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "更新者")]
    public string? ModifyBy { get; set; }

    /// <summary>
    /// 修改日期
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "修改日期")]
    public DateTime? ModifyTime { get; set; } = DateTime.Now;
}