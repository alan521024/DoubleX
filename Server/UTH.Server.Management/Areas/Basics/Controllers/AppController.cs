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
    public class AppController : AuthBase
    {
        #region Page

        /// <summary>
        /// 应用管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 应用编辑
        /// </summary>
        public IActionResult Edit()
        {
            return View();
        }

        #endregion

        #region HTTP

        public AppOutput Insert([FromForm]AppEditInput input)
        {
            var result = PlugCoreHelper.ApiUrl.Basics.AppInsert.GetResult<AppOutput, AppEditInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return result.Obj;
            }
            throw new DbxException(result.Code, result.Message);
        }

        public AppOutput Modify([FromForm]AppEditInput input)
        {
            var result = PlugCoreHelper.ApiUrl.Basics.AppUpdate.GetResult<AppOutput, AppEditInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return result.Obj;
            }
            throw new DbxException(result.Code, result.Message);
        }

        public AppOutput Delete([FromForm]AppEditInput input)
        {
            var result = PlugCoreHelper.ApiUrl.Basics.AppDelete.GetResult<AppOutput, AppEditInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return result.Obj;
            }
            throw new DbxException(result.Code, result.Message);
        }

        public PagingModel<AppOutput> Paging([FromBody]QueryInput input)
        {
            var result = PlugCoreHelper.ApiUrl.Basics.AppPaging.GetResult<PagingModel<AppOutput>, QueryInput>(input);
            if (result.Code == EnumCode.成功)
            {
                return result.Obj;
            }
            throw new DbxException(result.Code, result.Message);
        }

        #endregion
    }
}