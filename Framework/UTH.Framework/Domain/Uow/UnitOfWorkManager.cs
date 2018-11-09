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
        private readonly IUnitOfWorkProvider _currentUnitOfWorkProvider;

        public UnitOfWorkManager(IUnitOfWorkProvider _provider)
        {
            _currentUnitOfWorkProvider = _provider;
        }

        public IUnitOfWorkActive Current
        {
            get { return _currentUnitOfWorkProvider.Current; }
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options = null)
        {
            options = options ?? new UnitOfWorkOptions();

            var outerUow = _currentUnitOfWorkProvider.Current;

            var uow = EngineHelper.Resolve<IUnitOfWork>();

            uow.Completed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Failed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Disposed += (sender, args) =>
            {
                //_iocResolver.Release(uow);
                _currentUnitOfWorkProvider.Current = null;
            };

            if (outerUow != null)
            {
                //options.FillOuterUowFiltersForNonProvidedOptions(outerUow.Filters.ToList());
            }

            uow.Begin(options);

            _currentUnitOfWorkProvider.Current = uow;

            return uow;
        }
    }
}