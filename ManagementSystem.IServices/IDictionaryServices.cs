using ManagementSystem.Model;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 数据字典服务接口
    /// </summary>	
    public interface IDictionaryServices
    {
        /// <summary>
        /// 新增字典类型
        /// </summary>
        /// <param name="dictionaryType">字典类型</param>
        /// <returns></returns>
        Task<MessageModel<string>> AddDictionaryType(DictionaryType dictionaryType);
        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="id">字典类型id</param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteDictionaryType(long id);
        /// <summary>
        /// 获取字典类型列表-分页
        /// </summary>
        /// <param name="page">分页</param>
        /// <param name="key">查询关键字</param>
        /// <returns></returns>
        Task<MessageModel<PageModel<DictionaryType>>> GetDictionaryTypes(int page, string key);
        /// <summary>
        /// 获取全部字典类型
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryType>> GetAllDictionaryTypes();
        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="dictionaryType">字典类型</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateDictionaryType(DictionaryType dictionaryType);
        /// <summary>
        /// 新增字典项目
        /// </summary>
        /// <param name="dictionaryItem">字典项目</param>
        Task<MessageModel<string>> AddDictionaryItem(DictionaryItem dictionaryItem);
        /// <summary>
        /// 删除字典项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteDictionaryItem(long id);
        /// <summary>
        /// 获取字典项目列表
        /// </summary>
        /// <param name="key">字典类型id或code</param>
        /// <returns></returns>
        Task<MessageModel<List<DictionaryItem>>> GetDictionaryItems(string key);
        /// <summary>
        /// 获取全部字典项目-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<PageModel<DictionaryItem>> GetDictionaryItems(int page = 1, string key = "");
        /// <summary>
        /// 修改字典项目
        /// </summary>
        /// <param name="dictionaryType">字典项目</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateDictionaryItem(DictionaryItem dictionaryType);
        /// <summary>
        /// 获取所有字典项目
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryItem>> GetAllDictionaryItems();
        /// <summary>
        /// 更新Redis缓存中的字典项目
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryItem>> UpdateRedisDictionaryItems();
    }
}
