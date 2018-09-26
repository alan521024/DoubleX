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

namespace UTH.Server.Management.Controllers
{
    public class AppController : WebApiBase
    {
        public IHttpContextAccessor accessor { get; set; }

        public virtual bool Verify(string appCode)
        {
            if (appCode == "980001981")
            {
                return true;
            }
            
            return true;
        }

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
    }
}