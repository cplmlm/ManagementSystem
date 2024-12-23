using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;
using ManagementSystem.Services;
using ManagementSystem.Extensions;

namespace ManagementSystem.Controllers
{
    /// <summary>
    /// 行政区划
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class AreaController : BaseApiController
    {
        readonly IAreaServices _areaServices;
        readonly IUser _user;
        private readonly IRedisBasketRepository _redisBasketRepository;

        public AreaController(IAreaServices areaServices, IUser user, IRedisBasketRepository redisBasketRepository)
        {
            _areaServices = areaServices;
            _user = user;
            _redisBasketRepository = redisBasketRepository;
        }

        /// <summary>
        /// 获取全部行政区划-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Area>>> GetAreas(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 10;
            var data = await _areaServices.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 获取行政区划树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<AreaTree>> GetAreaTree()
        {
            AreaTree areaTree = new AreaTree();
            var result = await _redisBasketRepository.Get<object>("Area");
            if (result != null)
            {
                areaTree = await _redisBasketRepository.Get<AreaTree>("Area");
            }
            else
            {
                areaTree = await _areaServices.GetAreaTree();
                await _redisBasketRepository.Set("Area", areaTree, TimeSpan.MaxValue);//缓存一直有效  
            }
            return Success(areaTree, "获取成功");
        }
        /// <summary>
        /// 添加行政区划
        /// </summary>
        /// <param name="area">行政区划</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddArea([FromBody] Area area)
        {
            area.CreateId = _user.Id;
            area.CreateBy = _user.Name;
            var id = await _areaServices.Add(area,true);
            return id > 0 ? Success(id.ObjToString(), "添加成功") : Failed("添加失败");
        }
        /// <summary>
        /// 更新行政区划
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateArea([FromBody] Area area)
        {
            if (area == null || area.Id <= 0)
            {
                return Failed("缺少参数");
            }
          //  string where = " id=" + area.OldId;
            return await _areaServices.Update(area) ? Success("更新成功") : Failed("更新失败");
        }
        /// <summary>
        /// 删除行政区划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteArea(long id)
        {
            if (id <= 0)
            {
                return Failed("缺少参数");
            }
            var userDetail = await _areaServices.QueryById(id);
            if (userDetail == null)
            {
                return Failed("行政区划不存在");
            }
            userDetail.IsDeleted = true;
            bool result = await _areaServices.Update(userDetail);
            return result ? Success(userDetail?.Id.ObjToString(), "删除成功") : Failed("删除失败");
        }
    }
}
