using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Server.Management.Controllers
{
    [AllowAnonymous]
    public class CommonController : WebViewBase
    {
        public ICaptchaService captchaService { get; set; }

        [HttpGet]
        public ResultModel<CaptchaOutput> VerifyCode(EnumNotificationCategory category, EnumNotificationType type)
        {
            var result = new ResultModel<CaptchaOutput>() { Code = EnumCode.未知异常 };
            var output = captchaService.Send(new CaptchaSendInput() { Category = category, Type = type, Length = 4 });
            if (!output.IsNull() && !output.Code.IsEmpty())
            {
                output.Code = Convert.ToBase64String(ImageHelper.GetContentGraphic(output.Code, 100, 40, 16));

                result.Code = EnumCode.成功;
                result.Obj = output;
            }
            return result;
        }
    }
}
