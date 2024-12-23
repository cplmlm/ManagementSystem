using ManagementSystem.Model.Models;
using ManagementSystem.Services.Base;
using ManagementSystem.IServices;
using ManagementSystem.Common.Helper;
using ManagementSystem.Extensions;
using ManagementSystem.Repository.Base;

namespace ManagementSystem.Services;

/// <summary>
/// 系统授权服务层
/// </summary>	
public class AuthorizationServices : BaseServices<SystemAuthorization>,  IAuthorizationServices
{
    private readonly IBaseRepository<SystemAuthorization> _licenseRepository;

    public AuthorizationServices(IBaseRepository<SystemAuthorization> licenseRepository)
    {
        _licenseRepository = licenseRepository;
    }

    /// <summary>
    /// 系统注册
    /// </summary>
    /// <param name="registerCode">注册码</param>
    /// <returns></returns>
    public async Task<bool> SystemRegister(string registerCode)
    {
        DateTime expiredTime = DateTime.Now;
        DateTime registerTime = DateTime.Now;
        string machineCode = string.Empty;
        bool result = false;
        var checkRes = RegInfoHelper.CheckRegister(registerCode, ref expiredTime, ref registerTime, ref machineCode);
        if (checkRes)
        {
            SystemAuthorization systemAuthorization = new SystemAuthorization();
            systemAuthorization.MachineCode = machineCode;
            systemAuthorization.RegisterCode = registerCode;
            systemAuthorization.RegisterTime = registerTime;
            //systemAuthorization.ExpiredTime = expiredTime;
            var model = await _licenseRepository.Query(x => x.MachineCode == machineCode);
            if (model.Count() > 0)
            {
                systemAuthorization.Id = model.FirstOrDefault().Id;
                result = await _licenseRepository.Update(systemAuthorization);
            }
            else
            {
                long id = await _licenseRepository.Add(systemAuthorization);
                result = id > 0;
            }
        }
        return result;
    }
}
