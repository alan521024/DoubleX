namespace UTH.Meeting.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    public class ApplicationDomainProfile : IDomainProfile
    {
        public void Configuration()
        {
            //DOT
        }

        public void Mapper(IMapperConfigurationExpression config)
        {
           
        }
    }
}
