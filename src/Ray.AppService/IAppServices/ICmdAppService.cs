using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ray.AppService.IAppServices
{
    public interface ICmdAppService<TGetOutputDto, in TKey, in TCreateInput, in TUpdateInput>
    {
        Task<TGetOutputDto> CreateAsync(TCreateInput input);

        Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input);

        Task DeleteAsync(TKey id);
    }
}
