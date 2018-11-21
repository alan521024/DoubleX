namespace UTH.Module.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Reflection;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 用户组件配置
    /// </summary>
    public class UserConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Module.User"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return "User"; } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get { return "用户组件"; } }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assemblies { get { return this.GetType().Assembly; } }

        /// <summary>
        /// 是否业务组件
        /// </summary>
        public bool IsBusiness { get; set; } = true;

        /// <summary>
        /// 是否插件组件
        /// </summary>
        public bool IsPlug { get; set; } = false;

        /// <summary>
        /// 组件安装
        /// </summary>
        public void Install()
        {
            //account,sign,regist
            EngineHelper.RegisterType<IAccountRepository, AccountRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IAccountDomainService, AccountDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IAccountApplication, AccountApplication>(DomainHelper.ApplicationIoc);

            //member
            EngineHelper.RegisterType<IMemberRepository, MemberRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IMemberDomainService, MemberDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IMemberApplication, MemberApplication>(DomainHelper.ApplicationIoc);

            //organize
            EngineHelper.RegisterType<IOrganizeRepository, OrganizeRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IOrganizeDomainService, OrganizeDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IOrganizeApplication, OrganizeApplication>(DomainHelper.ApplicationIoc);

            //employe
            EngineHelper.RegisterType<IEmployeRepository, EmployeRepository>(DomainHelper.RepositoryIoc);
            EngineHelper.RegisterType<IEmployeDomainService, EmployeDomainService>(DomainHelper.ServiceIoc);
            EngineHelper.RegisterType<IEmployeApplication, EmployeApplication>(DomainHelper.ApplicationIoc);
        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Uninstall()
        {

        }
    }
}
