using ManagementSystem.Repository.Base;
using ManagementSystem.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem.Common.HttpContextUser;
using SqlSugar;
using System.Linq.Expressions;
using ManagementSystem.Model.ViewModels;


namespace ManagementSystem.IRepository
{
    /// <summary>
    /// IRoleModulePermissionRepository
    /// </summary>	
    public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>//类名
    {
        Task<List<RoleModulePermission>> RoleModuleMaps();
        Task<List<RoleModulePermission>> GetRMPMaps();
        Task<List<PermissionItemDto>> LoginRoleModuleMaps();
        
        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        Task UpdateModuleId(long permissionId, long moduleId);
    }
}
