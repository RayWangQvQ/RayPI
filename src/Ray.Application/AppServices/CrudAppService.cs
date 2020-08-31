using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.Dtos;
using Ray.Application.IAppServices;
using Ray.Domain.Entities;
using Ray.Domain.Helpers;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Page;

namespace Ray.Application.AppServices
{
    public class CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateInputDto, TUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        : QueryAppService<TEntity, TEntityKey, TGetListInputDto, TGetDetailOutputDto, TGetListItemOutputDto>,
            ICrudAppService<TEntityKey, TGetListInputDto, TCreateInputDto, TUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        protected virtual IUnitOfWork UnitOfWork => this.ServiceProvider.GetRequiredService<IUnitOfWork>();

        public CrudAppService(IServiceProvider serviceProvider) : base(serviceProvider)
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
            if (entity == null) throw new Exception($"Id is not exist:{id}");//todo:异常

            MapToEntity(input, entity);

            await Repository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public virtual async Task DeleteAsync(TEntityKey id)
        {
            await CheckDeletePolicyAsync();

            await Repository.DeleteAsync(id, true);
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
        /// 映射：创建Dto <see cref="TCreateInputDto"/>——>Entity <see cref="TEntity"/>
        /// </summary>
        /// <param name="createInput"></param>
        /// <returns></returns>
        protected virtual TEntity MapToEntity(TCreateInputDto createInput)
        {
            var entity = RayMapper.Map<TCreateInputDto, TEntity>(createInput);
            SetIdForGuids(entity);
            return entity;
        }

        /// <summary>
        /// 映射： 编辑Dto <see cref="TUpdateInputDto"/> ——> 实体 <see cref="TEntity"/>.
        /// 默认使用 <see cref="IRayMapper"/> 实现.
        /// </summary>
        protected virtual void MapToEntity(TUpdateInputDto updateInput, TEntity entity)
        {
            if (updateInput is IEntityDto<TEntityKey> entityDto)
            {
                entityDto.Id = entity.Id;
                RayMapper.Map(updateInput, entity);
                return;
            }

            TrySetUpdateInputDtoIdByPropertyName(updateInput, entity);

            RayMapper.Map(updateInput, entity);
        }

        #endregion

        /// <summary>
        /// Sets Id value for the entity if <see cref="TEntityKey"/> is <see cref="Guid"/>.
        /// 用于新增实体.
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

        /// <summary>
        /// 根据属性名称Id尝试为UpdateDto赋值主键
        /// </summary>
        /// <param name="updateInput"></param>
        /// <param name="entity"></param>
        protected virtual void TrySetUpdateInputDtoIdByPropertyName(TUpdateInputDto updateInput, TEntity entity)
        {
            PropertyInfo pi = typeof(TUpdateInputDto).GetProperty("Id");
            if (pi == null || !pi.CanWrite) return;

            Type dtoKeyType = pi.PropertyType;
            Type entityKeyType = typeof(TEntityKey);

            if (dtoKeyType == typeof(TEntityKey))
            {
                pi.SetValue(updateInput, entity.Id, null);
                return;
            }

            //判断Dto里是否设置为了可空类型
            if (dtoKeyType.IsGenericType && dtoKeyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                //可空类型
                NullableConverter nullableConverter = new NullableConverter(entityKeyType);
                entityKeyType = nullableConverter.UnderlyingType;
                if (string.IsNullOrEmpty(entityKeyType.FullName)) return;

                var value = Convert.ChangeType(entity.Id, entityKeyType);
                if (value != null)
                {
                    pi.SetValue(updateInput, value, null);
                }
            }
        }
    }


    public class CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        : CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>,
            ICrudAppService<TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TGetDetailOutputDto, TGetListItemOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public CrudAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }


    public class CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TGetDetailOrListItemOutputDto>
        : CrudAppService<TEntity, TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TCreateOrUpdateInputDto, TGetDetailOrListItemOutputDto, TGetDetailOrListItemOutputDto>,
            ICrudAppService<TEntityKey, TGetListInputDto, TCreateOrUpdateInputDto, TGetDetailOrListItemOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public CrudAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }


    public class CrudAppService<TEntity, TEntityKey, TCreateOrUpdateDto, TDto>
        : CrudAppService<TEntity, TEntityKey, PageAndSortRequest, TCreateOrUpdateDto, TCreateOrUpdateDto, TDto, TDto>,
            ICrudAppService<TEntityKey, TCreateOrUpdateDto, TDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public CrudAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }

    public class CrudAppService<TEntity, TEntityKey, TDto>
        : CrudAppService<TEntity, TEntityKey, PageAndSortRequest, TDto, TDto, TDto, TDto>,
            ICrudAppService<TEntityKey, TDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public CrudAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
