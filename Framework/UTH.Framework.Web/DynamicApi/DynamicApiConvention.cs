namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.ComponentModel;

    public class DynamicApiConvention : IApplicationModelConvention
    {
        public DynamicApiConvention(IServiceCollection services)
        {
        }

        public void Apply(Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel application)
        {
            foreach (var appController in application.Controllers)
            {
                var component = DynamicApiHelper.GetServiceComponent(appController.ControllerType);
                if (component.IsNull())
                    continue;

                var controller = DynamicApiHelper.GetServiceController(appController.ControllerType);
                if (controller.IsNull())
                    continue;


                ConfigureModule(appController, component);

                ConfigureController(appController, controller);

                if (appController.Actions.IsEmpty())
                    continue;

                for (var i = 0; i < appController.Actions.Count; i++)
                {
                    var action = DynamicApiHelper.GetServiceAction(appController.ControllerType,
                        appController.Actions[i].ActionMethod);

                    if (action.IsNull())
                        continue;

                    ConfigureAction(component, controller, action, appController.Actions[i]);
                }
            }
        }

        /// <summary>
        /// route value module 模块配置
        /// </summary>
        private void ConfigureModule(ControllerModel controller, DynamicApiComponent component)
        {
            if (controller.IsNull() || component.IsNull())
                return;

            if (controller.RouteValues.ContainsKey("module"))
                return;

            controller.RouteValues["module"] = component.Name;
        }

        /// <summary>
        /// Controller 控制器配置
        /// </summary>
        private void ConfigureController(ControllerModel controller, DynamicApiControls controls)
        {
            if (controller.IsNull() || controls.IsNull())
            {
                return;
            }

            controller.ControllerName = GetServiceRemovePostfix(controls.Name);
        }

        /// <summary>
        /// Action 操作配置
        /// </summary>
        private void ConfigureAction(DynamicApiComponent component, DynamicApiControls controls, DynamicApiAction apiAction, ActionModel action)
        {
            if (component.IsNull() || controls.IsNull() || apiAction.IsNull() || action.IsEmpty())
            {
                return;
            }

            //api explorer
            if (!action.ApiExplorer.IsNull())
            {
                action.ApiExplorer.IsVisible = true;
            }

            //selector
            RemoveEmptySelectors(action.Selectors);
            if (!action.Selectors.Any())
            {
                var selectorModel = new SelectorModel();

                //route
                selectorModel.AttributeRouteModel = CreateRoute(apiAction.Route, component.Name, controls.Name, apiAction.Name);

                //http
                if (apiAction.Verb != EnumHttpVerb.DEFAULT)
                {
                    selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { apiAction.Verb.ToString() }));
                }

                //
                action.Selectors.Add(selectorModel);
            }
            else
            {
                foreach (var selectorModel in action.Selectors)
                {
                    if (selectorModel.AttributeRouteModel == null)
                    {
                        selectorModel.AttributeRouteModel = CreateRoute(apiAction.Route, component.Name, controls.Name, apiAction.Name);
                    }
                }
            }

            //params
            ConfigureActionParameters(apiAction, action);


            //authorize
            if (BoolHelper.Get(apiAction.Authorize))
            {
                var authAttribute = new AuthorizeAttribute();
                action.Filters.Add(new AuthorizeFilter(authAttribute.Policy));
            }
        }

        /// <summary>
        /// 参数模型绑定
        /// </summary>
        /// <param name="controller"></param>
        private void ConfigureActionParameters(DynamicApiAction apiAction, ActionModel action)
        {
            if (apiAction.IsNull() || action.IsNull())
                return;

            foreach (var prm in action.Parameters)
            {
                if (prm.BindingInfo != null)
                {
                    continue;
                }

                if (!TypeHelper.IsPrimitiveExtendedIncludingNullable(prm.ParameterInfo.ParameterType))
                {
                    if (CanUseFormBodyBinding(apiAction, action, prm))
                    {
                        prm.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                    }
                }
            }
        }

        /// <summary>
        /// WebApi参数绑定
        /// </summary>
        private bool CanUseFormBodyBinding(DynamicApiAction apiAction, ActionModel action, ParameterModel parameter)
        {
            if (apiAction.IsNull() || action.IsNull())
                return false;

            //忽略
            if (!StringHelper.IsEqual(apiAction.ParamBinding, "FromBody"))
            {
                return false;
            }

            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpMethodActionConstraint == null)
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void RemoveEmptySelectors(IList<SelectorModel> selectors)
        {
            selectors
                .Where(selector => selector.AttributeRouteModel == null && selector.ActionConstraints.IsEmpty())
                .ToList()
                .ForEach(s => selectors.Remove(s));
        }

        private static AttributeRouteModel CreateRoute(string route, string module, string controller, string action)
        {
            if (route.IsEmpty())
                return null;

            var name = GetServiceRemovePostfix(controller);

            string routeFormat = route.ToLower()
                    .Replace("{module}", module)
                    .Replace("[module]", module)
                    .Replace("{controller}", name)
                    .Replace("[controller]", name)
                    .Replace("{action}", action)
                    .Replace("[action]", action);

            return new AttributeRouteModel(new RouteAttribute(routeFormat.ToLower()));
        }

        private static string GetServiceRemovePostfix(string serviceName)
        {
            if (!DynamicApiConfigObjService.Instance.ServicePostfix.IsEmpty())
            {
                serviceName = serviceName.RemovePostFix(DynamicApiConfigObjService.Instance.ServicePostfix.Split('|'));
            }
            return serviceName;
        }
    }
}
