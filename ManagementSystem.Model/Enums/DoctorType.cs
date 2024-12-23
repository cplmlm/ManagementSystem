using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Model.Enums
{
    public enum DoctorType
    {
        /// <summary>
        /// 既往病史
        /// </summary>
        [Description("既往病史")]
        PastMedicalHistory,
        /// <summary>
        /// 一般情况
        /// </summary>
        [Description("一般情况")]
        GeneralCondition,
        /// <summary>
        /// 内科
        /// </summary>
        [Description("内科")]
        InternalMedicine,
        /// <summary>
        /// 外科
        /// </summary>
        [Description("外科")]
        Surgery,
        /// <summary>
        /// 五官科
        /// </summary>
        [Description("五官科")]
        Otolaryngology,
        /// <summary>
        /// 口腔科
        /// </summary>
        [Description("口腔科")]
        Dentistry,
        /// <summary>
        /// 体检结论
        /// </summary>
        [Description("体检结论")]
        Conclusion,
    }
}
