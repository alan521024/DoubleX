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
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public UnitOfWorkBase()
        {
        }

        public void Begin()
        {
            BeginUow();
        }

        public abstract void SaveChanges();

        protected virtual void BeginUow()
        {

        }

        protected abstract void CompleteUow();


        public bool IsDisposed { get; }

        public void Dispose()
        {
        }

    }
}