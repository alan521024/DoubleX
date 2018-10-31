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
        /// 文件下载
        /// </summary>
        /// <param name="type"></param>
        /// <param name="md5"></param>
        /// <param name="name"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        [Produces("application/json")]
        public virtual FileContentResult Download(int type, string md5, string name = "", long begin = 0, long end = 0, int chunk = 0)
        {
            accessor.CheckNull();

            var response = accessor.HttpContext.Response;
            var range = StringHelper.Get(accessor.HttpContext.Request.Headers["Range"]);
            if (!range.IsEmpty())//如果遵守协议，支持断点续传
            {
                //Content-Range=bytes 0-100/200
                long.TryParse(range.Split('=')[1].Split('-')[0], out begin);
                long.TryParse(range.Split('-')[1], out end);
                end = end - begin > 0 ? end : 0;
            }

            var filePath = AppHelper.GetPathByMd5(md5, fileName: name, basePath: "Upload");

            long length = 0;
            var datas = FilesHelper.ReadFile(filePath, begin, end, chunk, out length);

            response.ContentType = "application/octet-stream";
            response.Headers.Add("Content-Disposition", "attachment;filename=" + name);
            response.Headers.Add("Content-Range", "bytes " + begin + "-" + (datas == null ? 0 : datas.Length) + "/" + length);

            return File(datas, "application/octet-stream", name);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Produces("application/json")]
        public ResultModel<bool> Upload(IFormCollection files)
        {
            return $"/api/app/upload".GetResult<bool>(WebHelper.GetFileUploadModel(files));
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
