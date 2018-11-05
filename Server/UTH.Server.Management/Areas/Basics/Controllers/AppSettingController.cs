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

namespace UTH.Server.Management.Areas.Basics.Controllers
{
    [Area("Basics")]
    public class AppSettingController : AuthBase
    {
        #region Page

        /// <summary>
        /// 配置编辑
        /// </summary>
        public IActionResult Index(Guid app)
        {
            var model = new AppSettingEdit();
            var appResult = PlugCoreHelper.ApiUrl.Basics.AppGetId.GetResult<AppDTO, AppEditInput>(new AppEditInput() { Id = app });
            if (appResult.Code == EnumCode.成功)
            {
                model.App = appResult.Obj;
            }
            var result = PlugCoreHelper.ApiUrl.Basics.AppSettingGetByApp.GetResult<AppSettingDTO, AppSettingEditInput>(new AppSettingEditInput { AppId = app });
            if (result.Code == EnumCode.成功)
            {
                model.Detail = result.Obj;
            }
            return View(model);
        }

        #endregion

        #region HTTP

        #endregion
    }
}