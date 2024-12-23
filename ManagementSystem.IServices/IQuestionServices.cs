

using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// �ʾ����ӿ�
    /// </summary>	
    public interface IQuestionServices :IBaseServices<Question>
    { 
        /// <summary>
        /// ����ʾ�
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<long> AddQuestionnaire(Question question);
        /// <summary>
        /// ɾ���ʾ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteQuestionnaire(long id);
        /// <summary>
        /// �����ʾ�
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
       Task<string> UpdateQuestionnaire(Question question);
        /// <summary>
        /// ��ȡ�ʾ��б�
        /// </summary>
        /// <param name="QuestionnaireTypeId">�ʾ�����ID</param>
        /// <param name="serialNumber">���</param>
        /// <param name="description">��������</param>
        /// <returns></returns>
        Task<List<Question>> GetQuestionnaires(string QuestionnaireTypeId = "", byte? serialNumber = 0, string description = "");
    }
}
