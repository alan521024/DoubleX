namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 领域配置（领域配置单独提出，不依赖模块配置，引入UTH.Module就可以使）
    /// eg:DTO的注入配置
    /// </summary>
    public interface IDomainProfile
    {
        /// <summary>
        /// 领域配置
        /// </summary>
        void Configuration();

        /// <summary>
        /// 对象映射
        /// </summary>
        void Mapper(IMapperConfigurationExpression config);

    }
}
