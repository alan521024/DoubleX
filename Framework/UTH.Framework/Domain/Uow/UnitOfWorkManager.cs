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
    /// 工作单元管理
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        IUnitOfWork current;

        public UnitOfWorkManager(IUnitOfWork _uow)
        {
            current = _uow;
        }

        public IUnitOfWork Begin()
        {
            current.CheckNull();

            current.Begin();

            return current;
        }
    }
}