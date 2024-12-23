using ManagementSystem.IServices;
using ManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;
using ManagementSystem.Common.Helper;
using ManagementSystem.Model.Models;

namespace ManagementSystem.Controllers;

/// <summary>
/// 系统授权
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthorizationController : BaseApiController
{
    readonly IAuthorizationServices _authorizationServices;
    public AuthorizationController(IAuthorizationServices authorizationServices)
    {
        _authorizationServices = authorizationServices;
    }


    /// <summary>
    /// 生成机器码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public MessageModel<string> GenerateMachineCode()
    {
        string machineCode = RegInfoHelper.GetMachineCode();
        return Success(machineCode, "获取成功");
    }

    /// <summary>
    /// 根据机器码生成注册码
    /// </summary>
    /// <param name="machineCode">机器码</param>
    /// <param name="expiredTime">到期时间</param>
    /// <returns></returns>
    [HttpGet]
    public MessageModel<string> GenerateRegisterCode(string machineCode, DateTime expiredTime)
    {
        string registerCode = RegInfoHelper.CreateRegisterCode(machineCode, expiredTime);
        return Success(registerCode, "获取成功"); ;
    }

    /// <summary>
    /// 系统注册
    /// </summary>
    /// <param name="systemAuthorization">注册码</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<MessageModel<string>> SystemRegister(SystemAuthorization systemAuthorization)
    {
        var result = await _authorizationServices.SystemRegister(systemAuthorization.RegisterCode);
        return result ? Success("注册成功") : Failed("注册失败");
    }

    /// <summary>
    /// 获取系统注册信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<MessageModel<SystemAuthorization>> GetSystemRegisterInfo()
    {
        var systemAuthorizations = await _authorizationServices.Query();
        if(systemAuthorizations == null || systemAuthorizations.Count == 0)
        {

            return Success(systemAuthorizations.FirstOrDefault(), "获取成功");
        }
        else
        {
            var systemAuthorization = systemAuthorizations.FirstOrDefault();
            DateTime expiredTime = DateTime.Now;
            RegInfoHelper.GetExpiredTimeByRegisterCode(systemAuthorization?.RegisterCode, ref expiredTime);
            systemAuthorization.ExpiredTime = expiredTime;
            return Success(systemAuthorization, "获取成功");
        }
    }
}
