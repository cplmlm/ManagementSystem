using ManagementSystem.IServices.Base;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// ҵ��ͼƬ�ӿ�
    /// </summary>	
    public interface IBusinessImageServices : IBaseServices<BusinessImage>
    {
        /// <summary>
        /// ����ҵ��ͼƬ
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<long> AddBusinessImage(BusinessImage image);
        /// <summary>
        /// ����ҵ��ͼƬ
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<string> UpdateBusinessImage(BusinessImage image);
    }
}
