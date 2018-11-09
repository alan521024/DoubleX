namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using SqlSugar;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 工作单元
    /// </summary>
    public class SqlSugarUnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        protected override void BeginUow()
        {

        }

        //IRepository repository;

        //public UnitOfWorkSqlSugar(IRepository repository)
        //{
        //    this.repository = repository;
        //}

        //public override void Repository(IRepository repository)
        //{
        //    this.repository = repository;
        //}

        //public override void SaveChanges()
        //{
        //    CompleteUow();
        //}

        //public override IDomainDefaultService<TEntity> Resolve<TEntity>()
        //{
        //    IDomainDefaultService<TEntity> IService = default(IDomainDefaultService<TEntity>);
        //    var iRep = EngineHelper.Resolve<IRepository<TEntity>>(new KeyValueModel<string, object>("context", repository.GetContext()));
        //    IService = EngineHelper.Resolve<IDomainDefaultService<TEntity>>(new KeyValueModel<string, object>("repository", iRep));
        //    return IService;
        //}

        //public override TService Resolve<TEntity, TService>()
        //{
        //    TService IService = default(TService);
        //    var iRep = EngineHelper.Resolve<IRepository<TEntity>>(new KeyValueModel<string, object>("context", repository.GetContext()));
        //    IService = EngineHelper.Resolve<TService>(new KeyValueModel<string, object>("repository", iRep));
        //    return IService;
        //}

        //public override TService Resolve<TEntity, TService, TRepository>()
        //{
        //    TService IService = default(TService);
        //    var iRep = EngineHelper.Resolve<TRepository>(new KeyValueModel<string, object>("context", repository.GetContext()));
        //    IService = EngineHelper.Resolve<TService>(new KeyValueModel<string, object>("repository", iRep));
        //    return IService;
        //}


        //protected override void BeginUow()
        //{
        //    repository.CheckNull();
        //    repository.BeginTran();
        //}

        //protected override void CompleteUow()
        //{
        //    try
        //    {
        //        repository.CheckNull();
        //        repository.CommitTran();
        //    }
        //    catch (Exception ex)
        //    {
        //        repository.RollbackTran();
        //        throw ex;
        //    }
        //}
        public override void SaveChanges()
        {
        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void CompleteUow()
        {
        }

        protected override Task CompleteUowAsync()
        {
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {
        }
    }

}