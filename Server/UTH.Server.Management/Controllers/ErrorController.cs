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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AllowAnonymous]
    public class ErrorController : WebViewBase
    {
        #region Page

        [Route("/error/404")]
        public virtual IActionResult Page404()
        {
            return View();
        }

        [Route("/error/401")]
        public virtual IActionResult Page401()
        {
            return View();
        }

        [Route("/error/500")]
        public virtual IActionResult Page500()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
