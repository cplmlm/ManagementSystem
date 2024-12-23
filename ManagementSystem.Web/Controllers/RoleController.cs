using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;

namespace ManagementSystem.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class RoleController : BaseApiController
    {
        readonly IRoleServices _roleServices;
        readonly IUser _user;


        public RoleController(IRoleServices roleServices, IUser user)
        {
            _roleServices = roleServices;
            _user = user;
        }

        /// <summary>
        /// 获取全部角色-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Role>>> GetRoles(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 15;
            var data = await _roleServices.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Role>>> GetAllRoles()
        {
            var data = await _roleServices.Query(a => a.IsDeleted != true);
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddRole([FromBody] Role role)
        {
            role.CreateId = _user.Id;
            role.CreateBy = _user.Name;
            var id = (await _roleServices.Add(role));
            return id > 0 ? Success(id.ObjToString(), "添加成功") : Failed("添加失败");

        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateRole([FromBody] Role role)
        {
            if (role == null || role.Id <= 0)
            {
                return Failed("缺少参数");
            }
            return await _roleServices.Update(role) ? Success(role?.Id.ObjToString(), "更新成功") : Failed("更新失败");
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteRole(long id)
        {
            if (id <= 0) return Failed();
            var userDetail = await _roleServices.QueryById(id);
            if (userDetail == null) return Success<string>(null, "角色不存在");
            userDetail.IsDeleted = true;
            return await _roleServices.Update(userDetail) ? Success(userDetail?.Id.ObjToString(), "删除成功") : Failed();
        }
    }
}
