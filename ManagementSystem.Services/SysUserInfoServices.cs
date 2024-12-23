using ManagementSystem.Repository.Base;
using ManagementSystem.IServices;
using ManagementSystem.Model.Models;
using ManagementSystem.Services.Base;
using System.Linq;
using System.Threading.Tasks;
using ManagementSystem.Common;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ManagementSystem.Model.ViewModels;
using System.Security.Claims;
using System.Xml.Linq;
using ManagementSystem.Model;
using System.IdentityModel.Tokens.Jwt;
using ManagementSystem.Extensions.Authorizations.Policys;
using ManagementSystem.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Common.HttpContextUser;
using AutoMapper;
using ManagementSystem.Repository.UnitOfWorks;
using Microsoft.Extensions.Logging;
using System;


namespace ManagementSystem.Services
{
    /// <summary>
    /// sysUserInfoServices
    /// </summary>	
    public class SysUserInfoServices : BaseServices<SysUserInfo>, ISysUserInfoServices
    {
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<SysUserInfo> _sysUserReposiroty;
        private readonly PermissionRequirement _requirement;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly IUnitOfWorkManage _unitOfWorkManage;
        private readonly ILogger<SysUserInfoServices> _logger;
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IBaseRepository<SystemConfig> _systemConfigRepository;
        public SysUserInfoServices(IBaseRepository<UserRole> userRoleRepository, IBaseRepository<Role> roleRepository, IBaseRepository<SysUserInfo> sysUserReposiroty,
            PermissionRequirement requirement, IRoleModulePermissionServices roleModulePermissionServices, IMapper mapper, IUser user, IUnitOfWorkManage unitOfWorkManage,
              ILogger<SysUserInfoServices> logger, IBaseRepository<Organization> organizationRepository, IBaseRepository<SystemConfig> systemConfigRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _sysUserReposiroty = sysUserReposiroty;
            _requirement = requirement;
            _roleModulePermissionServices = roleModulePermissionServices;
            _mapper = mapper;
            _user = user;
            _unitOfWorkManage = unitOfWorkManage;
            _logger = logger;
            _organizationRepository = organizationRepository;
            _systemConfigRepository = systemConfigRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public async Task<SysUserInfo> SaveUserInfo(string loginName, string loginPwd)
        {
            SysUserInfo sysUserInfo = new(loginName, loginPwd);
            SysUserInfo model = new SysUserInfo();
            var userList = await base.Query(a => a.LoginName == sysUserInfo.LoginName && a.LoginPWD == sysUserInfo.LoginPWD);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(sysUserInfo);
                model = await base.QueryById(id);
            }
            return model;
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await base.Query(a => a.LoginName == loginName)).FirstOrDefault();
            var roleList = await _roleRepository.Query(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRoles = await _userRoleRepository.Query(ur => ur.UserId == user.Id);
                if (userRoles.Count > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<MessageModel<TokenInfoViewModel>> Login(string name, string password)
        {
            string jwtStr = string.Empty;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return MessageModel<TokenInfoViewModel>.FailResult("用户名或密码不能为空");
            }
            var user = await _sysUserReposiroty.Query(d => d.LoginName== name && d.LoginPWD==SM4Helper.EncryptCBC(password)  && d.IsDeleted == false);
            if (user == null || user.Count == 0)
            {
                return MessageModel<TokenInfoViewModel>.FailResult("用户名或密码错误");
            }
            if (user.Count > 0)
            {
                var userRoles = await GetUserRoleNameStr(name, password);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstOrDefault().RealName),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().Id.ToString()),
                    new Claim("OrganizationId", user.FirstOrDefault().OrganizationId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.DateToTimeStamp()),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
                var data = await _roleModulePermissionServices.LoginRoleModuleMaps();
                var list = (from item in data
                            select new Extensions.Authorizations.Policys.PermissionItem
                            {
                                Url = item.Url,
                                Role = item.Role,
                            }).ToList();

                _requirement.Permissions = list;
                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return MessageModel<TokenInfoViewModel>.SuccessResult("登录成功", token);
            }
            else
            {
                return MessageModel<TokenInfoViewModel>.FailResult("认证失败"); ;
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUserInfo">用户基本信息</param>
        /// <returns></returns>
        public async Task<string> AddUser(SysUserInfoDto sysUserInfo)
        {
            string result = "";
            var users = await _sysUserReposiroty.Query(d => d.LoginName == sysUserInfo.LoginName && d.IsDeleted == false);
            if (users.Count > 0)
            {
                result="登录名已存在";
            }
            sysUserInfo.LoginPWD = SM4Helper.EncryptCBC(sysUserInfo?.LoginPWD);
            var id = await _sysUserReposiroty.Add(_mapper.Map<SysUserInfo>(sysUserInfo));
            if (id == 0)
            {
                result="添加失败";
            }
            return result;
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        public async Task<MessageModel<string>> UpdateUser(SysUserInfoDto sysUserInfo)
        {
            // 这里使用事务处理
            var data = new MessageModel<string>();
            var oldUser = await _sysUserReposiroty.QueryById(sysUserInfo.Id);
            if (oldUser is not { Id: > 0 })
            {
                return MessageModel<string>.FailResult("用户不存在或已被删除");
            }
            if (sysUserInfo.LoginName != oldUser.LoginName)
            {
                var users = await _sysUserReposiroty.Query(d => d.LoginName == sysUserInfo.LoginName && d.IsDeleted == false);
                if (users.Count > 0)
                {
                    return MessageModel<string>.FailResult("登录名已存在");
                }
            }
            try
            {
                if (sysUserInfo.LoginPWD != oldUser.LoginPWD)
                {
                    oldUser.CriticalModifyTime = DateTime.Now;
                }
                _mapper.Map(sysUserInfo, oldUser);
                _unitOfWorkManage.BeginTran();
                // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
                var usreroles = (await _userRoleRepository.Query(d => d.UserId == oldUser.Id));
                if (usreroles.Any())
                {
                    var ids = usreroles.Select(d => d.Id.ToString()).ToArray();
                    var isAllDeleted = await _userRoleRepository.DeleteByIds(ids);
                    if (!isAllDeleted)
                    {
                        return MessageModel<string>.FailResult("服务器更新异常");
                    }
                }
                // 然后再执行添加操作
                if (sysUserInfo.RIDs.Count > 0)
                {
                    var userRolsAdd = new List<UserRole>();
                    sysUserInfo.RIDs.ForEach(rid => { userRolsAdd.Add(new UserRole(oldUser.Id, rid)); });

                    var oldRole = usreroles.Select(s => s.RoleId).OrderBy(i => i).ToArray();
                    var newRole = userRolsAdd.Select(s => s.RoleId).OrderBy(i => i).ToArray();
                    if (!oldRole.SequenceEqual(newRole))
                    {
                        oldUser.CriticalModifyTime = DateTime.Now;
                    }
                    await _userRoleRepository.Add(userRolsAdd);
                }
                oldUser.LoginPWD = SM4Helper.EncryptCBC(oldUser.LoginPWD);
                bool success = await _sysUserReposiroty.Update(oldUser);

                _unitOfWorkManage.CommitTran();

                if (success)
                {
                    data = MessageModel<string>.SuccessResult("更新成功", oldUser.Id.ObjToString());
                }
            }
            catch (Exception e)
            {
                _unitOfWorkManage.RollbackTran();
                _logger.LogError(e, e.Message);
            }

            return data;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteUser(long id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userDetail = await _sysUserReposiroty.QueryById(id);
                userDetail.IsDeleted = true;
                bool success = await _sysUserReposiroty.Update(userDetail);
                if (success)
                {
                    return MessageModel<string>.SuccessResult("删除成功", userDetail?.Id.ObjToString());
                }
                else
                {
                    return MessageModel<string>.FailResult("删除失败");
                }
            }
            else
            {
                return MessageModel<string>.FailResult("删除失败,请传入正确的Id");
            }
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key">用户名称</param>
        /// <param name="organizationId">机构id</param>
        /// <returns></returns>
        public async Task<MessageModel<PageModel<SysUserInfoDto>>> GetUsers(int page = 1, string key = "", string organizationId = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 10;
            var data = await _sysUserReposiroty.QueryPage(a => a.IsDeleted != true && a.Status >= 0 && a.OrganizationId == organizationId.ObjToLong() && ((a.LoginName != null && a.LoginName.Contains(key)) || (a.RealName != null && a.RealName.Contains(key))), page, intPageSize, " Id desc ");
            #region MyRegion
            // 这里可以封装到多表查询，此处简单处理
            var allUserRoles = await _userRoleRepository.Query(d => d.IsDeleted == false);
            var allRoles = await _roleRepository.Query(d => d.IsDeleted == false);
            //   var allDepartments = await _departmentServices.Query(d => d.IsDeleted == false);

            var sysUserInfos = data.data;
            foreach (var item in sysUserInfos)
            {
                var currentUserRoles = allUserRoles.Where(d => d.UserId == item.Id).Select(d => d.RoleId).ToList();
                item.RIDs = currentUserRoles;
                item.RoleNames = allRoles.Where(d => currentUserRoles.Contains(d.Id)).Select(d => d.Name).ToList();
                if (item?.OrganizationId > 0)
                {
                    var organization = await _organizationRepository.QueryById(item?.OrganizationId);
                    item.OrganizationName = organization?.Name;
                }
                // var departmentNameAndIds = GetFullDepartmentName(allDepartments, item.DepartmentId);
                //item.DepartmentName = departmentNameAndIds.Item1;
                // item.Dids = departmentNameAndIds.Item2;

            }
            data.data = sysUserInfos;

            #endregion
            return MessageModel<PageModel<SysUserInfoDto>>.SuccessResult("获取成功", data.ConvertTo<SysUserInfoDto>(_mapper));
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        public async Task<string> UpdatePassword(SysUserInfoDto sysUserInfo)
        {
            var user = await _sysUserReposiroty.QueryById(sysUserInfo.Id);
            if (user== null)
            {
                return "用户不存在或已被删除";
            }
            if (user.LoginPWD != SM4Helper.EncryptCBC(sysUserInfo.OldLoginPWD))
            {
                return "旧密码错误";
            }
            user.LoginPWD = SM4Helper.EncryptCBC(sysUserInfo.LoginPWD);
            user.CriticalModifyTime = DateTime.Now;
            bool success = await _sysUserReposiroty.Update(user);
            return success ? "" : "修改失败";
        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        public async Task<string> ResetUserPassword(SysUserInfoDto sysUserInfo)
        {
            var user = await _sysUserReposiroty.QueryById(sysUserInfo.Id);
            if (user == null)
            {
                return "用户不存在或已被删除";
            }
            user.LoginPWD = SM4Helper.EncryptCBC("123456");
            user.CriticalModifyTime = DateTime.Now;
            bool success = await _sysUserReposiroty.Update(user);
            return success ? "" : "重置密码失败";
        }
    }
}