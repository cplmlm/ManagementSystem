using ManagementSystem.Model.Models.RootTkey.Interface;

namespace ManagementSystem.Model.Models;

/// <summary>
/// 系统授权信息表
/// </summary>
[SqlSugar.SugarTable("SystemAuthorization", "系统授权信息表")]
public class SystemAuthorization : RootEntityTkey<long>
{

    /// <summary>
    ///机器码
    /// </summary>
    [SqlSugar.SugarColumn(Length = 100, ColumnDescription = "机器码")]
    public string? MachineCode { get; set; }
    /// <summary>
    /// // 注册码
    /// </summary>
    [SqlSugar.SugarColumn(Length = 500, ColumnDescription = "注册码")]
    public string? RegisterCode { get; set; }
    /// <summary>
    /// 授权时间
    /// </summary>
    [SqlSugar.SugarColumn(ColumnDescription = "授权日期")]
    public DateTime? RegisterTime { get; set; }
    /// <summary>
    /// 授权到期时间
    /// </summary>
    [SqlSugar.SugarColumn(ColumnDescription = "授权到期日期")]
    public DateTime? ExpiredTime { get; set; }
    /// <summary>
    /// 授权是否有效
    /// </summary>
    [SqlSugar.SugarColumn(ColumnDescription = "授权是否有效")]
    public bool IsActive { get; set; } = true;
}
