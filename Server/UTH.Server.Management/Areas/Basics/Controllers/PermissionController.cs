using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Module;
using UTH.Plug;

namespace UTH.Server.Management.Areas.Basics.Controllers
{
    [Area("Basics")]
    public class PermissionController : AuthBase
    {
        #region 页面视图

        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 权限导航编辑
        /// </summary>
        public IActionResult NavEdit()
        {
            return View();
        }

        /// <summary>
        /// 权限操作编辑
        /// </summary>
        public IActionResult ActionEdit()
        {
            return View();
        }

        #endregion

        #region 数据操作

        #endregion
    }
}