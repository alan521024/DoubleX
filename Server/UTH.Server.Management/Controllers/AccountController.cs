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
        public ICaptchaApplication captchaService { get; set; }

        #region Page

        /// <summary>
        /// Page Login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            string currentPath = AppContext.BaseDirectory;
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
        public virtual ActionResult Login([FromForm]SignInInput input)
        {
            input.CheckNull();

            if (input.ImgCode.IsEmpty() || input.ImgCodeKey.IsEmpty())
            {
                this.AddPageError(Lang.userYanZhengMaCuoWu);
                return View();
            }

            if (!captchaService.Verify(new CaptchaInput()
            {
                Category = EnumCaptchaCategory.Login,
                Mode = EnumCaptchaMode.Image,
                Key = input.ImgCodeKey,
                Code = input.ImgCode
            }))
            {
                this.AddPageError(Lang.userYanZhengMaCuoWu);
                return View();
            }

            var result = PlugCoreHelper.ApiUrl.User.SignIn.GetResult<SignInOutput, SignInInput>(input);
            if (result.Code == EnumCode.成功)
            {
                AppHelper.SignIn(result.Obj.Token);
                return !ReturnUrl.IsEmpty() ? Redirect(ReturnUrl) : Redirect("/");
            }
            else
            {
                this.AddPageError(result.Message);
                return View();
            }
        }

        #endregion
    }
}
