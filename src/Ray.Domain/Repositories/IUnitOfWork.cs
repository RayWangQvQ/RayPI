using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ray.Domain.Repositories
{
    /// <summary>
    /// 工作单元interface
    /// 【职责：将一组操作事务性地一次性持久化到数据库】
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存修改且广播领域事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
