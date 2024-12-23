

using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using System.Threading.Tasks;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 问卷管理接口
    /// </summary>	
    public interface IQuestionServices :IBaseServices<Question>
    { 
        /// <summary>
        /// 添加问卷
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<long> AddQuestionnaire(Question question);
        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteQuestionnaire(long id);
        /// <summary>
        /// 更新问卷
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
       Task<string> UpdateQuestionnaire(Question question);
        /// <summary>
        /// 获取问卷列表
        /// </summary>
        /// <param name="QuestionnaireTypeId">问卷类型ID</param>
        /// <param name="serialNumber">序号</param>
        /// <param name="description">问题描述</param>
        /// <returns></returns>
        Task<List<Question>> GetQuestionnaires(string QuestionnaireTypeId = "", byte? serialNumber = 0, string description = "");
    }
}
