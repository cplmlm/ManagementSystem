

using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 学生管理接口
    /// </summary>	
    public interface IOrganizationServices : IBaseServices<Organization>
    {

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        Task<MessageModel<string>> AddOrganization(OrganizationDto organization);
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteOrganization(long id);
        /// <summary>
        /// 获取机构列表
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="key">查询</param>
        /// <returns></returns>
        Task<MessageModel<PageModel<OrganizationDto>>> GetOrganizations(int page, string key);
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="organization">机构信息</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateOrganization(OrganizationDto organization);
        /// <summary>
        /// 查询树形机构Table
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="number">序号</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        Task<List<Organization>> GetTreeTable(long parentId = 0, int number = 0, string key = "");
        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        Task<List<Organization>> GetAllOrganizations();
    }
}
