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
        IRepository repository;

        public override dynamic Context { get; set; }

        protected override void BeginUow()
        {
            Context = SqlSugarHelper.GetContext(Options.Connection ?? EngineHelper.Configuration.Store.Database);
            repository = EngineHelper.Resolve<IRepository>();
            repository.BeginTran();
        }

        public override void SaveChanges()
        {
        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void CompleteUow()
        {
            repository.CommitTran();
        }

        protected override Task CompleteUowAsync()
        {
            repository.CommitTran();
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {
            repository.RollbackTran();
        }
    }

}