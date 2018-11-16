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
    public class EmployeController : AuthBase
    {
        #region Page

        /// <summary>
        /// 组织成员管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(Guid organize)
        {
            var model = new EmployeViewModel();
            if (!organize.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.User.OrganizeGetId.GetResult<OrganizeDTO, OrganizeEditInput>(new OrganizeEditInput() { Id = organize });
                if (result.Code == EnumCode.成功)
                {
                    model.Organize = result.Obj;
                }
            }
            return View(model);
        }

        /// <summary>
        /// 组织成员编辑
        /// </summary>
        public IActionResult Edit(Guid organize, Guid id)
        {
            var model = new EmployeViewModel() { Employe = new EmployeDTO() };
            if (!id.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.User.EmployeGetId.GetResult<EmployeDTO, EmployeEditInput>(new EmployeEditInput() { Id = id });
                if (result.Code == EnumCode.成功)
                {
                    model.Employe = result.Obj;
                }
            }

            if (!organize.IsEmpty())
            {
                var result = PlugCoreHelper.ApiUrl.User.OrganizeGetId.GetResult<OrganizeDTO, OrganizeEditInput>(new OrganizeEditInput() { Id = organize });
                if (result.Code == EnumCode.成功)
                {
                    model.Organize = result.Obj;
                }
            }

            if (model.Organize.IsNull() && !model.Employe.IsNull())
            {
                var result = PlugCoreHelper.ApiUrl.User.OrganizeQuery.GetResult<List<OrganizeDTO>, QueryInput<OrganizeDTO>>(new QueryInput<OrganizeDTO>() { Size = 1, Query = new OrganizeDTO() { Code = model.Employe.Organize } });
                if (result.Code == EnumCode.成功)
                {
                    model.Organize = result.Obj.FirstOrDefault();
                }
            }
            return View(model);
        }

        #endregion

        #region HTTP

        #endregion
    }
}