namespace UTH.Plug.Notification
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

    /// <summary>
    /// 通知消息组件配置
    /// </summary>
    public class NotificationPlugConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// 组件名称(命名空间)
        /// </summary>
        public string Namespace { get { return "UTH.Plug.Notification"; } }

        /// <summary>
        /// 组件标识
        /// </summary>
        public string Name { get { return "NotificationPlug"; } }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get { return "通知插件"; } }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assemblies { get { return this.GetType().Assembly; } }

        /// <summary>
        /// 是否业务组件
        /// </summary>
        public bool IsBusiness { get; set; } = false;

        /// <summary>
        /// 是否插件组件
        /// </summary>
        public bool IsPlug { get; set; } = true;

        /// <summary>
        /// 组件安装
        /// </summary>
        public void Install()
        {
            IConfigObjService<SmsConfigModel> _config = new DefaultConfigObjService<SmsConfigModel>();
            var enableName = _config.Load().Name;

            EngineHelper.RegisterType<ISmsService, LuoSiMaoSmsService>();

            //默认短信
            //if (StringHelper.IsEqual(enableName, EnumSmsPlatform.Aliyun.GetName()))
            //{
            //    EngineHelper.RegisterType<ISmsService, AliyunSmsService>();
            //}
            //else if (StringHelper.IsEqual(enableName, EnumSmsPlatform.LuoSiMao.GetName()))
            //{
            //    EngineHelper.RegisterType<ISmsService, LuoSiMaoSmsService>();
            //}
            //else
            //{
            //    EngineHelper.RegisterType<ISmsService, LuoSiMaoSmsService>();
            //}
        }

        /// <summary>
        /// 组件卸载
        /// </summary>
        public void Shutdown()
        {

        }
    }
}
