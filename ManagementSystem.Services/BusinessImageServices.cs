using ManagementSystem.Common.HttpContextUser;
using ManagementSystem.Model.Models;
using ManagementSystem.IServices;
using ManagementSystem.Repository.Base;
using ManagementSystem.Services.Base;

namespace ManagementSystem.Services
{
    public class BusinessImageServices: BaseServices<BusinessImage>, IBusinessImageServices
    {
        private readonly IBaseRepository<BusinessImage> _imageRepository;
        private readonly IUser _user;

        public BusinessImageServices(IBaseRepository<BusinessImage> imageRepository,IUser user)
        {
            _imageRepository = imageRepository;
            _user = user;
        }

        /// <summary>
        /// 添加业务图片
        /// </summary>
        /// <param name="image">业务图片基本信息</param>
        /// <returns></returns>
        public async Task<long> AddBusinessImage(BusinessImage image)
        {
            image.CreateId = _user.Id;
            image.CreateTime = DateTime.Now;
            var id = await _imageRepository.Add(image);
            return id;
        }
        /// <summary>
        /// 更新业务图片
        /// </summary>
        /// <param name="image">业务图片基本信息</param>
        /// <returns></returns>
        public async Task<string> UpdateBusinessImage(BusinessImage image)
        {
            var oldBusinessImage = await _imageRepository.QueryById(image.Id);
            if (oldBusinessImage is not { Id: > 0 })
            {
                return "业务图片不存在或已被删除";
            }
            oldBusinessImage.ModifyId = _user.Id;
            oldBusinessImage.ModifyTime = DateTime.Now;
            oldBusinessImage.Base64String = image.Base64String;
            var result = await _imageRepository.Update(oldBusinessImage);
            return result ? "" : "更新失败";
        }
    }
}
