using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ray.AppService.IAppServices
{
    /// <summary>
    /// 增删改查
    /// </summary>
    /// <typeparam name="TGetOutputDto">获取详情Dto</typeparam>
    /// <typeparam name="TGetListOutputDto">获取列表项Dto</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    /// <typeparam name="TGetListInput">请求获取详情Dto</typeparam>
    /// <typeparam name="TCreateInput">请求创建Dto</typeparam>
    /// <typeparam name="TUpdateInput">请求编辑Dto</typeparam>
    public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IQueryAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">请求添加Dto</param>
        /// <returns></returns>
        Task<TGetOutputDto> CreateAsync(TCreateInput input);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">请求编辑Dto</param>
        /// <returns></returns>
        Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(TKey id);
    }
}
