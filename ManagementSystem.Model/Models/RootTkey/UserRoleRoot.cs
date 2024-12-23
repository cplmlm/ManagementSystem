using System;

namespace ManagementSystem.Model
{
    /// <summary>
    /// 用户跟角色关联表
    /// 父类
    /// </summary>
    public class UserRoleRoot<Tkey> : RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Tkey UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Tkey RoleId { get; set; }

    }
}
