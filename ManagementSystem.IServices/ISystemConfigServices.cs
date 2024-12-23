using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// ϵͳ���ýӿ�
    /// </summary>	
    public interface ISystemConfigServices : IBaseServices<SystemConfig>
    {
        /// <summary>
        /// ����ϵͳ����
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        Task<long> AddSystemConfig(SystemConfig systemConfig);
        /// <summary>
        /// ����ϵͳ����
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        Task<string> UpdateSystemConfig(SystemConfig systemConfig);
        /// <summary>
        /// ɾ��ϵͳ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteSystemConfig(long id);
    }
}
