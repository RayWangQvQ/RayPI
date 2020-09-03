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
    public interface IRepository
    {
        /// <summary>
        /// 工作单元，用于事务性提交数据到数据库
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
