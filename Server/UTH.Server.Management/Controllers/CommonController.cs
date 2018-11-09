using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Server.Management.Controllers
{
    [AllowAnonymous]
    public class CommonController : Controller
    {
        public ICaptchaApplication captchaService { get; set; }
        public IHttpContextAccessor accessor { get; set; }

        /// <summary>
        /// 执行Api
        /// </summary>
        public ResultModel<object> Do()
        {
            string actionUrl = WebHelper.GetQueryValue(accessor.HttpContext, "action"), requestBody = null;
            using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
            {
                requestBody = reader.ReadToEnd();
            }
            actionUrl.CheckEmpty();

            return $"/api{actionUrl}".GetResult<object>(requestBody);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Produces("application/json")]
        public ResultModel<bool> Upload(IFormCollection files)
        {
            return PlugCoreHelper.ApiUrl.Core.AssetsUpload.GetResult<bool>(WebHelper.GetFileUploadModel(files));
        }

        /// <summary>
        /// 验证码校验
        /// </summary>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<CaptchaOutput> Verify(EnumCaptchaCategory category, EnumCaptchaMode mode)
        {
            var result = new ResultModel<CaptchaOutput>() { Code = EnumCode.未知异常 };
            var output = captchaService.Send(new CaptchaInput() { Category = category, Mode = mode, Length = 4 });
            if (!output.IsNull() && !output.Code.IsEmpty())
            {
                if (mode == EnumCaptchaMode.Image)
                {
                    output.Code = Convert.ToBase64String(ImageHelper.GetContentGraphic(output.Code, 100, 40, 16));
                }

                result.Code = EnumCode.成功;
                result.Obj = output;
            }
            return result;
        }
    }
}
