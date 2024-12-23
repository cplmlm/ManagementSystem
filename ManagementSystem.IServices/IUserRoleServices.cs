using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{	
	/// <summary>
	/// UserRoleServices
	/// </summary>	
    public interface IUserRoleServices :IBaseServices<UserRole>
	{

        Task<UserRole> SaveUserRole(long uid, long rid);
        Task<int> GetRoleIdByUid(long uid);
    }
}

