using System;
using System.Threading.Tasks;
using Ray.Application.IAppServices;
using Ray.Domain.Entities;
using Ray.Domain.Helpers;
using Ray.Domain.Repositories;

namespace Ray.Application.AppServices
{
    public class CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateInputDto, TUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        : QueryAppService<TEntity, TEntityKey, TGetListInputDto, TGetDetailOutputDto, TGetListItemOutputDto>,
            ICrudAppService<TEntityKey, TGetListInputDto, TCreateInputDto, TUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public CrudAppService(IRepositoryBase<TEntity, TEntityKey> repository) : base(repository)
        {

        }

        protected virtual string CreatePolicyName { get; set; }

        protected virtual string UpdatePolicyName { get; set; }

        protected virtual string DeletePolicyName { get; set; }

        public virtual async Task<TGetDetailOutputDto> CreateAsync(TCreateInputDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = MapToEntity(input);

            await Repository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public virtual async Task<TGetDetailOutputDto> UpdateAsync(TEntityKey id, TUpdateInputDto input)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);
            //TODO: Check if input has id different than given id and normalize if it's default value, throw ex otherwise
            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public virtual async Task DeleteAsync(TEntityKey id)
        {
            await CheckDeletePolicyAsync();

            await Repository.DeleteAsync(id);
        }



        #region 鉴权
        protected virtual async Task CheckCreatePolicyAsync()
        {
            await CheckPolicyAsync(CreatePolicyName);
        }
        protected virtual async Task CheckUpdatePolicyAsync()
        {
            await CheckPolicyAsync(UpdatePolicyName);
        }
        protected virtual async Task CheckDeletePolicyAsync()
        {
            await CheckPolicyAsync(DeletePolicyName);
        }
        #endregion

        #region Map映射
        /// <summary>
        /// Maps <see cref="TCreateInputDto"/> to <see cref="TEntity"/> to create a new entity.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TEntity MapToEntity(TCreateInputDto createInput)
        {
            var entity = ObjectMapper.Map<TCreateInputDto, TEntity>(createInput);
            SetIdForGuids(entity);
            return entity;
        }
        /// <summary>
        /// Maps <see cref="TUpdateInputDto"/> to <see cref="TEntity"/> to update the entity.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual void MapToEntity(TUpdateInputDto updateInput, TEntity entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }
        #endregion

        /// <summary>
        /// Sets Id value for the entity if <see cref="TEntityKey"/> is <see cref="Guid"/>.
        /// It's used while creating a new entity.
        /// </summary>
        protected virtual void SetIdForGuids(TEntity entity)
        {
            var entityWithGuidId = entity as IEntity<Guid>;

            if (entityWithGuidId == null || entityWithGuidId.Id != Guid.Empty)
            {
                return;
            }

            EntityHelper.TrySetId(
                entityWithGuidId,
                () => GuidGenerator.Create(),
                true
            );
        }
    }
}
