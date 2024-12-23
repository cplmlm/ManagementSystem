using ManagementSystem.Model.Models.RootTkey;
using SqlSugar;


namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 调查问卷问题表
    /// </summary>
    [SugarTable("Question", "调查问卷问题表")]
    public class Question : BaseEntity
    {
        /// <summary>
        /// 问卷类型id
        /// </summary>
        [SugarColumn(ColumnDescription = "问卷类型id")]
        public long QuestionnaireTypeId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(ColumnDescription = "序号")]
        public byte SerialNumber { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        [SugarColumn(ColumnDescription = "问题", Length = 200)]
        public string Description { get; set; }
        /// <summary>
        /// 选择类型
        /// </summary>
        [SugarColumn(ColumnDescription = "选择类型 1-单选 2-多选 3-填空 4-评分 5-文本")]
        public byte? SelectType { get; set; }
        /// <summary>
        /// 问卷选项
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<QuestionOption>? Options { get; set; }
    }
}
