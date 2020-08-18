using System.Threading.Tasks;
using Ray.Infrastructure.Page;

namespace Ray.Application.IAppServices
{
    /// <summary>
    /// 查询AppService
    /// </summary>
    /// <typeparam name="TGetDetailOutputDto">返回详情Dto</typeparam>
    /// <typeparam name="TGetListItemOutputDto">返回列表项Dto</typeparam>
    /// <typeparam name="TEntityKey">主键</typeparam>
    /// <typeparam name="TGetListInput">查询列表Dto</typeparam>
    public interface IQueryAppService<in TEntityKey, in TGetListInput, TGetDetailOutputDto, TGetListItemOutputDto> : IAppService
    {
        /// <summary>
        /// 根据主键获取详情Dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TGetDetailOutputDto> GetAsync(TEntityKey id);

        /// <summary>
        /// 查询列表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<TGetListItemOutputDto>> GetPageAsync(TGetListInput input);
    }


    /// <summary>
    /// 查询AppService
    /// （详情Dto和集合ItemDto共用一个Dto）
    /// </summary>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TOutPutDto"></typeparam>
    public interface IQueryAppService<in TEntityKey, in TGetListInput, TOutPutDto>
        : IQueryAppService<TEntityKey, TGetListInput, TOutPutDto, TOutPutDto>
    {

    }
}
