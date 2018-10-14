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

    public abstract class ApplicationCrudService<TEntity, TDTOInput> : ApplicationCrudService<TEntity, TDTOInput, TDTOInput>, IApplicationCrudService<TDTOInput>
       where TEntity : class, IEntity
       where TDTOInput : IEntityKeys
    {
        public ApplicationCrudService(IRepository<TEntity, Guid> repository) : base(repository)
        {
        }
    }

    public abstract class ApplicationCrudService<TEntity, TDTOInput, TEditInput> : ApplicationCrudService<TEntity, TDTOInput, TEditInput, TDTOInput>,
       IApplicationCrudService<TDTOInput, TEditInput>
       where TEntity : class, IEntity
       where TDTOInput : IEntityKeys
       where TEditInput : IEntityKeys
    {
        public ApplicationCrudService(IRepository<TEntity, Guid> repository) : base(repository)
        {
        }
    }

    public abstract class ApplicationCrudService<TEntity, TDTOInput, TEditInput, TQueryInput> :
       ApplicationCrudService<TEntity, TDTOInput, TEditInput, TEditInput, TQueryInput, TQueryInput, TQueryInput, Guid>,
       IApplicationCrudService<TDTOInput, TEditInput, TQueryInput>
       where TEntity : class, IEntity
       where TDTOInput : IEntityKeys
       where TEditInput : IEntityKeys
       where TQueryInput : IEntityKeys
    {
        public ApplicationCrudService(IRepository<TEntity, Guid> repository) : base(repository)
        {
        }
    }

    public abstract class ApplicationCrudService<TEntity, TDTOInput, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput, TPrimaryKey> :
    BaseService,
    IApplicationCrudService<TDTOInput, TInsertInput, TUpdateInput, TGetInput, TDeleteInput, TQueryInput, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    where TDTOInput : IEntityKeys<TPrimaryKey>
    where TUpdateInput : IEntityKeys<TPrimaryKey>
    where TGetInput : IEntityKeys<TPrimaryKey>
    where TDeleteInput : IEntityKeys<TPrimaryKey>
    where TQueryInput : IEntityKeys<TPrimaryKey>
    {
        protected readonly IRepository<TEntity, TPrimaryKey> Repository;

        public ApplicationCrudService(IRepository<TEntity, TPrimaryKey> repository)
        {
            Repository = repository;
        }

        public virtual TDTOInput Get(TGetInput input)
        {
            return MapperToDto(Repository.Find(input.Id));
        }

        public virtual TDTOInput Insert(TInsertInput input)
        {
            var entity = MapperToEntity(input);
            var output = default(TDTOInput);

            if (Repository.Insert(entity) > 0)
            {
                output = MapperToDto(entity);
            }

            return output;
        }

        public virtual TDTOInput Update(TUpdateInput input)
        {
            var entity = MapperToEntity(input);
            var output = default(TDTOInput);

            if (Repository.Update(entity) > 0)
            {
                output = MapperToDto(entity);
            }

            return output;
        }

        public virtual int Delete(TDeleteInput input)
        {
            if (!input.Ids.IsEmpty())
            {
                return Repository.Delete(input.Ids);
            }
            else
            {
                return Repository.Delete(input.Id);
            }
        }

        public virtual List<TDTOInput> Query(QueryInput<TQueryInput> input)
        {
            var where = InputToWhere(input);
            var sorting = InputToSorting(input);
            var entitys = Repository.Find(top: input.Size, where: where, sorting: sorting);
            return MapperToDtos(entitys);
        }

        public virtual PagingModel<TDTOInput> Paging(QueryInput<TQueryInput> input)
        {
            var where = InputToWhere(input);
            var sorting = InputToSorting(input);

            PagingModel<TDTOInput> result = new PagingModel<TDTOInput>();
            var total = 0;
            var rows = Repository.Paging(input.Page, input.Size, where, sorting, ref total);
            result.Rows = MapperToDtos(rows);
            result.Total = total;
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

        protected virtual TDTOInput MapperToDto<T>(T source)
        {
            return EngineHelper.Map<TDTOInput>(source);
        }

        protected virtual List<TDTOInput> MapperToDtos<T>(IEnumerable<T> source)
        {
            return EngineHelper.Map<List<TDTOInput>>(source);
        }

        protected virtual TEntity MapperToEntity<T>(T source)
        {
            return EngineHelper.Map<TEntity>(source);
        }
    }
}
