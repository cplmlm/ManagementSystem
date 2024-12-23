using ManagementSystem.IRepository;
using ManagementSystem.Model.Models;
using ManagementSystem.Repository.Base;
using SqlSugar;
using ManagementSystem.Repository.UnitOfWorks;
using ManagementSystem.Model.ViewModels;


namespace ManagementSystem.Repository
{
    public class RoleModulePermissionRepository : BaseRepository<RoleModulePermission>, IRoleModulePermissionRepository
    {

        public RoleModulePermissionRepository(IUnitOfWorkManage unitOfWorkManage) : base(unitOfWorkManage)
        {

        }

        /// <summary>
        /// 角色权限Map
        /// RoleModulePermission, Module, Role 三表联合
        /// 第四个类型 RoleModulePermission 是返回值
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.RoleId == r.Id
                },

                (rmp, m, r) => new RoleModulePermission()
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },
                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );

            //string sqlStr= "SELECT  \"r\".\"isdeleted\" AS \"r.role.isdeleted\" , \"r\".\"name\" AS \"r.role.name\" , \"r\".\"description\" AS \"r.role.description\" , \"r\".\"ordersort\" AS \"r.role.ordersort\" , \"r\".\"dids\" AS \"r.role.dids\" , \"r\".\"authorityscope\" AS \"r.role.authorityscope\" , \"r\".\"enabled\" AS \"r.role.enabled\" , \"r\".\"createid\" AS \"r.role.createid\" , \"r\".\"createby\" AS \"r.role.createby\" , \"r\".\"createtime\" AS \"r.role.createtime\" , \"r\".\"modifyid\" AS \"r.role.modifyid\" , \"r\".\"modifyby\" AS \"r.role.modifyby\" , \"r\".\"modifytime\" AS \"r.role.modifytime\" , \"r\".\"id\" AS \"r.role.id\" , \"m\".\"isdeleted\" AS \"m.modules.isdeleted\" , \"m\".\"name\" AS \"m.modules.name\" , \"m\".\"linkurl\" AS \"m.modules.linkurl\" , \"m\".\"area\" AS \"m.modules.area\" , \"m\".\"controller\" AS \"m.modules.controller\" , \"m\".\"action\" AS \"m.modules.action\" , \"m\".\"icon\" AS \"m.modules.icon\" , \"m\".\"code\" AS \"m.modules.code\" , \"m\".\"ordersort\" AS \"m.modules.ordersort\" , \"m\".\"description\" AS \"m.modules.description\" , \"m\".\"ismenu\" AS \"m.modules.ismenu\" , \"m\".\"enabled\" AS \"m.modules.enabled\" , \"m\".\"createid\" AS \"m.modules.createid\" , \"m\".\"createby\" AS \"m.modules.createby\" , \"m\".\"createtime\" AS \"m.modules.createtime\" , \"m\".\"modifyid\" AS \"m.modules.modifyid\" , \"m\".\"modifyby\" AS \"m.modules.modifyby\" , \"m\".\"modifytime\" AS \"m.modules.modifytime\" , \"m\".\"parentid\" AS \"m.modules.parentid\" , \"m\".\"id\" AS \"m.modules.id\" , \"rmp\".\"isdeleted\" AS \"isdeleted\"  FROM \"rolemodulepermission\" \"rmp\" Left JOIN \"modules\" \"m\" ON ( \"rmp\".\"moduleid\" = \"m\".\"id\" ) AND ( \"m\".\"isdeleted\" = false )  Left JOIN \"role\" \"r\" ON ( \"rmp\".\"roleid\" = \"r\".\"id\" ) AND ( \"r\".\"isdeleted\" = false )   WHERE ((( \"rmp\".\"isdeleted\" = false ) AND ( \"m\".\"isdeleted\" = false )) AND ( \"r\".\"isdeleted\" = false ))  AND ( \"rmp\".\"isdeleted\" = false ) ";
            //var data = await QuerySql(sqlStr);
            //return data;
        }
        /// <summary>
        /// 角色权限Map（登录临时解决办法）
        /// RoleModulePermission, Module, Role 三表联合
        /// 第四个类型 RoleModulePermission 是返回值
        /// </summary>
        /// <returns></returns>
        public async Task<List<PermissionItemDto>> LoginRoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, PermissionItemDto>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.RoleId == r.Id
                },

                (rmp, m, r) => new PermissionItemDto()
                {
                    Role = r.Name,
                    Url = m.LinkUrl,
                },
                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );
        }


        /// <summary>
        /// 查询出角色-菜单-接口关系表全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRMPMaps()
        {
            return await Db.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.ModuleId)
                .Mapper(rmp => rmp.Permission, rmp => rmp.PermissionId)
                .Mapper(rmp => rmp.Role, rmp => rmp.RoleId)
                .Where(d => d.IsDeleted == false)
                .ToListAsync();
        }


        /// <summary>
        /// 查询出角色-菜单-接口关系表全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRMPMapsPage()
        {
            return await Db.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.ModuleId)
                .Mapper(rmp => rmp.Permission, rmp => rmp.PermissionId)
                .Mapper(rmp => rmp.Role, rmp => rmp.RoleId)
                .ToPageListAsync(1, 5, 10);
        }

        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        public async Task UpdateModuleId(long permissionId, long moduleId)
        {
            await Db.Updateable<RoleModulePermission>(it => it.ModuleId == moduleId).Where(
                it => it.PermissionId == permissionId).ExecuteCommandAsync();
        }
    }

}