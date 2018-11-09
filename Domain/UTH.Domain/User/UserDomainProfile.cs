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
    /// <summary>
    /// 领域配置文件
    /// </summary>
    public class UserDomainProfile : IDomainProfile
    {
        /// <summary>
        /// 领域配置
        /// </summary>
        public void Configuration()
        {
            EngineHelper.RegisterType(typeof(IValidator<AccountEditInput>), typeof(AccountEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<SignInInput>), typeof(SignInInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<SignOutInput>), typeof(SignOutInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<SignRefreshInput>), typeof(SignRefreshInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<RegistInput>), typeof(RegistInputValidator));
            EngineHelper.RegisterType(typeof(IValidator<FindPwdInput>), typeof(FindPwdInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<MemberEditInput>), typeof(MemberEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<OrganizeEditInput>), typeof(OrganizeEditInputValidator));

            EngineHelper.RegisterType(typeof(IValidator<EmployeEditInput>), typeof(EmployeEditInputValidator));
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        public void Mapper(IMapperConfigurationExpression config)
        {
            config.CreateMap<AccountEntity, AccountDTO>();
            config.CreateMap<AccountDTO, AccountEntity>();
            config.CreateMap<AccountEditInput, AccountEntity>();

            config.CreateMap<SignInInput, AccountEntity>();
            config.CreateMap<AccountEntity, SignInOutput>();

            config.CreateMap<RegistInput, AccountEntity>();
            config.CreateMap<AccountEntity, RegistOutput>();

            config.CreateMap<MemberEntity, MemberDTO>();
            config.CreateMap<MemberDTO, MemberEntity>();
            config.CreateMap<MemberEditInput, MemberEntity>();

            config.CreateMap<OrganizeEntity, OrganizeDTO>();
            config.CreateMap<OrganizeDTO, OrganizeEntity>();
            config.CreateMap<OrganizeEditInput, OrganizeEntity>();

            config.CreateMap<EmployeEntity, EmployeDTO>();
            config.CreateMap<EmployeDTO, EmployeEntity>();
            config.CreateMap<EmployeEditInput, EmployeEntity>();
        }

    }
}
