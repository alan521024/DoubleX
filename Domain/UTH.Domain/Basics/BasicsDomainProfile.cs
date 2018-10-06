using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Domain
{
    public class BasicsDomainProfile : IDomainProfile
    {
        public void Configuration()
        {
            EngineHelper.RegisterType(typeof(IValidator<AppEditInput>), typeof(AppEditInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<AppVersionEditInput>), typeof(AppVersionEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<AssetsEditInput>), typeof(AssetsEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<DictionaryEditInput>), typeof(DictionaryEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<OperateEditInput>), typeof(OperateEditInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<NavigationEditInput>), typeof(NavigationEditInputValidator));
        }

        public void Mapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<AppBase, AppEditInput>();
            config.CreateMap<AppEditInput, AppEntity>();
            config.CreateMap<AppEntity, AppOutput>();
            config.CreateMap<AppOutput, ApplicationModel>();
            config.CreateMap<AppVersionBase, AppVersionEditInput>();
            config.CreateMap<AppVersionEditInput, AppVersionEntity>();
            config.CreateMap<AppVersionEntity, AppVersionOutput>();
            config.CreateMap<AppVersionOutput, ApplicationVersion>();

            config.CreateMap<AssetsBase, AssetsEditInput>();
            config.CreateMap<AssetsEditInput, AssetsEntity>();
            config.CreateMap<AssetsEntity, AssetsOutput>();

            config.CreateMap<DictionaryBase, DictionaryEditInput>();
            config.CreateMap<DictionaryEditInput, DictionaryEntity>();
            config.CreateMap<DictionaryEntity, DictionaryOutput>();

            config.CreateMap<AssetsBase, AssetsEditInput>();
            config.CreateMap<AssetsEditInput, AssetsEntity>();
            config.CreateMap<AssetsEntity, AssetsOutput>();

            config.CreateMap<OperateBase, OperateEditInput>();
            config.CreateMap<OperateEditInput, OperateEntity>();
            config.CreateMap<OperateEntity, OperateOutput>();

            config.CreateMap<NavigationBase, NavigationEditInput>();
            config.CreateMap<NavigationEditInput, NavigationEntity>();
            config.CreateMap<NavigationEntity, NavigationOutput>();
        }
    }
}
