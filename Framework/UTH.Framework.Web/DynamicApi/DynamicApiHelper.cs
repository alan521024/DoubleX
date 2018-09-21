namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using System.Reflection;

    /// <summary>
    /// 动态Api辅助操作
    /// </summary>
    public static class DynamicApiHelper
    {
        #region 动态Api 根据配置 创建 组件/控制器/Action 

        public static List<DynamicApiComponent> ApiComponents
        {
            get
            {
                if (_apiComponents == null)
                {
                    _apiComponents = GetModuleDynamicApis();
                }
                return _apiComponents;
            }
        }
        private static List<DynamicApiComponent> _apiComponents;

        public static List<DynamicApiComponent> CreateApiComponents(DynamicApiConfigModel root, List<DynamicApiComponent> settings)
        {
            root.CheckNull();

            var list = new List<DynamicApiComponent>();

            if (root.Enable.IsEmpty())
            {
                root.Enable = "true";
            }
            if (root.ResultWrapper.IsEmpty())
            {
                root.ResultWrapper = "true";
            }
            if (root.ExceptionWrapper.IsEmpty())
            {
                root.ExceptionWrapper = "true";
            }

            if (settings.IsNull())
            {
                return list;
            }

            foreach (var setting in settings)
            {
                if (setting == null)
                    continue;

                //module name not empty
                setting.Name.CheckEmpty();

                if (!BoolHelper.Get(setting.Enable, defaultValue: root.Enable))
                {
                    continue;
                }

                var module = EngineHelper.Component.Where(x => x.IsBusiness && x.Namespace == setting.TypeName).FirstOrDefault();
                if (module.IsNull())
                {
                    continue;
                }
                if (module.Assemblies.IsNull())
                {
                    continue;
                }

                var services = EngineHelper.TypeFinder.FindClassesOfType<IApplicationService>(new List<Assembly>() { module.Assemblies });
                if (services == null)
                {
                    services = new List<Type>();
                }

                var component = new DynamicApiComponent(setting, root);
                component.TypeName = setting.TypeName;
                component.Name = setting.Name;
                component.Assemblies = module.Assemblies;
                component.Controllers = CreateApiController(component, setting.Controllers, services);

                list.Add(component);
            }

            return list;
        }

        public static List<DynamicApiControls> CreateApiController(DynamicApiComponent component, List<DynamicApiControls> settings, IEnumerable<Type> services)
        {
            List<DynamicApiControls> list = new List<DynamicApiControls>();

            if (component.IsNull())
            {
                return list;
            }

            if (!BoolHelper.Get(component.Enable))
            {
                return list;
            }

            if (services.IsEmpty())
            {
                return list;
            }

            if (settings.IsEmpty())
            {
                settings = new List<DynamicApiControls>();
            }

            foreach (var service in services)
            {
                var setting = settings.Where(x => x.TypeName == service.FullName).FirstOrDefault();

                var controller = new DynamicApiControls(setting, component);
                controller.TypeName = service.FullName;  //setting中未找到 service 配置，new 新的实例并设置TypeName
                controller.Name = (setting.IsNull() || (!setting.IsNull() && setting.Name.IsEmpty())) ? service.Name : setting.Name;
                controller.Actions = new List<DynamicApiAction>();

                if (!BoolHelper.Get(controller.Enable))
                    continue;

                controller.Actions = CreateApiActions(controller, setting == null ? new List<DynamicApiAction>() : setting.Actions, service.GetMethods());

                list.Add(controller);
            }

            return list;
        }

        public static List<DynamicApiAction> CreateApiActions(DynamicApiControls controller, List<DynamicApiAction> settings, MethodInfo[] methods)
        {
            List<DynamicApiAction> list = new List<DynamicApiAction>();

            if (controller.IsNull())
            {
                return list;
            }

            if (!BoolHelper.Get(controller.Enable))
            {
                return list;
            }

            if (methods.IsEmpty())
            {
                return list;
            }

            if (settings.IsEmpty())
            {
                settings = new List<DynamicApiAction>();
            }

            foreach (var method in methods)
            {
                //method 不能进行api构造的 跳过

                var setting = settings.Where(x => x.TypeName == method.Name).FirstOrDefault();

                var action = new DynamicApiAction(setting, controller);
                action.TypeName = method.Name;  //setting中未找到 action 配置，new 新的实例并设置TypeName
                action.Name = (setting.IsNull() || (!setting.IsNull() && setting.Name.IsEmpty())) ? action.TypeName : setting.Name;

                if (!BoolHelper.Get(action.Enable))
                    continue;

                list.Add(action);
            }

            return list;
        }

        public static List<DynamicApiComponent> GetModuleDynamicApis()
        {
            var config = DynamicApiConfigObjService.Instance;
            config.CheckNull();
            return CreateApiComponents(config, config.Components);
        }

        public static List<AssemblyPart> GetModuleAssemblyParts()
        {
            var list = new List<AssemblyPart>();
            foreach (var item in GetModuleDynamicApis())
            {
                list.Add(new AssemblyPart(item.Assemblies));
            }
            return list;
        }

        #endregion

        #region 辅助操作

        public static bool IsServiceController(Type controllerType, bool checkEnable = true)
        {
            return !GetServiceController(controllerType, checkEnable: true).IsNull();
        }

        public static bool IsServiceAction(Type controllerType, MethodInfo method, bool checkEnable = true)
        {
            var controller = GetServiceController(controllerType, checkEnable: true);
            return IsServiceAction(controller, method, checkEnable: true);

        }

        public static bool IsServiceAction(DynamicApiControls controller, MethodInfo method, bool checkEnable = true)
        {
            return !GetServiceAction(controller, method, checkEnable: true).IsNull();
        }


        public static DynamicApiComponent GetServiceComponent(Type controllerType, bool checkEnable = true)
        {
            if (controllerType.IsNull())
                return null;

            if (controllerType.Namespace.IsEmpty())
                return null;

            var component = ApiComponents.Where(x => x.TypeName == controllerType.Namespace).FirstOrDefault();
            if (component.IsNull())
                return null;

            if (checkEnable && !BoolHelper.Get(component.Enable))
                return null;

            return component;
        }

        public static DynamicApiControls GetServiceController(Type controllerType, bool checkEnable = true)
        {
            var component = GetServiceComponent(controllerType, checkEnable: checkEnable);
            if (component.IsNull())
                return null;

            var controller = component.Controllers.Where(x => x.TypeName == controllerType.FullName).FirstOrDefault();
            if (controller.IsNull())
                return null;

            if (checkEnable && !BoolHelper.Get(controller.Enable))
                return null;

            return controller;
        }

        public static DynamicApiAction GetServiceAction(Type controllerType, MethodInfo method, bool checkEnable = true)
        {
            var controller = GetServiceController(controllerType, checkEnable: true);
            return GetServiceAction(controller, method, checkEnable: true);
        }

        public static DynamicApiAction GetServiceAction(DynamicApiControls controls, MethodInfo method, bool checkEnable = true)
        {
            if (controls.IsNull() || method.IsNull())
                return null;

            var action = controls.Actions.Where(x => x.Name == method.Name).FirstOrDefault();

            if (action.IsNull())
                return null;

            if (action.Name == "DeleteId" || action.Name == "DeleteIds")
                return null;

            if (checkEnable && !BoolHelper.Get(action.Enable))
                return null;

            return action;
        }

        #endregion
    }
}
