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
    public class UserDomainProfile : IDomainProfile
    {
        public void Mapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<AccountDto, AccountEditInput>();
            config.CreateMap<AccountEditInput, AccountEntity>();
            config.CreateMap<AccountEntity, AccountOutput>();

            config.CreateMap<SignInInput, AccountEntity>();
            config.CreateMap<AccountEntity, SignInOutput>();
            config.CreateMap<AccountEntity, RegistOutput>();

            config.CreateMap<OrganizeDto, OrganizeEditInput>();
            config.CreateMap<OrganizeEditInput, OrganizeEntity>();
            config.CreateMap<OrganizeEntity, OrganizeOutput>();

            config.CreateMap<EmployeDto, EmployeEditInput>();
            config.CreateMap<EmployeEditInput, EmployeEntity>();
            config.CreateMap<EmployeEntity, EmployeOutput>();
        }

        public void Configuration()
        {
            
            EngineHelper.RegisterType(typeof(IValidator<AccountEditInput>), typeof(AccountEditInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<SignInInput>), typeof(SignInInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<SignOutInput>), typeof(SignOutInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<SignRefreshInput>), typeof(SignRefreshInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<RegistInput>), typeof(RegistInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<FindPwdInput>), typeof(FindPwdInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<OrganizeEditInput>), typeof(OrganizeEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<EmployeEditInput>), typeof(EmployeEditInputValidator));
        }

    }
}
