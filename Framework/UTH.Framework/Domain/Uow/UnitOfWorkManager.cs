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

        /// <summary>
        /// 获取当前活动的工作单元(如果不存在，则为空)。
        /// </summary>
        public IUnitOfWorkActive Current
        {
            get { return _currentUnitOfWorkProvider.Current; }
        }

        /// <summary>
        /// 开始一个工作单元
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
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

            _currentUnitOfWorkProvider.Current = uow;

            uow.Begin(options);

            return uow;
        }
    }
}