

using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// sysUserInfoServices
    /// </summary>	
    public interface ISysUserInfoServices : IBaseServices<SysUserInfo>
    {
        Task<SysUserInfo> SaveUserInfo(string loginName, string loginPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
        Task<MessageModel<TokenInfoViewModel>> Login(string name, string password);
        Task<string> AddUser(SysUserInfoDto sysUserInfo);
        Task<MessageModel<string>> DeleteUser(long id);
        Task<MessageModel<PageModel<SysUserInfoDto>>> GetUsers(int page, string key,string orgrizaitionId);
        Task<MessageModel<string>> UpdateUser(SysUserInfoDto sysUserInfo);
        Task<string> UpdatePassword(SysUserInfoDto sysUserInfo);
        /// <summary>
        /// ÷ÿ÷√”√ªß√‹¬Î
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        Task<string> ResetUserPassword(SysUserInfoDto sysUserInfo);
    }
}
