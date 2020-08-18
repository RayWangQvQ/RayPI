using System.Threading.Tasks;
using Ray.Infrastructure.Page;

namespace Ray.Application.IAppServices
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TGetOutputDto">返回详情Dto</typeparam>
    /// <typeparam name="TGetListOutputDto">返回列表项Dto</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    /// <typeparam name="TGetListInput">查询列表Dto</typeparam>
    public interface IQueryAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput> : IAppService
    {
        /// <summary>
        /// 根据主键获取详情Dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TGetOutputDto> GetAsync(TKey id);

        /// <summary>
        /// 查询列表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<TGetListOutputDto>> GetPageAsync(TGetListInput input);
    }
}
