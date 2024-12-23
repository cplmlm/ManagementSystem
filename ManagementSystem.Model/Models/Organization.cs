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
    /// 机构
    /// </summary>
    public class Organization : RootEntityTkey<long>, IDeleteFilter
    {
        /// <summary>
        /// 父组织Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 组织代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组织简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 组织等级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 拼音编码
        /// </summary>
        public string PyCode { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public int Property { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 许可证编号
        /// </summary>
        public string LicenceCode { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string CertificateCode { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public double Status { get; set; }

        /// <summary>
        /// 引用Id
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string HealthcareCode { get; set; }

        /// <summary>
        /// 单位负责人员工Id
        /// </summary>
        public string HeadOfUnitStaffId { get; set; }

        /// <summary>
        /// 统计员员工Id
        /// </summary>
        public string StatisticianStaffId { get; set; }

        /// <summary>
        /// 编码员员工Id
        /// </summary>
        public string CoderStaffId { get; set; }

        /// <summary>
        /// 组织联系电话号码
        /// </summary>
        public string OrgContactPhone { get; set; }

        /// <summary>
        /// 医院等级
        /// </summary>
        public int HospitalLevel { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        ///key
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int? Key { get; set; } = 0;
        /// <summary>
        /// 父级key
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int? ParentKey { get; set; } = 0;
        /// <summary>
        /// 是否有子节点
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool HasChildren { get; set; } = true;
    }
}
