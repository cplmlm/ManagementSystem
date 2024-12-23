using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices;

/// <summary>
/// 授权服务接口
/// </summary>
public interface IAuthorizationServices : IBaseServices<SystemAuthorization>
{
    /// <summary>
    /// 系统注册
    /// </summary>
    /// <param name="registerCode">注册码</param>
    /// <returns></returns>
    Task<bool> SystemRegister(string registerCode);
}
