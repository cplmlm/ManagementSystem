using ManagementSystem.IServices.Base;
using ManagementSystem.Model;
using ManagementSystem.Model.Models;
using ManagementSystem.Model.ViewModels;

namespace ManagementSystem.IServices
{
    /// <summary>
    /// 行政地区服务接口
    /// </summary>	
    public interface IAreaServices : IBaseServices<Area>
	{
          Task<AreaTree> GetAreaTree();
    }
}
