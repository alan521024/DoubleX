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
    public class AppVersionController : AuthBase
    {
        #region Page

        /// <summary>
        /// 版本管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(Guid app)
        {
            var model = new AppDTO() { AppType = EnumAppType.Web };
            if (!app.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Basics.AppGetId.GetResult<AppDTO, AppEditInput>(new AppEditInput() { Id = app });
                if (result.Code == EnumCode.成功)
                {
                    model = result.Obj;
                }
            }
            return View(model);
        }

        /// <summary>
        /// 版本编辑
        /// </summary>
        public IActionResult Edit(Guid app, Guid id)
        {
            var model = new AppVersionEdit();
            model.Detail.AppId = app;

            var appResult = PlugCoreHelper.ApiUrl.Basics.AppQuery.GetResult<List<AppDTO>, QueryInput<AppDTO>>(new QueryInput<AppDTO>() { Size = 100 });
            if (appResult.Code == EnumCode.成功)
            {
                model.Apps = appResult.Obj;
            }

            if (!id.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.Basics.AppVersionGetId.GetResult<AppVersionDTO, AppVersionEditInput>(new AppVersionEditInput() { Id = id });
                if (result.Code == EnumCode.成功)
                {
                    model.Detail = result.Obj;
                }
                else
                {
                    throw new DbxException(result.Code, result.Message);
                }
            }

            return View(model);
        }

        #endregion

        #region HTTP

        #endregion
    }
}