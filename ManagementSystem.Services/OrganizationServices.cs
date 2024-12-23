using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using ManagementSystem.Model;
using AutoMapper;
using ManagementSystem.IServices;
using ManagementSystem.Repository.Base;
using ManagementSystem.Services.Base;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ManagementSystem.Services
{
    public class OrganizationServices : BaseServices<Organization>, IOrganizationServices
    {
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly IBaseRepository<UserRole> _userRoleRepository;

        public OrganizationServices(IBaseRepository<Organization> organizationRepository, IMapper mapper, IUser user,IBaseRepository<UserRole> userRoleRepository)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _user = user;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="organization">机构基本信息</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> AddOrganization(OrganizationDto organization)
        {
            var id = await _organizationRepository.Add(_mapper.Map<Organization>(organization));
            if (id > 0)
            {
                return MessageModel<string>.SuccessResult("添加成功", id.ObjToString());
            }
            else
            {
                return MessageModel<string>.FailResult("添加失败");
            }
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="organization">机构基本信息</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> UpdateOrganization(OrganizationDto organization)
        {
            var oldUser = await _organizationRepository.QueryById(organization.Id);
            if (oldUser is not { Id: > 0 })
            {
                return MessageModel<string>.FailResult("机构不存在或已被删除");
            }
            var result = await _organizationRepository.Update(_mapper.Map<Organization>(organization));
            if (result)
            {
                return MessageModel<string>.SuccessResult("更新成功", organization.Id.ObjToString());
            }
            else
            {
                return MessageModel<string>.FailResult("更新失败");
            }
        }
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteOrganization(long id)
        {
            if (id > 0)
            {
                var userDetail = await _organizationRepository.QueryById(id);
                if (userDetail is null)
                {
                    return MessageModel<string>.FailResult("机构不存在或已被删除");
                }
                userDetail.IsDeleted = true;
                bool success = await _organizationRepository.Update(userDetail);
                if (success)
                {
                    return MessageModel<string>.SuccessResult("删除成功");
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
        /// 获取全部机构
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<MessageModel<PageModel<OrganizationDto>>> GetOrganizations(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 10;
            var data = await _organizationRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");
            return MessageModel<PageModel<OrganizationDto>>.SuccessResult("获取成功", data.ConvertTo<OrganizationDto>(_mapper));
        }

        /// <summary>
        /// 获取全部机构
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetAllOrganizations()
        {
            List<Organization> data = new List<Organization>();
            //通过角色id判断是否是超级管理员，超级管理员可以查看所有机构
            var userRole = await   _userRoleRepository.Query(x => x.IsDeleted != true && x.UserId == _user.Id && x.RoleId == 1825476163338899456);
            if (userRole.Count == 0)
            {
                //不是超级管理员，只能查看自己所属机构及下属机构
                var organization = await _organizationRepository.QueryById(_user.OrganizationId);
                data.Add(organization);
                var organizations = await _organizationRepository.Query(x => x.ParentId == _user.OrganizationId);
                if(organizations.Count > 0)
                {
                    data.AddRange(organizations);
                }        
            }
            else
            {
                data = await _organizationRepository.Query(x => x.IsDeleted != true);
            }
            return data;
        }
        /// <summary>
        /// 查询树形机构Table
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="number">序号</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public async Task<List<Organization>> GetTreeTable(long parentId = 0, int number = 0, string key = "")
        {
            List<Organization> organizations = new List<Organization>();
            var organizationsList = await _organizationRepository.Query(d => d.IsDeleted == false);
            // 父节点为0，则为根节点，否则为子节点,number为序号
            int parentKey = number;
            number++;// 序号递增
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            if (key != "")
            {
                organizations = organizationsList.Where(a => a.Name.Contains(key)).ToList();
            }
            else
            {
                organizations = organizationsList.Where(a => a.ParentId == parentId).ToList();
            }
            foreach (var item in organizations)
            {
                var parent = organizationsList.FirstOrDefault(d => d.Id == item.ParentId);
                while (parent != null)
                {
                    parent = organizationsList.FirstOrDefault(d => d.Id == parent.ParentId);
                }
                item.HasChildren = organizationsList.Where(d => d.ParentId == item.Id).Any();
                item.Key = ++number;
                item.ParentKey = parentKey;
            }
            return organizations;
        }
    }
}
