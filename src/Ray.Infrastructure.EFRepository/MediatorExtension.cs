using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ray.Domain;
using Ray.Domain.Entities;

namespace Ray.Infrastructure.EFRepository
{
    static class MediatorExtension
    {
        /// <summary>
        /// 广播领域事件
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task PublishDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            IEnumerable<EntityEntry<Entity>> domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            List<IDomainEvent> domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
