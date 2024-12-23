using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Model.Models;
using ManagementSystem.IServices;
using ManagementSystem.Repository.Base;
using ManagementSystem.Services.Base;
using ManagementSystem.Repository.UnitOfWorks;
using System.Linq.Expressions;
using ManagementSystem.Common.Extensions;
using ManagementSystem.Extensions;

namespace ManagementSystem.Services
{
    public class QuestionnaireServices : BaseServices<Question>, IQuestionServices
    {
        private readonly IBaseRepository<Question> _questionRepository;
        private readonly IBaseRepository<QuestionOption> _questionOptionRepository;
        private readonly IRedisBasketRepository _redisBasketRepository;
        private readonly IUser _user;
        private readonly IUnitOfWorkManage _unitOfWorkManage;
        public QuestionnaireServices(IBaseRepository<Question> questionRepository, IBaseRepository<QuestionOption> questionOptionRepository,
            IUser user, IUnitOfWorkManage unitOfWorkManage, IRedisBasketRepository redisBasketRepository)
        {
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _user = user;
            _unitOfWorkManage = unitOfWorkManage;
            _redisBasketRepository = redisBasketRepository;
        }

        /// <summary>
        /// 添加调查问卷
        /// </summary>
        /// <param name="question">调查问卷</param>
        /// <returns></returns>
        public async Task<long> AddQuestionnaire(Question question)
        {
            long id = 0;
            try
            {
                _unitOfWorkManage.BeginTran();
                question.CreateId = _user.Id;
                question.CreateBy = _user.Name;
                id = await _questionRepository.Add(question);
                if (question.Options is not null)
                {
                    List<QuestionOption> options = new List<QuestionOption>();
                    foreach (var option in question.Options)
                    {
                        options.Add(new QuestionOption
                        {
                            QuestionId = id,
                            SerialNumber = option.SerialNumber,
                            Description = option.Description,
                            CreateId = _user.Id,
                            CreateBy = _user.Name,
                        });
                    }
                    var idList = await _questionOptionRepository.Add(options);
                    _unitOfWorkManage.CommitTran();
                }
                else
                {
                    id = 0;
                }
            }
            catch (Exception e)
            {
                _unitOfWorkManage.RollbackTran();
                id = 0;
                //_logger.LogError(e, e.Message);
            }
            return id;
        }
        /// <summary>
        /// 更新调查问卷
        /// </summary>
        /// <param name="question">调查问卷</param>
        /// <returns></returns>
        public async Task<string> UpdateQuestionnaire(Question question)
        {
            bool success = false;
            if (question.Id == 0)
            {
                return "更新失败,请传入正确的Id";
            }
            var oldQuestion = await _questionRepository.QueryById(question.Id);
            if (question is null)
            {
                return "调查问卷不存在或已被删除";
            }
            if (question.Options is null || question.Options.Count == 0)
            {
                return "更新失败,请传入选项";
            }

            try
            {
                _unitOfWorkManage.BeginTran();
                question.ModifyId = _user.Id;
                question.ModifyBy = _user.Name;
                question.ModifyTime = DateTime.Now;
                question.CreateBy = oldQuestion.CreateBy;
                question.CreateId = oldQuestion.CreateId;
                question.CreateTime = oldQuestion.CreateTime;
                question.QuestionnaireTypeId = oldQuestion.QuestionnaireTypeId;
                success = await _questionRepository.Update(question);
                success = await _questionOptionRepository.Update(question.Options);
                _unitOfWorkManage.CommitTran();
            }
            catch (Exception e)
            {
                _unitOfWorkManage.RollbackTran();
                //_logger.LogError(e, e.Message);
            }
            return success ? "" : "更新失败";
        }
        /// <summary>
        /// 删除调查问卷
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<string> DeleteQuestionnaire(long id)
        {
            bool success = false;
            if (id == 0)
            {
                return "删除失败,请传入正确的Id";
            }
            var question = await _questionRepository.QueryById(id);
            if (question is null)
            {
                return "调查问卷不存在或已被删除";
            }
            try
            {
                _unitOfWorkManage.BeginTran();
                question.IsDeleted = true;
                question.ModifyId = _user.Id;
                question.ModifyBy = _user.Name;
                question.ModifyTime = DateTime.Now;
                success = await _questionRepository.Update(question);
                List<QuestionOption> options = await _questionOptionRepository.Query(x => x.QuestionId == id);
                if (options is not null)
                {
                    foreach (var option in options)
                    {
                        option.IsDeleted = true;
                        option.ModifyId = _user.Id;
                        option.ModifyBy = _user.Name;
                        option.ModifyTime = DateTime.Now;
                    }
                    success = await _questionOptionRepository.Update(options);
                    _unitOfWorkManage.CommitTran();
                }
            }
            catch (Exception e)
            {
                _unitOfWorkManage.RollbackTran();
                //_logger.LogError(e, e.Message);
            }
            return success ? "" : "删除失败";
        }
        /// <summary>
        /// 获取问卷列表
        /// </summary>
        /// <param name="QuestionnaireTypeId">问卷类型ID</param>
        /// <param name="serialNumber">序号</param>
        /// <param name="description">问题描述</param>
        /// <returns></returns>
        public async Task<List<Question>> GetQuestionnaires(string QuestionnaireTypeId = "", byte? serialNumber = 0, string description = "")
        {
            var result = await _redisBasketRepository.Get<object>("Questionnaires");
            List<Question> data = new List<Question>();
            if (result != null)
            {
                data = await _redisBasketRepository.Get<List<Question>>("Questionnaires");
            }
            else
            {
                //查询条件  
                Expression<Func<Question, bool>> condition = x => x.IsDeleted == false;
                if (!string.IsNullOrEmpty(QuestionnaireTypeId))
                {
                    condition = condition.And(x => x.QuestionnaireTypeId == QuestionnaireTypeId.ObjToLong());
                }
                if (serialNumber != 0)
                {
                    condition = condition.And(x => x.SerialNumber == serialNumber);
                }
                if (!string.IsNullOrEmpty(description))
                {
                    condition = condition.And(x => x.Description.Contains(description));
                }
                // 获取问题选项
                var questions = await _questionRepository.Query(condition);
                if (questions is not null && questions.Count > 0)
                {
                    foreach (var question in questions)
                    {
                        var options = await _questionOptionRepository.Query(x => x.QuestionId == question.Id);
                        question.Options = options.OrderBy(x => x.SerialNumber).ToList();
                    }
                    data = questions.OrderBy(x => x.SerialNumber).ToList();
                }
                // 缓存问题列表
                if (data.Count > 0)
                {
                    await _redisBasketRepository.Set("Questionnaires", data, TimeSpan.MaxValue);//缓存一直有效
                }
            }
            return data;
        }
    }
}
