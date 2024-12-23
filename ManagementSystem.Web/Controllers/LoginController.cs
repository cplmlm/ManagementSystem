using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Model.ViewModels;
using ManagementSystem.Model;
using ManagementSystem.IServices;

namespace ManagementSystem.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private readonly ISysUserInfoServices _sysUserInfoServices;

        public LoginController(ISysUserInfoServices sysUserInfoServices)
        {
            _sysUserInfoServices = sysUserInfoServices;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TokenInfoViewModel>> UserLogin(string name = "", string password = "")
        {
            return await _sysUserInfoServices.Login(name, password);
        }
    }
}
