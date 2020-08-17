using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ray.Domain.Entities;

namespace Ray.Domain.Repositories
{
    /// <summary>
    /// 定义一个【仓储】
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        /*
         * （按照严格DDD标准，泛型应该定义为IAggregateRoot聚合根，这里为了兼容贫血型，泛型定义为IEntity即可）
         */

        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
