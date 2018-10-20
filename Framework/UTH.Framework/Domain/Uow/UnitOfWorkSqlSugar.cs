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

    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWorkSqlSugar : UnitOfWorkBase, IUnitOfWork
    {
        IRepository repository;

        public UnitOfWorkSqlSugar(IRepository repository)
        {
            this.repository = repository;
        }

        public override void SaveChanges()
        {
            CompleteUow();
        }

        protected override void BeginUow()
        {
            repository.CheckNull();
            repository.BeginTran();
        }

        protected override void CompleteUow()
        {
            try
            {
                repository.CheckNull();
                repository.CommitTran();
            }
            catch (Exception ex)
            {
                repository.RollbackTran();
                throw ex;
            }
        }
    }
}