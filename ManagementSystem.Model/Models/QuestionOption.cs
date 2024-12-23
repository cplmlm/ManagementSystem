using ManagementSystem.Model.Enums;
using ManagementSystem.Model.Models.RootTkey;
using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;
using System.Runtime.Intrinsics.Arm;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 调查问卷问题选项表
    /// </summary>
    [SugarTable("QuestionOption", "调查问卷问题选项表")]
    public class QuestionOption : BaseEntity
    {
        //public QuestionOption(long questionId,byte? serialNumber, string? description)
        //{
        //    QuestionId = questionId;
        //    SerialNumber = serialNumber;
        //    Description = description;
        //}
        /// <summary>
        /// 调查问卷问题表id
        /// </summary>
        [SugarColumn(ColumnDescription = "调查问卷问题表id")]
        public long QuestionId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(ColumnDescription = "序号")]
        public byte? SerialNumber { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(ColumnDescription = "项目名称", Length = 100)]
        public string? Description { get; set; }
    }
}
