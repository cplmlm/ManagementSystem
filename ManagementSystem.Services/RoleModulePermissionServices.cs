using ManagementSystem.IServices;
using ManagementSystem.Model.Models;
using ManagementSystem.Services.Base;
using ManagementSystem.Repository;
using ManagementSystem.IRepository;
using ManagementSystem.Repository.Base;
using ManagementSystem.Common;
using Microsoft.Extensions.DependencyInjection;
using ManagementSystem.Common.LifetimeInterfaces;
using ManagementSystem.Model.ViewModels;


namespace ManagementSystem.Services
{
    [ServiceAttribute(ServiceLifetime.Transient)]
    /// <summary>
    /// RoleModulePermissionServices 应用服务
    /// </summary>	
    public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices,IScopedInterface
    {
        readonly IRoleModulePermissionRepository _dal;
        readonly IBaseRepository<Modules> _moduleRepository;
        readonly IBaseRepository<Role> _roleRepository;

        // 将多个仓储接口注入 
        public RoleModulePermissionServices(
            IRoleModulePermissionRepository dal,
            IBaseRepository<Modules> moduleRepository,
            IBaseRepository<Role> roleRepository)
        {
            this._dal = dal;
            this._moduleRepository = moduleRepository;
            this._roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await base.Query(a => a.IsDeleted == false);
            var roles = await _roleRepository.Query(a => a.IsDeleted == false);
            var modules = await _moduleRepository.Query(a => a.IsDeleted == false);

            //var roleModulePermissionsAsync = base.Query(a => a.IsDeleted == false);
            //var rolesAsync = _roleRepository.Query(a => a.IsDeleted == false);
            //var modulesAsync = _moduleRepository.Query(a => a.IsDeleted == false);

            //var roleModulePermissions = await roleModulePermissionsAsync;
            //var roles = await rolesAsync;
            //var modules = await modulesAsync;


            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId);
                }

            }
            return roleModulePermissions;
        }



        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await _dal.RoleModuleMaps();
        }

        public async Task<List<PermissionItemDto>> LoginRoleModuleMaps()
        {
            return await _dal.LoginRoleModuleMaps();
        }

        public async Task<List<RoleModulePermission>> GetRMPMaps()
        {
            return await _dal.GetRMPMaps();
        }

        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        public async Task UpdateModuleId(long permissionId, long moduleId)
        {
            await _dal.UpdateModuleId(permissionId, moduleId);
        }

    }
}
