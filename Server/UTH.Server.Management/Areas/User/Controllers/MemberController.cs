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
    public class MemberController : AuthBase
    {
        #region Page

        /// <summary>
        /// 个人用户管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 个人用户编辑
        /// </summary>
        public IActionResult Edit(Guid id)
        {
            var model = new MemberDTO() { Birthdate=DateTimeHelper.DefaultDateTime  };
            if (!id.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.User.MemberGetId.GetResult<MemberDTO, MemberEditInput>(new MemberEditInput() { Id = id });
                if (result.Code == EnumCode.成功)
                {
                    return View(result.Obj);
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