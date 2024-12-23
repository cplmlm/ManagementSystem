using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Common.Helper;
using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Extensions.AuthHelper;
using ManagementSystem.IServices;
using ManagementSystem.Model;
using ManagementSystem.Model.ViewModels;

namespace ManagementSystem.Web.Controllers
{
  /// <summary>
  /// 用户管理
  /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class UserController : BaseApiController
    {
        private readonly ISysUserInfoServices _sysUserInfoServices;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="mapper"></param>
        public UserController(ISysUserInfoServices sysUserInfoServices, IMapper mapper, IUser user)
        {
            _sysUserInfoServices = sysUserInfoServices;
            _mapper = mapper;
            _user = user;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<TokenInfoViewModel>> UserLogin(string name = "", string password = "")
        {
            return await _sysUserInfoServices.Login(name, password);
        }
        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> AddUser([FromBody] SysUserInfoDto sysUserInfo)
        {
            var data = await _sysUserInfoServices.AddUser(sysUserInfo);
            return string.IsNullOrEmpty(data) ? Success("添加用户成功") : Failed(data);
        }
        /// <summary>
        /// 更新用户与角色
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdateUser([FromBody] SysUserInfoDto sysUserInfo)
        {
            var data = await _sysUserInfoServices.UpdateUser(sysUserInfo);
            return data;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> UpdatePassword([FromBody] SysUserInfoDto sysUserInfo)
        {
            var result = await _sysUserInfoServices.UpdatePassword(sysUserInfo);
            return string.IsNullOrEmpty(result) ? Success("修改密码成功") : Failed(result);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteUser(long id)
        {
            var data = await _sysUserInfoServices.DeleteUser(id);
            return data;
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key">用户姓名/账号名称</param>
        /// <param name="organizationId">机构id</param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageModel<SysUserInfoDto>>> GetUsers(int page = 1, string key = "", string organizationId = "")
        {
            var data = await _sysUserInfoServices.GetUsers(page, key, organizationId);
            return data;
        }
        /// <summary>
        /// 获取用户详情根据token
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<SysUserInfoDto>> GetInfoByToken(string token)
        {
            var data = new MessageModel<SysUserInfoDto>();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                if (tokenModel != null && tokenModel.Uid > 0)
                {
                    var userinfo = await _sysUserInfoServices.QueryById(tokenModel.Uid);
                    if (userinfo != null)
                    {
                        data.Data = _mapper.Map<SysUserInfoDto>(userinfo);
                        data.Message = "获取成功";
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 获取明文密码
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<string> GetPlaintextPassword(string ciphertext)
        {
            return Success(SM4Helper.DecryptCBC(ciphertext), "获取明文密码成功");
        }
        /// <summary>
        /// 获取明文密码
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<string> GetCiphertextPassword(string plaintext)
        {
            return Success(SM4Helper.DecryptCBC(plaintext), "获取密文密码成功");
        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="sysUserInfo">用户信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> ResetUserPassword([FromBody] SysUserInfoDto sysUserInfo)
        {
            var result = await _sysUserInfoServices.ResetUserPassword(sysUserInfo);
            return string.IsNullOrEmpty(result) ? Success("重置密码成功") : Failed(result);
        }
        /// <summary>
        /// 获取当前用户机构下所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SysUserInfoDto>>> GetUsersByOrganizationId()
        {
            var sysUserInfos = await _sysUserInfoServices.Query(x => x.OrganizationId==_user.OrganizationId);
            return Success(_mapper.Map<List<SysUserInfoDto>>(sysUserInfos));
        }
    }
}
