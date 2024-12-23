using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;


namespace ManagementSystem.Controllers
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class SystemConfigController : BaseApiController
    {
        readonly ISystemConfigServices _systemConfigServices;

        public SystemConfigController(ISystemConfigServices systemConfigServices)
        {
            _systemConfigServices = systemConfigServices;
        }

        /// <summary>
        /// 获取全部系统配置
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <param name="key">参数名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SystemConfig>>> GetSystemConfigs(string parentId = "", string key = "")
        {
            var data = await _systemConfigServices.Query(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)) && (a.ParentId == parentId.ObjToLong() || string.IsNullOrEmpty(parentId)));
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 添加系统配置
        /// </summary>
        /// <param name="systemConfig">系统配置</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddSystemConfig([FromBody] SystemConfig systemConfig)
        {
            var id = (await _systemConfigServices.AddSystemConfig(systemConfig));
            return id > 0 ? Success(id.ObjToString(), "添加成功") : Failed("添加失败");
        }
        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateSystemConfig([FromBody] SystemConfig systemConfig)
        {
            if (systemConfig == null || systemConfig.Id <= 0)
            {
                return Failed("缺少参数");
            }
            string result = await _systemConfigServices.UpdateSystemConfig(systemConfig);
            return string.IsNullOrEmpty(result) ? Success("更新成功") : Failed(result);
        }
        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteSystemConfig(long id)
        {
            string result = await _systemConfigServices.DeleteSystemConfig(id);
            return string.IsNullOrEmpty(result) ? Success("删除成功") : Failed(result);
        }
    }
}
