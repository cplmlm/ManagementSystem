using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;

namespace ManagementSystem.Model.Models
{
    /// <summary>
    /// 用户跟角色关联表
    /// </summary>
    public class UserRole : UserRoleRoot<long>, IDeleteFilter
    {
        public UserRole() { }

        public UserRole(long uid, long rid)
        {
            UserId = uid;
            RoleId = rid;
            CreateTime = DateTime.Now;
            IsDeleted = false;
            CreateId = uid;
            CreateTime = DateTime.Now;
        }


        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsDeleted { get; set; }
       
        /// <summary>
        /// 创建Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long? CreateId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; }

    }
}
