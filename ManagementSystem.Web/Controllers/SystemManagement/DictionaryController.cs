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
    public class DictionaryController : BaseApiController
    {
        private readonly IDictionaryServices _dictionaryServices;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictionaryServices"></param>
        public DictionaryController(IDictionaryServices dictionaryServices)
        {
            _dictionaryServices = dictionaryServices;
        }
        #region 字典类型
        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddDictionaryType([FromBody] DictionaryType dictionary)
        {
            var data = await _dictionaryServices.AddDictionaryType(dictionary);
            return data;
        }
        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateDictionaryType([FromBody] DictionaryType dictionary)
        {
            var data = await _dictionaryServices.UpdateDictionaryType(dictionary);
            return data;
        }
        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteDictionaryType(long id)
        {
            var data = await _dictionaryServices.DeleteDictionaryType(id);
            return data;
        }
        /// <summary>
        /// 获取全部字典类型
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<PageModel<DictionaryType>>> GetDictionaryTypes(int page = 1, string key = "")
        {
            var data = await _dictionaryServices.GetDictionaryTypes(page, key);
            return data;
        }
        /// <summary>
        /// 获取全部字典类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<DictionaryType>>> GetAllDictionaryTypes()
        {
            var data = await _dictionaryServices.GetAllDictionaryTypes();
            return Success(data, "获取成功");
        }
        #endregion
        #region 字典项目
        /// <summary>
        /// 添加字典项目
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddDictionaryItem([FromBody] DictionaryItem dictionary)
        {
            var data = await _dictionaryServices.AddDictionaryItem(dictionary);
            return data;
        }
        /// <summary>
        /// 更新字典项目
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateDictionaryItem([FromBody] DictionaryItem dictionary)
        {
            var data = await _dictionaryServices.UpdateDictionaryItem(dictionary);
            return data;
        }
        /// <summary>
        /// 删除字典项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteDictionaryItem(long id)
        {
            var data = await _dictionaryServices.DeleteDictionaryItem(id);
            return data;
        }
        /// <summary>
        /// 获取字典项目
        /// </summary>
        /// <param name="key">字典类型id或者code</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<DictionaryItem>>> GetDictionaryItems(string key = "")
        {
            var data = await _dictionaryServices.GetDictionaryItems(key);
            return data;
        }
        /// <summary>
        /// 获取全部字典项目-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<DictionaryItem>>> GetDictionaryItemsPage(int page = 1, string key = "")
        {
            var data = await _dictionaryServices.GetDictionaryItems(page, key);
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 获取全部字典项目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<DictionaryItem>>> GetAllDictionaryItems()
        {
            var data = await _dictionaryServices.GetAllDictionaryItems();
            return Success(data, "获取成功");
        }
        /// <summary>
        /// 更新字典项目Redis缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DictionaryItem>>> UpdateRedisDictionaryItems()
        {
            var data = await _dictionaryServices.UpdateRedisDictionaryItems();
            return Success(data, "获取成功");
        }
        #endregion
    }
}
