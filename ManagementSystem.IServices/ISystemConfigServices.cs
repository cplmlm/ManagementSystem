using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 系统配置接口
    /// </summary>	
    public interface ISystemConfigServices : IBaseServices<SystemConfig>
    {
        /// <summary>
        /// 新增系统配置
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        Task<long> AddSystemConfig(SystemConfig systemConfig);
        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        Task<string> UpdateSystemConfig(SystemConfig systemConfig);
        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteSystemConfig(long id);
    }
}
