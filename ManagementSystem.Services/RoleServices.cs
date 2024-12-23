using ManagementSystem.IServices;
using ManagementSystem.Model.Models;
using ManagementSystem.Services.Base;

namespace ManagementSystem.Services
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class RoleServices : BaseServices<Role>, IRoleServices
    {
       /// <summary>
       /// �����ɫ
       /// </summary>
       /// <param name="roleName"></param>
       /// <returns></returns>
        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await base.Query(a => a.Name == role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(role);
                model = await base.QueryById(id);
            }

            return model;

        }

        /// <summary>
        /// ͨ����ɫId��ȡ��ɫ����
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<string> GetRoleNameByRid(int rid)
        {
            return ((await base.QueryById(rid))?.Name);
        }
 
    }
}
