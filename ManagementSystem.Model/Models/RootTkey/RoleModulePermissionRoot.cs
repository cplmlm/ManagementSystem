using SqlSugar;
using System;

namespace ManagementSystem.Model
{
    /// <summary>
    /// 按钮跟权限关联表
    /// 父类
    /// </summary>
    public class RoleModulePermissionRoot<Tkey> : RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
       
        /// <summary>
        /// 角色Id
        /// </summary>
        public Tkey RoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Tkey ModuleId { get; set; }
        /// <summary>
        /// api Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Tkey PermissionId { get; set; }
       
    }
}
