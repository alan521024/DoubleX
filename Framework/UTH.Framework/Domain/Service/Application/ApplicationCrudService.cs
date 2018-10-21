namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    public abstract class ApplicationCrudService<TEntity, DTO, TEditInput> :
        ApplicationCrudService<IDomainDefaultService<TEntity>, TEntity, DTO, TEditInput>,
        IApplicationCrudService<DTO, TEditInput>
        where TEntity : class, IEntity
        where DTO : IKeys
        where TEditInput : IKeys
    {
        public ApplicationCrudService(IDomainDefaultService<TEntity> _service, IApplicationSession session, ICachingService caching) : base(_service, session, caching)
        {
        }
    }

    public abstract class ApplicationCrudService<TService, TEntity, DTO, TEditInput> :
       ApplicationCrudService<TService, TEntity, DTO, TEditInput, DTO>,
       IApplicationCrudService<DTO, TEditInput>
       where TService : IDomainDefaultService<TEntity>
       where TEntity : class, IEntity
       where DTO : IKeys
       where TEditInput : IKeys
    {
        public ApplicationCrudService(TService _service, IApplicationSession session, ICachingService caching) : base(_service, session, caching)
        {
        }
    }

    public abstract class ApplicationCrudService<TService, TEntity, DTO, TEditInput, TQueryInput> :
       ApplicationCrudService<TService, TEntity, DTO, TEditInput, TEditInput, TQueryInput, TQueryInput, TQueryInput>,
       IApplicationCrudService<DTO, TEditInput, TQueryInput>
       where TService : IDomainDefaultService<TEntity>
       where TEntity : class, IEntity
       where DTO : IKeys
       where TEditInput : IKeys
       where TQueryInput : IKeys
    {
        public ApplicationCrudService(TService _service, IApplicationSession session, ICachingService caching) : base(_service, session, caching)
        {
        }
    }

    public abstract class ApplicationCrudService<TService, TEntity, DTO, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput> :
        ApplicationService,
        IApplicationCrudService<DTO, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput>
        where TService : IDomainDefaultService<TEntity>
        where TEntity : class, IEntity
        where DTO : IKeys
        where TUpdateInput : IKeys
        where TGetInput : IKeys
        where TDeleteInput : IKeys
        where TQueryInput : IKeys
    {
        protected readonly TService service;

        public ApplicationCrudService(TService _service, IApplicationSession session, ICachingService caching) : base(session, caching)
        {
            service = _service;
        }

        public virtual DTO Get(TGetInput input)
        {
            return MapperToDto(service.Get(input.Id));
        }

        public virtual DTO Insert(TInsertInput input)
        {
            var entity = MapperToEntity(input);
            if (service.Insert(entity) > 0)
            {
                return MapperToDto(entity);
            }
            throw new DbxException(EnumCode.提示消息, Lang.sysXingZengShiBai);
        }

        public virtual DTO Update(TUpdateInput input)
        {
            var entity = MapperToEntity(input);
            if (service.Update(entity) > 0)
            {
                return MapperToDto(entity);
            }
            throw new DbxException(EnumCode.提示消息, Lang.sysXiuGaiShiBai);
        }

        public virtual int Delete(TDeleteInput input)
        {
            input = DeleteBefore(input);

            if (!input.Ids.IsEmpty())
            {
                return service.Delete(input.Ids);
            }
            else
            {
                return service.Delete(input.Id);
            }
        }

        public virtual List<DTO> Query(QueryInput<TQueryInput> input)
        {
            var where = InputToWhere(input);
            var sorting = InputToSorting(input);
            var entitys = service.Find(top: input.Size, where: where, sorting: sorting);
            return MapperToDtos(entitys);
        }

        public virtual PagingModel<DTO> Paging(QueryInput<TQueryInput> input)
        {
            var where = InputToWhere(input);
            var sorting = InputToSorting(input);
            var query = service.Paging(input.Page, input.Size, where, sorting);

            PagingModel<DTO> result = new PagingModel<DTO>()
            {
                Total = query.Total,
                Rows = MapperToDtos(query.Rows)
            };

            return result;
        }



        protected virtual Expression<Func<TEntity, bool>> InputToWhere(QueryInput<TQueryInput> input)
        {
            return null;
        }

        protected virtual List<KeyValueModel> InputToSorting(QueryInput<TQueryInput> input)
        {
            return input.Sorting;
        }

        protected virtual TInsertInput InsertBefore(TInsertInput input) { return input; }

        protected virtual DTO InsertAfter(DTO output) { return output; }


        protected virtual TEntity UpdateBefore(TUpdateInput input, TEntity entity) { return entity; }

        protected virtual DTO UpdateAfter(DTO output) { return output; }

        protected virtual TDeleteInput DeleteBefore(TDeleteInput input) { return input; }

        protected virtual DTO MapperToDto<T>(T source)
        {
            return EngineHelper.Map<DTO>(source);
        }

        protected virtual List<DTO> MapperToDtos<T>(IEnumerable<T> source)
        {
            return EngineHelper.Map<List<DTO>>(source);
        }

        protected virtual TEntity MapperToEntity<T>(T source)
        {
            return EngineHelper.Map<TEntity>(source);
        }
    }
}
