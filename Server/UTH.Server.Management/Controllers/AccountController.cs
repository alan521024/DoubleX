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
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Server.Management.Controllers
{
    [AllowAnonymous]
    public class AccountController : WebViewBase
    {
        public ICaptchaService captchaService { get; set; }

        #region Page

        /// <summary>
        /// Page Login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        #endregion

        #region HTTP

        /// <summary>
        /// Post Login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Login([FromForm]SignInInput input)
        {
            input.CheckNull();

            #region  验证码 校验

            if (input.ImgCode.IsEmpty() || input.ImgCodeTag.IsEmpty())
            {
                throw new DbxException(EnumCode.校验失败, Lang.userYanZhengMaCuoWu);
            }

            var captchaVerify = captchaService.Verify(new CaptchaVerifyInput()
            {
                Category = EnumNotificationCategory.Login,
                Type = EnumNotificationType.Image,
                Tag = input.ImgCodeTag,
                Code = input.ImgCode
            });

            if (!captchaVerify)
            {
                throw new DbxException(EnumCode.校验失败, Lang.userYanZhengMaCuoWu);
            }

            #endregion

            var result = PlugCoreHelper.ApiUrl.User.SignIn.GetResult<SignInOutput, SignInInput>(input);
            if (result.Code == EnumCode.成功)
            {
                AppHelper.SignIn(result.Obj.Token);
            }
            else
            {
                throw new DbxException(result.Code, result.Message);
            }

            return !ReturnUrl.IsEmpty() ? Redirect(ReturnUrl) : Redirect("/");
        }

        #endregion
    }
}
