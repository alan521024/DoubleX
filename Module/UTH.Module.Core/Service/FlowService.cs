namespace UTH.Module.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;

    /// <summary>
    /// 流程领域服务
    /// </summary>
    public class FlowService : DomainService, IFlowService
    {
        public FlowService(IApplicationSession session, ICachingService caching) : base(session, caching)
        {
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public FlowModel Create(FlowEditInput input)
        {
            input.CheckNull();

            var model = new FlowModel();
            model.Id = Guid.NewGuid();
            model.Total = 10;
            model.Current = 5;

            var key = FormatKey(model.Id);
            Caching.Set(key, model, TimeSpan.FromMilliseconds(input.Expire));

            return model;
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public FlowModel Progress(Guid flowId)
        {
            flowId.CheckEmpty();
            return Caching.Get<FlowModel>(FormatKey(flowId));
        }

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="model"></param>
        public void SetProgress(FlowModel model)
        {
            Caching.Set<FlowModel>(FormatKey(model.Id), model);
        }



        /// <summary>
        /// 格式化缓存KEY
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string FormatKey(Guid id)
        {
            return $"flow_{id}";
        }
    }
}
