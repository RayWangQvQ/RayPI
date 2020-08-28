using System.Threading.Tasks;

namespace Ray.Application.IAppServices
{
    /// <summary>
    /// 增删改查AppService
    /// </summary>
    /// <typeparam name="TGetDetailOutputDto">获取详情Dto</typeparam>
    /// <typeparam name="TGetListItemOutputDto">获取列表项Dto</typeparam>
    /// <typeparam name="TEntityKey">主键</typeparam>
    /// <typeparam name="TGetListInputDto">请求获取详情Dto</typeparam>
    /// <typeparam name="TCreateInputDto">请求创建Dto</typeparam>
    /// <typeparam name="TUpdateInputDto">请求编辑Dto</typeparam>
    public interface ICrudAppService<in TEntityKey, in TGetListInputDto, in TCreateInputDto, in TUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        : IQueryAppService<TEntityKey, TGetListInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
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
        Task<TGetDetailOutputDto> UpdateAsync(TEntityKey id, TUpdateInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(TEntityKey id);
    }

    /// <summary>
    /// 增删改查AppService
    /// (TCreateOrUpdateInputDto与TCreateOrUpdateInputDto共用一个TCreateOrUpdateInputDto)
    /// </summary>
    /// <typeparam name="TGetDetailOutputDto"></typeparam>
    /// <typeparam name="TGetListItemOutputDto"></typeparam>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TGetListInputDto"></typeparam>
    /// <typeparam name="TCreateOrUpdateInputDto"></typeparam>
    public interface ICrudAppService<in TEntityKey, in TGetListInputDto, in TCreateOrUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        :ICrudAppService<TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
    {

    }

    /// <summary>
    /// 增删改查AppService
    /// (TCreateOrUpdateInputDto与TCreateOrUpdateInputDto共用一个TCreateOrUpdateInputDto)
    /// (TGetDetailOrListItemOutputDto与TGetDetailOrListItemOutputDto共用一个TGetDetailOrListItemOutputDto)
    /// </summary>
    /// <typeparam name="TGetDetailOrListItemOutputDto"></typeparam>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TGetListInputDto"></typeparam>
    /// <typeparam name="TCreateOrUpdateInputDto"></typeparam>
    public interface ICrudAppService<in TEntityKey, in TGetListInputDto, in TCreateOrUpdateInputDto, TGetDetailOrListItemOutputDto>
        :ICrudAppService<TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto, TGetDetailOrListItemOutputDto, TGetDetailOrListItemOutputDto>
    {

    }

    /// <summary>
    /// 增删改查AppService
    /// (共用过一个Dto)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TGetListInputDto"></typeparam>
    public interface ICrudAppService<in TEntityKey, in TGetListInputDto, TDto>
        :ICrudAppService<TEntityKey, TGetListInputDto, TDto, TDto, TDto, TDto>
    {

    }
}
