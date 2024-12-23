namespace ManagementSystem.Model
{
    public class ApiResponse
    {
        public ResultStatus Status { get; set; } = ResultStatus.Success;
        public string Value { get; set; } = "";
        public MessageModel<string> MessageModel = new MessageModel<string>() { };

        public ApiResponse(ResultStatus apiCode, string msg = null)
        {
            switch (apiCode)
            {
                case ResultStatus.CODE401:
                {
                    Status = ResultStatus.CODE401;
                    Value = msg ?? "很抱歉，您无权访问该接口，请确保已经登录!";
                }
                    break;
                case ResultStatus.CODE403:
                {
                    Status = ResultStatus.CODE403;
                    Value = msg ?? "很抱歉，您的访问权限等级不够，联系管理员!";
                }
                    break;
                case ResultStatus.CODE404:
                {
                    Status = ResultStatus.CODE404;
                    Value = "资源不存在!";
                }
                    break;
                case ResultStatus.Error:
                {
                    Status = ResultStatus.Error;
                    Value = msg;
                }
                    break;
            }

            MessageModel = new MessageModel<string>()
            {
                Status = Status,
                Message = Value,
            };
        }
    }
}