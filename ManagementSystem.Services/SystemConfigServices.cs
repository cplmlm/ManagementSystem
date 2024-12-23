using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using ManagementSystem.Model;
using AutoMapper;
using ManagementSystem.IServices;
using ManagementSystem.Repository.Base;
using ManagementSystem.Services.Base;
using Microsoft.Extensions.Logging;
using ManagementSystem.Repository.UnitOfWorks;
using ManagementSystem.Common.Helper;

namespace ManagementSystem.Services
{
    public class SystemConfigServices: BaseServices<SystemConfig>, ISystemConfigServices
    {
        private readonly IBaseRepository<SystemConfig> _systemConfigRepository;
        private readonly IUser _user;

        public SystemConfigServices(IBaseRepository<SystemConfig> systemConfigRepository,IUser user)
        {
            _systemConfigRepository = systemConfigRepository;
            _user = user;
        }

        /// <summary>
        /// 添加系统参数
        /// </summary>
        /// <param name="systemConfig">系统参数基本信息</param>
        /// <returns></returns>
        public async Task<long> AddSystemConfig(SystemConfig systemConfig)
        {
            systemConfig.CreateId = _user.Id;
            var id = await _systemConfigRepository.Add(systemConfig);
            return id;
        }
        /// <summary>
        /// 更新系统参数
        /// </summary>
        /// <param name="systemConfig">系统参数基本信息</param>
        /// <returns></returns>
        public async Task<string> UpdateSystemConfig(SystemConfig systemConfig)
        {
            var oldSystemConfig = await _systemConfigRepository.QueryById(systemConfig.Id);
            if (oldSystemConfig is not { Id: > 0 })
            {
                return "系统参数不存在或已被删除";
            }
            systemConfig.ModifyId = _user.Id;
            systemConfig.ModifyTime = DateTime.Now;
            systemConfig.CreateId = oldSystemConfig.CreateId;
            systemConfig.CreateTime = oldSystemConfig.CreateTime;
            systemConfig.ParentId = oldSystemConfig.ParentId;
            var result = await _systemConfigRepository.Update(systemConfig);
            return result ? "" : "更新失败";
        }
        /// <summary>
        /// 删除系统参数
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<string> DeleteSystemConfig(long id)
        {
            if (id == 0)
            {
                return "删除失败,请传入正确的Id";
            }
            var systemConfig = await _systemConfigRepository.QueryById(id);
            if(systemConfig is null)
            {
                return "系统参数不存在或已被删除";
            }
            systemConfig.IsDeleted = true;
            systemConfig.ModifyId = _user.Id;
            systemConfig.ModifyTime = DateTime.Now;
            bool success = await _systemConfigRepository.Update(systemConfig);
            return success ? "" : "删除失败";
        }
    }
}
