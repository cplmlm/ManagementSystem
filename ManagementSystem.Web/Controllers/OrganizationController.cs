using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using ManagementSystem.Services;

namespace ManagementSystem.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class OrganizationController : BaseApiController
    {
        private readonly IOrganizationServices _organizationServices;

        public OrganizationController(IOrganizationServices organizationServices)
        {
            _organizationServices = organizationServices;
        }
        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddOrganization([FromBody] OrganizationDto organization)
        {
            var data = await _organizationServices.AddOrganization(organization);
            return data;
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateOrganization([FromBody] OrganizationDto organization)
        {
            var data = await _organizationServices.UpdateOrganization(organization);
            return data;
        }
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteOrganization(long id)
        {
            var data = await _organizationServices.DeleteOrganization(id);
            return data;
        }
        /// <summary>
        /// 获取全部机构-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageModel<OrganizationDto>>> GetOrganizations(int page = 1, string key = "")
        {
            var data = await _organizationServices.GetOrganizations(page, key);
            return data;
        }
        /// <summary>
        /// 获取全部机构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<Organization>>> GetAllOrganizations()
        {
            var data = await _organizationServices.GetAllOrganizations();
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 查询树形 Table 
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="number">序号</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Organization>>> GetTreeTable(long parentId = 0, int number = 0, string key = "")
        {
            List<Organization> organizations = await _organizationServices.GetTreeTable(parentId, number, key);
            return Success(organizations, "获取成功");
        }
    }
}
