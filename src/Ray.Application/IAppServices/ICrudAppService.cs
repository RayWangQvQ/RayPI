using System.Threading.Tasks;

namespace Ray.Application.IAppServices
{
    /// <summary>
    /// 增删改查
    /// </summary>
    /// <typeparam name="TGetDetailOutputDto">获取详情Dto</typeparam>
    /// <typeparam name="TGetListItemOutputDto">获取列表项Dto</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    /// <typeparam name="TGetListInputDto">请求获取详情Dto</typeparam>
    /// <typeparam name="TCreateInputDto">请求创建Dto</typeparam>
    /// <typeparam name="TUpdateInputDto">请求编辑Dto</typeparam>
    public interface ICrudAppService<TGetDetailOutputDto, TGetListItemOutputDto, in TKey, in TGetListInputDto, in TCreateInputDto, in TUpdateInputDto>
        : IQueryAppService<TGetDetailOutputDto, TGetListItemOutputDto, TKey, TGetListInputDto>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">请求添加Dto</param>
        /// <returns></returns>
        Task<TGetDetailOutputDto> CreateAsync(TCreateInputDto input);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">请求编辑Dto</param>
        /// <returns></returns>
        Task<TGetDetailOutputDto> UpdateAsync(TKey id, TUpdateInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(TKey id);
    }
}
