using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;
using ManagementSystem.Model;
using AutoMapper;
using ManagementSystem.IServices;
using ManagementSystem.Repository.Base;
using ManagementSystem.Services.Base;
using System.Collections.Generic;
using ManagementSystem.Extensions;

namespace ManagementSystem.Services
{
    public class DictionaryServices : BaseServices<DictionaryType>, IDictionaryServices
    {
        private readonly IBaseRepository<DictionaryType> _dictionaryTypeRepository;
        private readonly IBaseRepository<DictionaryItem> _dictionaryItemRepository;
        private readonly IRedisBasketRepository _redisBasketRepository;
        private readonly IUser _user;
        public DictionaryServices(IBaseRepository<DictionaryType> dictionaryTypeRepository, 
            IBaseRepository<DictionaryItem> dictionaryItemRepository,
            IUser user, IRedisBasketRepository redisBasketRepository)
        {
            _dictionaryTypeRepository = dictionaryTypeRepository;
            _dictionaryItemRepository = dictionaryItemRepository;
            _redisBasketRepository = redisBasketRepository;
            _user = user;
        }

        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="dictionaryType">字典类型</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> AddDictionaryType(DictionaryType dictionaryType)
        {
            dictionaryType.CreateBy = _user.Name;
            dictionaryType.CreateId = _user.Id;
            dictionaryType.CreateTime = DateTime.Now;
            var result = await Validate(dictionaryType);
            if (!string.IsNullOrEmpty(result))
            {
                return MessageModel<string>.FailResult(result);
            }
            var id = await _dictionaryTypeRepository.Add(dictionaryType);
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
        /// 验证
        /// </summary>
        private async Task<string> Validate(DictionaryType dictionaryType)
        {
            string result = string.Empty;
            var model = await _dictionaryTypeRepository.Query(a => a.Name == dictionaryType.Name);
            if (model.Count > 0)
            {
                result = "字典类型名称已存在！";
            }
            //model = await _dictionaryItemRepository.Query(a => a.Code == dictionaryType.Code);
            //if (model.Count > 0)
            //{
            //    result += "字典类型编码已存在！";
            //}
            return result;
        }
        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="dictionaryType">字典类型基本信息</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> UpdateDictionaryType(DictionaryType dictionaryType)
        {
            var oldDictionaryType = await _dictionaryTypeRepository.QueryById(dictionaryType.Id);
            if (oldDictionaryType is not { Id: > 0 })
            {
                return MessageModel<string>.FailResult("字典类型不存在或已被删除");
            }
            oldDictionaryType.Name = dictionaryType.Name;
            oldDictionaryType.Code = dictionaryType.Code;
            oldDictionaryType.ModifyId = _user.Id;
            oldDictionaryType.ModifyBy = _user.Name;
            oldDictionaryType.ModifyTime = DateTime.Now;
            var result = await _dictionaryTypeRepository.Update(oldDictionaryType);
            if (result)
            {
                return MessageModel<string>.SuccessResult("更新成功", oldDictionaryType.Id.ObjToString());
            }
            else
            {
                return MessageModel<string>.FailResult("更新失败");
            }
        }
        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteDictionaryType(long id)
        {
            if (id > 0)
            {
                var userDetail = await _dictionaryTypeRepository.QueryById(id);
                userDetail.IsDeleted = true;
                bool success = await _dictionaryTypeRepository.Update(userDetail);
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
        /// 获取全部字典类型-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<MessageModel<PageModel<DictionaryType>>> GetDictionaryTypes(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 10;
            var data = await _dictionaryTypeRepository.QueryPage(a => a.IsDeleted != true && ((a.Name != null && a.Name.Contains(key))), page, intPageSize, "CreateTime desc ");
            return MessageModel<PageModel<DictionaryType>>.SuccessResult("获取成功", data);
        }
        /// <summary>
        /// 获取全部字典类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryType>> GetAllDictionaryTypes()
        {
            var data = await _dictionaryTypeRepository.Query(a => a.IsDeleted != true, "CreateTime desc ");
            return data;
        }
        /// <summary>
        /// 添加字典项目
        /// </summary>
        /// <param name="dictionaryItem">字典项目</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> AddDictionaryItem(DictionaryItem dictionaryItem)
        {
            dictionaryItem.CreateBy = _user.Name;
            dictionaryItem.CreateId = _user.Id;
            dictionaryItem.CreateTime = DateTime.Now;
            var id = await _dictionaryItemRepository.Add(dictionaryItem);
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
        /// 更新字典项目
        /// </summary>
        /// <param name="dictionaryItem">字典项目基本信息</param>
        /// <returns></returns>
        public async Task<MessageModel<string>> UpdateDictionaryItem(DictionaryItem dictionaryItem)
        {
            var oldDictionaryItem = await _dictionaryItemRepository.QueryById(dictionaryItem.Id);
            if (oldDictionaryItem is not { Id: > 0 })
            {
                return MessageModel<string>.FailResult("字典项目不存在或已被删除");
            }
            oldDictionaryItem.Name = dictionaryItem.Name;
            oldDictionaryItem.Code = dictionaryItem.Code;
            oldDictionaryItem.Description = dictionaryItem.Description;
            oldDictionaryItem.SerialNumber = dictionaryItem.SerialNumber;
            oldDictionaryItem.ModifyId = _user.Id;
            oldDictionaryItem.ModifyBy = _user.Name;
            oldDictionaryItem.ModifyTime = DateTime.Now;
            var result = await _dictionaryItemRepository.Update(oldDictionaryItem);
            if (result)
            {
                return MessageModel<string>.SuccessResult("更新成功", oldDictionaryItem.Id.ObjToString());
            }
            else
            {
                return MessageModel<string>.FailResult("更新失败");
            }
        }
        /// <summary>
        /// 删除字典项目
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteDictionaryItem(long id)
        {
            if (id > 0)
            {
                var userDetail = await _dictionaryItemRepository.QueryById(id);
                userDetail.IsDeleted = true;
                bool success = await _dictionaryItemRepository.Update(userDetail);
                if (success)
                {
                    return MessageModel<string>.SuccessResult("删除成功",null);
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
        /// 获取字典项目
        /// </summary>
        /// <param name="key">字典类型id或者code</param>
        /// <returns></returns>
        public async Task<MessageModel<List<DictionaryItem>>> GetDictionaryItems(string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            var data = await _dictionaryItemRepository.Query(a => a.IsDeleted != true && (a.DictionaryTypeId == key.ObjToLong()), " SerialNumber asc ");
            return MessageModel<List<DictionaryItem>>.SuccessResult("获取成功", data);
        }
        /// <summary>
        /// 获取全部字典项目-分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<PageModel<DictionaryItem>> GetDictionaryItems(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 10;
            var data = await _dictionaryItemRepository.QueryPage(a => a.IsDeleted != true && a.DictionaryTypeId == key.ObjToLong(), page, intPageSize, "CreateTime desc ");
            return data;
        }
        /// <summary>
        /// 获取所有字典项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryItem>> GetAllDictionaryItems()
        {
            List<DictionaryItem> dictionaryItems = new List<DictionaryItem>();
            var result = await _redisBasketRepository.Get<object>("DictionaryItems");
            if (result != null)
            {
                dictionaryItems = await _redisBasketRepository.Get<List<DictionaryItem>>("DictionaryItems");
            }
            else
            {
                dictionaryItems = await _dictionaryItemRepository.Query(a => a.IsDeleted != true, "serialNumber asc ");
                if (dictionaryItems.Count > 0)
                {
                    await _redisBasketRepository.Set("DictionaryItems", dictionaryItems, TimeSpan.MaxValue);//缓存一直有效
                }
            }
            return dictionaryItems;
        }
        /// <summary>
        /// 更新字典项目Redis缓存
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryItem>> UpdateRedisDictionaryItems()
        {
            await _redisBasketRepository.Remove("DictionaryItems");
            var dictionaryItems = await _dictionaryItemRepository.Query(a => a.IsDeleted != true, "serialNumber asc ");
            if (dictionaryItems.Count > 0)
            {
                await _redisBasketRepository.Set("DictionaryItems", dictionaryItems, TimeSpan.MaxValue);//缓存一直有效
            }
            return dictionaryItems;
        }
    }
}
