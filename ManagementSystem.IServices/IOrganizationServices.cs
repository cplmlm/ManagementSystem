

using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// ѧ������ӿ�
    /// </summary>	
    public interface IOrganizationServices : IBaseServices<Organization>
    {

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        Task<MessageModel<string>> AddOrganization(OrganizationDto organization);
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteOrganization(long id);
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="key">��ѯ</param>
        /// <returns></returns>
        Task<MessageModel<PageModel<OrganizationDto>>> GetOrganizations(int page, string key);
        /// <summary>
        /// ���»���
        /// </summary>
        /// <param name="organization">������Ϣ</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateOrganization(OrganizationDto organization);
        /// <summary>
        /// ��ѯ���λ���Table
        /// </summary>
        /// <param name="parentId">���ڵ�</param>
        /// <param name="number">���</param>
        /// <param name="key">�ؼ���</param>
        /// <returns></returns>
        Task<List<Organization>> GetTreeTable(long parentId = 0, int number = 0, string key = "");
        /// <summary>
        /// ��ȡ���л���
        /// </summary>
        /// <returns></returns>
        Task<List<Organization>> GetAllOrganizations();
    }
}
