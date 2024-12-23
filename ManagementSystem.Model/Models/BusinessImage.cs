using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 业务图片
    /// </summary>
    [SugarTable("BusinessImage", "业务图片")]
    public class BusinessImage : RootEntityTkey<long>
    {
        /// <summary>
        /// 对应业务ID
        /// </summary>
        [SugarColumn(ColumnDescription = "对应业务ID")]
        public long? RelatedBusinessId { get; set; }
        /// <summary>
        /// base64字符串
        /// </summary>
        [SugarColumn(ColumnDescription = "base64字符串", Length = 8000)]
        public string? Base64String { get; set; }
        /// <summary>
        /// 创建Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建Id")]
        public long? CreateId { get; set; }
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
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "更新时间")]
        public DateTime? ModifyTime { get; set; }
    }
}
