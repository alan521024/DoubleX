using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Server.Api.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorController : WebViewBase
    {
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
    }
}
