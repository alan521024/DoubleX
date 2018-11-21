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


    public abstract class ApplicationCrudKeyService<TService, TEntity, DTO, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput,TKey> :
        ApplicationService,
        IApplicationCrudKeyService<DTO, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput,TKey>
        where TService : IDomainDefaultService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where DTO : IKeys<TKey>
        where TUpdateInput : IKeys<TKey>
        where TGetInput : IKeys<TKey>
        where TDeleteInput : IKeys<TKey>
        where TQueryInput : IKeys<TKey>
    {
        protected readonly IUnitOfWorkManager CurrentUnitOfWorkManager;
        protected readonly TService service;

        public ApplicationCrudKeyService(TService _service, IApplicationSession session, ICachingService caching) : base(session, caching)
        {
            service = _service;
            CurrentUnitOfWorkManager = EngineHelper.Resolve<IUnitOfWorkManager>();
        }

        public virtual DTO Get(TGetInput input)
        {
            return MapperToDto(service.Get(input.Id));
        }

        public virtual DTO Insert(TInsertInput input)
        {
            var entity = service.Insert(MapperToEntity(input));
            if (!entity.IsNull())
            {
                return MapperToDto(entity);
            }
            throw new DbxException(EnumCode.提示消息, Lang.sysXingZengShiBai);
        }

        public virtual DTO Update(TUpdateInput input)
        {
            var entity = service.Update(MapperToEntity(input));
            if (!entity.IsNull())
            {
                return MapperToDto(entity);
            }
            throw new DbxException(EnumCode.提示消息, Lang.sysXiuGaiShiBai);
        }

        public virtual int Delete(TDeleteInput input)
        {
            int rows = 0;
            if (!input.Ids.IsEmpty())
            {
                rows += service.Delete(input.Ids);
            }
            if (!input.Id.IsEmpty())
            {
                rows += service.Delete(input.Id);
            }
            return rows;
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

    public abstract class ApplicationCrudKeyService<TService, TEntity, DTO, TEditInput, TQueryInput, TKey> :
       ApplicationCrudKeyService<TService, TEntity, DTO, TEditInput, TEditInput, TQueryInput, TQueryInput, TQueryInput, TKey>,
       IApplicationCrudKeyService<DTO, TEditInput, TQueryInput, TKey>
       where TService : IDomainDefaultService<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
       where DTO : IKeys<TKey>
       where TEditInput : IKeys<TKey>
       where TQueryInput : IKeys<TKey>
    {
        public ApplicationCrudKeyService(TService _service, IApplicationSession session, ICachingService caching) : base(_service, session, caching)
        {
        }
    }

    public abstract class ApplicationCrudKeyService<TService, TEntity, DTO, TEditInput, TKey> :
       ApplicationCrudKeyService<TService, TEntity, DTO, TEditInput, DTO, TKey>,
       IApplicationCrudKeyService<DTO, TEditInput, TKey>
       where TService : IDomainDefaultService<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
       where DTO : IKeys<TKey>
       where TEditInput : IKeys<TKey>
    {
        public ApplicationCrudKeyService(TService _service, IApplicationSession session, ICachingService caching) : base(_service, session, caching)
        {
        }
    }

    public abstract class ApplicationCrudKeyService<TEntity, DTO, TEditInput, TKey> :
        ApplicationCrudKeyService<IDomainDefaultService<TEntity, TKey>, TEntity, DTO, TEditInput, TKey>,
        IApplicationCrudKeyService<DTO, TEditInput, TKey>
        where TEntity : class, IEntity<TKey>
        where DTO : IKeys<TKey>
        where TEditInput : IKeys<TKey>
    {

        public ApplicationCrudKeyService(IDomainDefaultService<TEntity, TKey> _service, IApplicationSession session, ICachingService caching) :
            base(_service, session, caching)
        {
        }
    }
}
