
using Newtonsoft.Json;

namespace ManagementSystem.Model
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// 状态结果
        /// </summary>
        [JsonProperty("status")]
        public ResultStatus Status { get; set; } = ResultStatus.Success;

        private string? _msg;

        /// <summary>
        /// 消息描述
        /// </summary>
        [JsonProperty("message")]
        public string? Message
        {

            get
            {
                return !string.IsNullOrEmpty(_msg) ? _msg : "";
            }
            set
            {
                _msg = value;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        [JsonProperty("data")]
        public T? Data { get; set; }

        /// <summary>
        /// 成功状态返回结果
        /// </summary>
        /// <param name="result">返回的数据</param>
        /// <returns></returns>
        public static MessageModel<T> SuccessResult(T data)
        {
            return new MessageModel<T> { Status = ResultStatus.Success, Data = data };
        }

        public static MessageModel<T> SuccessResult(string message, T data)
        {
            return new MessageModel<T> { Data = data, Status = ResultStatus.Success, Message = message };
        }
        /// <summary>
        /// 失败状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">失败信息</param>
        /// <returns></returns>
        public static MessageModel<T> FailResult(string? msg = null)
        {
            return new MessageModel<T> { Status = ResultStatus.Fail, Message = msg };
        }

        /// <summary>
        /// 异常状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        public static MessageModel<T> ErrorResult(string? msg = null)
        {
            return new MessageModel<T> { Status = ResultStatus.Error, Message = msg };
        }

        /// <summary>
        /// 自定义状态返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static MessageModel<T> Result(ResultStatus status, T data, string? msg = null)
        {
            return new MessageModel<T> { Status = status, Data = data, Message = msg };
        }
        /// <summary>
        /// 隐式将T转化为MessageModel<T>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator MessageModel<T>(T value)
        {
            return new MessageModel<T> { Data = value };
        }
    }
}
