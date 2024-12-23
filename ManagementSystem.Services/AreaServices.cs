using ManagementSystem.Model.ViewModels;
using ManagementSystem.Model.Models;
using ManagementSystem.Services.Base;
using ManagementSystem.IServices;
using ManagementSystem.Common.Helper;
using ManagementSystem.Extensions;
using ManagementSystem.Repository.Base;

namespace ManagementSystem.Services;

/// <summary>
/// 行政地区服务层
/// </summary>	
public class AreaServices : BaseServices<Area>, IAreaServices
{
    private readonly IBaseRepository<Area> _areaRepository;

    public AreaServices(IBaseRepository<Area> areaRepository)
    {
        _areaRepository = areaRepository;
    }
    /// <summary>
    /// 获取行政区划树
    /// </summary>
    /// <returns></returns>
    public async Task<AreaTree> GetAreaTree()
    {
        AreaTree areaTree = new AreaTree();
            var areas = await _areaRepository.Query(x => x.IsDeleted == false);
            var areaTrees = (from child in areas
                             where child.IsDeleted == false
                             orderby child.Id
                             select new AreaTree
                             {
                                 Value = child.Id.ObjToString(),
                                 Label = child.Name,
                                 ParentId = child.ParentId,
                             }).ToList();
            AreaTree rootRoot = new AreaTree()
            {
                Value = "0",
                ParentId = 0,
                Label = "",
            };
            RecursionHelper.LoopAreaChildren(areaTrees, rootRoot);
            areaTree = rootRoot;
        return areaTree;
    }
}
