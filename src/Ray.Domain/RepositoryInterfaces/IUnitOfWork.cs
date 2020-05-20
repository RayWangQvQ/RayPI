using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ray.Domain.RepositoryInterfaces
{
    /// <summary>
    /// 工作单元interface
    /// 【职责：持久化到数据库】
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// </summary>
        int SaveChanges();

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
