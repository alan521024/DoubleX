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
    /// 工作单元提供程序(获取/设置工具单元)
    /// </summary>
    public class AsyncUnitOfWorkProvider : IUnitOfWorkProvider
    {
        /// <summary>
        /// 是否初始
        /// </summary>
        private bool _isInit = false;

        /// <summary>
        /// 程序标识
        /// </summary>
        public Guid id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(); }
            set
            {
                if (!_isInit)
                {
                    SetCurrentUow(null);
                    _isInit = true;
                }
                else
                {
                    SetCurrentUow(value);
                }
            }
        }

        private static readonly AsyncLocal<LocalUowWrapper> AsyncLocalUow = new AsyncLocal<LocalUowWrapper>();
        private static IUnitOfWork GetCurrentUow()
        {
            var uow = AsyncLocalUow.Value?.UnitOfWork;
            if (uow == null)
            {
                return null;
            }

            if (uow.IsDisposed)
            {
                AsyncLocalUow.Value = null;
                return null;
            }

            return uow;
        }
        private static void SetCurrentUow(IUnitOfWork value)
        {
            lock (AsyncLocalUow)
            {
                if (value == null)
                {
                    if (AsyncLocalUow.Value == null)
                    {
                        return;
                    }
                    if (AsyncLocalUow.Value.UnitOfWork?.GetOuter() == null)
                    {
                        AsyncLocalUow.Value.UnitOfWork = null;
                        AsyncLocalUow.Value = null;
                        return;
                    }

                    AsyncLocalUow.Value.UnitOfWork = AsyncLocalUow.Value.UnitOfWork.GetOuter();
                }
                else
                {
                    if (AsyncLocalUow.Value?.UnitOfWork == null)
                    {
                        if (AsyncLocalUow.Value != null)
                        {
                            AsyncLocalUow.Value.UnitOfWork = value;
                        }

                        AsyncLocalUow.Value = new LocalUowWrapper(value);
                        return;
                    }

                    value.SetOuter(AsyncLocalUow.Value.UnitOfWork);
                    AsyncLocalUow.Value.UnitOfWork = value;
                }
            }
        }


        private class LocalUowWrapper
        {
            public IUnitOfWork UnitOfWork { get; set; }

            public LocalUowWrapper(IUnitOfWork unitOfWork)
            {
                UnitOfWork = unitOfWork;
            }
        }
    }

}
