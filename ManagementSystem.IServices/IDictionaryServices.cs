using ManagementSystem.Model;
using ManagementSystem.Model.Models;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// �����ֵ����ӿ�
    /// </summary>	
    public interface IDictionaryServices
    {
        /// <summary>
        /// �����ֵ�����
        /// </summary>
        /// <param name="dictionaryType">�ֵ�����</param>
        /// <returns></returns>
        Task<MessageModel<string>> AddDictionaryType(DictionaryType dictionaryType);
        /// <summary>
        /// ɾ���ֵ�����
        /// </summary>
        /// <param name="id">�ֵ�����id</param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteDictionaryType(long id);
        /// <summary>
        /// ��ȡ�ֵ������б�-��ҳ
        /// </summary>
        /// <param name="page">��ҳ</param>
        /// <param name="key">��ѯ�ؼ���</param>
        /// <returns></returns>
        Task<MessageModel<PageModel<DictionaryType>>> GetDictionaryTypes(int page, string key);
        /// <summary>
        /// ��ȡȫ���ֵ�����
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryType>> GetAllDictionaryTypes();
        /// <summary>
        /// �����ֵ�����
        /// </summary>
        /// <param name="dictionaryType">�ֵ�����</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateDictionaryType(DictionaryType dictionaryType);
        /// <summary>
        /// �����ֵ���Ŀ
        /// </summary>
        /// <param name="dictionaryItem">�ֵ���Ŀ</param>
        Task<MessageModel<string>> AddDictionaryItem(DictionaryItem dictionaryItem);
        /// <summary>
        /// ɾ���ֵ���Ŀ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> DeleteDictionaryItem(long id);
        /// <summary>
        /// ��ȡ�ֵ���Ŀ�б�
        /// </summary>
        /// <param name="key">�ֵ�����id��code</param>
        /// <returns></returns>
        Task<MessageModel<List<DictionaryItem>>> GetDictionaryItems(string key);
        /// <summary>
        /// ��ȡȫ���ֵ���Ŀ-��ҳ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<PageModel<DictionaryItem>> GetDictionaryItems(int page = 1, string key = "");
        /// <summary>
        /// �޸��ֵ���Ŀ
        /// </summary>
        /// <param name="dictionaryType">�ֵ���Ŀ</param>
        /// <returns></returns>
        Task<MessageModel<string>> UpdateDictionaryItem(DictionaryItem dictionaryType);
        /// <summary>
        /// ��ȡ�����ֵ���Ŀ
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryItem>> GetAllDictionaryItems();
        /// <summary>
        /// ����Redis�����е��ֵ���Ŀ
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryItem>> UpdateRedisDictionaryItems();
    }
}
