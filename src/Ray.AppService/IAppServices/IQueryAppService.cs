using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ray.Infrastructure.Helpers.Page;

namespace Ray.AppService.IAppServices
{
    public interface IQueryAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput>
    {
        Task<TGetOutputDto> GetAsync(TKey id);

        Task<PageResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input);
    }
}
