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

namespace UTH.Server.Management.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : AuthBase
    {
        #region Page

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 用户导入
        /// </summary>
        /// <returns></returns>
        public IActionResult Import()
        {
            return View();
        }

        #endregion

        #region HTTP

        #endregion
    }
}