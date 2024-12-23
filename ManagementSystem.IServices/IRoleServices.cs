using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IRoleServices :IBaseServices<Role>
	{
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
