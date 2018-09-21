namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 授权过滤器(权限)
    /// </summary>
    public class WebAuthorizationFilter : IAsyncAuthorizationFilter, ITransientDependency
    {
        protected IAuthorizeService authorizeService;

        public WebAuthorizationFilter(IAuthorizeService _authorizeService)
        {
            authorizeService = _authorizeService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            if (!context.Filters.Any(item => item is AuthorizeFilter))
            {
                return;
            }

            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            //业务授权 权限/角色判断
            var controlTypeInfo = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo;
            var actionMethod = context.ActionDescriptor.GetMethodInfo();
            var serviceAttr = WebHelper.GetActionServiceAttribute(controlTypeInfo, actionMethod);
            if (serviceAttr != null && serviceAttr.IsAuthorize)
            {
                try
                {
                    //认证通过后进行授权校验
                    if (context.HttpContext.User.HasClaim(t => t.Type == ClaimTypes.Name))
                    {
                        //授权失败，抛出异常
                        await authorizeService.AuthorizeAsync(serviceAttr.Permissions);
                    }
                }
                catch (AuthorizeException ex)
                {
                    WrapErrorResult(context, ex);
                }
                catch (Exception ex)
                {
                    WrapErrorResult(context, ex);
                }
            }
        }

        /// <summary>
        /// 授权及出现异常结果处理
        /// </summary>
        private void WrapErrorResult(AuthorizationFilterContext context, Exception ex = null)
        {
            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            if (WebHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                if (ex != null)
                {
                    context.Result = new ObjectResult(WebHelper.GetExceptionResult(ex))
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                    };
                }
                else
                {
                    context.Result = new ObjectResult(new ResultModel() { Code = EnumCode.授权失败 })
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                    };
                }
            }
            else
            {
                //在未授权页面，进行授权操作，未通过，跳转至未授权页，循环了
                //严格讲(401页面不应进行授权过滤器,但该过滤器目前注册为全局（所有普），所以会出现该情况)
                if (context.HttpContext.Request.Path.ToString() != "/error/401")
                {
                    context.Result = new RedirectResult("/error/401");
                }
            }
        }
    }
}