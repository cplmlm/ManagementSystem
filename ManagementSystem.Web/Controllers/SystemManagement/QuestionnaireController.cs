using ManagementSystem.IServices;
using ManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Web.Controllers;
using ManagementSystem.Model.Models;



namespace ManagementSystem.Controllers
{
    /// <summary>
    /// 调查问卷
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class QuestionnaireController : BaseApiController
    {
        readonly IQuestionServices _questionnaireServices;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="questionnaireServices"></param>
        public QuestionnaireController(IQuestionServices questionnaireServices)
        {
            _questionnaireServices = questionnaireServices;
        }

        /// <summary>
        /// 添加调查问卷
        /// </summary>
        /// <param name="question">调查问卷</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddQuestionnaire([FromBody] Question question)
        {
            var id = await _questionnaireServices.AddQuestionnaire(question);
            return id > 0 ? Success("添加成功") : Failed("添加失败");
        }
        /// <summary>
        /// 更新调查问卷
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateQuestionnaire([FromBody] Question question)
        {
            if (question == null || question.Id <= 0)
            {
                return Failed("缺少参数");
            }
            string result = await _questionnaireServices.UpdateQuestionnaire(question);
            return string.IsNullOrEmpty(result) ? Success("更新成功") : Failed(result);
        }

        /// <summary>
        /// 删除调查问卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteQuestionnaire(long id)
        {
            string result = await _questionnaireServices.DeleteQuestionnaire(id);
            return string.IsNullOrEmpty(result) ? Success("删除成功") : Failed(result);
        }

        /// <summary>
        /// 获取问卷列表
        /// </summary>
        /// <param name="QuestionnaireTypeId">问卷类型ID</param>
        /// <param name="serialNumber">序号</param>
        /// <param name="description">问题描述</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Question>>> GetQuestionnaires(string QuestionnaireTypeId = "", byte? serialNumber = 0, string description = "")
        {
            var data = await _questionnaireServices.GetQuestionnaires(QuestionnaireTypeId, serialNumber, description);
           // var data=await _questionnaireServices.Query();
            return Success(data, "获取成功");
        }
    }
}
