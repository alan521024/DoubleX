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
            config.CreateMap<AppEntity, AppDTO>();
            config.CreateMap<AppDTO, AppEntity>();
            config.CreateMap<AppEditInput, AppEntity>();
            config.CreateMap<AppDTO, ApplicationModel>();

            config.CreateMap<AppVersionEntity, AppVersionDTO>();
            config.CreateMap<AppVersionDTO, AppVersionEntity>();
            config.CreateMap<AppVersionEditInput, AppVersionEntity>();
            config.CreateMap<AppVersionDTO, ApplicationVersion>();

            config.CreateMap<DictionaryEntity, DictionaryDTO>();
            config.CreateMap<DictionaryDTO, DictionaryEntity>();
            config.CreateMap<DictionaryEditInput, DictionaryEntity>();

            config.CreateMap<AssetsEntity, AssetsDTO>();
            config.CreateMap<AssetsDTO, AssetsEntity>();
            config.CreateMap<AssetsEditInput, AssetsEntity>();

            config.CreateMap<OperateEntity, OperateDTO>();
            config.CreateMap<OperateDTO, OperateEntity>();
            config.CreateMap<OperateEditInput, OperateEntity>();

            config.CreateMap<NavigationEntity, NavigationDTO>();
            config.CreateMap<NavigationDTO, NavigationEntity>();
            config.CreateMap<NavigationEditInput, NavigationEntity>();
        }
    }
}
