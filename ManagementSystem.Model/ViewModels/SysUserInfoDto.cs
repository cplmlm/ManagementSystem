using SqlSugar;
using System;
using System.Collections.Generic;

namespace ManagementSystem.Model.ViewModels
{
    public class SysUserInfoDto:RootEntityTkey<long>
    {
        public List<long> RIDs { get; set; }
        public long? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public string? LoginName { get; set; }
        public string? LoginPWD { get; set; }
        public string? OldLoginPWD { get; set; }
        public string? RealName { get; set; }
        public int Status { get; set; }
        public long DepartmentId { get; set; }
        public string? Remark { get; set; }
        public System.DateTime CreateTime { get; set; } = DateTime.Now;
        public System.DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 关键业务修改时间
        /// </summary>
        public DateTime CriticalModifyTime { get; set; } = DateTime.Now;
        public DateTime LastErrTime { get; set; } = DateTime.Now;
        public int ErrorCount { get; set; }
        public string? Name { get; set; }
        public bool Enable { get; set; } = true;
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 医生签名
        /// </summary>
        public string? Signature { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }
        public List<string?> RoleNames { get; set; }
        public List<long> Dids { get; set; }
        public string? DepartmentName { get; set; }
    }
}
