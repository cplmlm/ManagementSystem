using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 业务图片接口
    /// </summary>	
    public interface IBusinessImageServices : IBaseServices<BusinessImage>
    {
        /// <summary>
        /// 新增业务图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<long> AddBusinessImage(BusinessImage image);
        /// <summary>
        /// 更新业务图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<string> UpdateBusinessImage(BusinessImage image);
    }
}
