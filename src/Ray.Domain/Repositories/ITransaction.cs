using System.Threading.Tasks;

namespace Ray.Domain.Repositories
{
    /// <summary>
    /// 事务
    /// </summary>
    public interface ITransaction<TTransaction>
    {
        /// <summary>
        /// 当前事务
        /// </summary>
        TTransaction CurrentTransaction { get; }

        /// <summary>
        /// 是否有正在进行的事务
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task<TTransaction> BeginTransactionAsync();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CommitTransactionAsync(TTransaction transaction);

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();
    }
}
