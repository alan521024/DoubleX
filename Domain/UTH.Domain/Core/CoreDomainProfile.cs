using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Castle.DynamicProxy;
using AutoMapper;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Domain
{
    public class CoreDomainProfile : IDomainProfile
    {
        public void Configuration()
        {
            //DTO
        }

        public void Mapper(IMapperConfigurationExpression config)
        {
            //..
        }
    }
}
