using System.ComponentModel;

namespace ManagementSystem.Model
{
    public enum ResultStatus
    {
        [Description("请求成功")]
        Success = 200,
        [Description("请求失败")]
        Fail = 400,
        [Description("无权访问该接口")]
        CODE401 = 401,
        [Description("访问权限等级不够")]
        CODE403 = 403,
        [Description("资源不存在")]
        CODE404 = 404,
        [Description("请求异常")]
        Error = 500
    }
}
