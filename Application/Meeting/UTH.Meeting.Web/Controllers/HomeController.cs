namespace UTH.Meeting.Web.Controllers
{
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

    public class HomeController : WebViewBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
