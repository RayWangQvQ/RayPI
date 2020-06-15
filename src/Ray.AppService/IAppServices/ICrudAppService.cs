using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.AppService.IAppServices
{
    public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IQueryAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>,
        ICmdAppService<TGetOutputDto, TKey, TCreateInput, TUpdateInput>
    {
    }
}
