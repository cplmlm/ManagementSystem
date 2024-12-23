using ManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ManagementSystem.Web.Controllers
{
    /// <summary>
    /// 基础API控制器
    /// </summary>
    public class BaseApiController : Controller
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<T> Success<T>(T data, string Message = "成功")
        {
            return new MessageModel<T>()
            {
                Message = Message,
                Data = data,
            };
        }
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<string> Success(string Message = "成功")
        {
            return new MessageModel<string>()
            {
                Message = Message,
                Data = null,
            };
        }
        /// <summary>
        /// 失败返回
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<string> Failed(string Message = "失败", ResultStatus status = ResultStatus.Fail)
        {
            return new MessageModel<string>()
            {
                Status = status,
                Message = Message,
                Data = null,
            };
        }

        /// <summary>
        /// 失败返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<T> Failed<T>(string Message = "失败", ResultStatus status = ResultStatus.Fail)
        {
            return new MessageModel<T>()
            {
                Status = status,
                Message = Message,
                Data = default,
            };
        }
        /// <summary>
        /// 分页返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="dataCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="data"></param>
        /// <param name="pageCount"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(int page, int dataCount, int pageSize, List<T> data,
            int pageCount, string Message = "获取成功")
        {
            return new MessageModel<PageModel<T>>()
            {
                Message = Message,
                Data = new PageModel<T>(page, dataCount, pageSize, data)
            };
        }
        /// <summary>
        /// 分页返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageModel"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(PageModel<T> pageModel, string Message = "获取成功")
        {
            return new MessageModel<PageModel<T>>()
            {
                Message = Message,
                Data = pageModel
            };
        }
    }
}