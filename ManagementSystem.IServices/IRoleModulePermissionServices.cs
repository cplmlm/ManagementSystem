using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace ManagementSystem.IServices
{
    public interface IRoleModulePermissionServices :IBaseServices<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> GetRoleModule();
       // Task<List<TestMuchTableResult>> QueryMuchTable();
        Task<List<RoleModulePermission>> RoleModuleMaps();

        Task<List<PermissionItemDto>> LoginRoleModuleMaps();
        Task<List<RoleModulePermission>> GetRMPMaps();
        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        Task UpdateModuleId(long permissionId, long moduleId);
    }
}
