using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;


namespace ManagementSystem.Controllers
{
    /// <summary>
    /// 业务图片
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class BusinessImageController : BaseApiController
    {
      private  readonly IBusinessImageServices _imageServices;

        public BusinessImageController(IBusinessImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        /// <summary>
        /// 添加业务图片
        /// </summary>
        /// <param name="image">业务图片</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddBusinessImage(BusinessImage image)
        {
            var id = (await _imageServices.AddBusinessImage(image));
            return id > 0 ? Success(id.ObjToString(), "添加成功") : Failed("添加失败");
        }
        /// <summary>
        /// 更新业务图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateBusinessImage(BusinessImage image)
        {
            if (image == null || image.Id <= 0)
            {
                return Failed("缺少参数");
            }
            string result = await _imageServices.UpdateBusinessImage(image);
            return string.IsNullOrEmpty(result) ? Success("更新成功") : Failed(result);
        }
    }
}
