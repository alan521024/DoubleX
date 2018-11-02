using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;

namespace UTH.Server.Api.Controllers
{
    public class AssetsController : WebApiBase
    {
        public IHttpContextAccessor accessor { get; set; }
        public IDomainDefaultService<AssetsEntity> assetsService { get; set; }
        public IApplicationSession session { get; set; }

        public virtual FileContentResult Download(string md5, string name = "", long begin = 0, long end = 0, int chunk = 0, EnumAssetsType assetsType = EnumAssetsType.Default)
        {
            var response = accessor.HttpContext.Response;
            var range = StringHelper.Get(accessor.HttpContext.Request.Headers["Range"]);
            if (!range.IsEmpty())//如果遵守协议，支持断点续传
            {
                //Content-Range=bytes 0-100/200
                long.TryParse(range.Split('=')[1].Split('-')[0], out begin);
                long.TryParse(range.Split('-')[1], out end);
                end = end - begin > 0 ? end : 0;
            }

            var rootPath = EngineHelper.Configuration.FileServer.Upload;
            var file = FilesHelper.GetFile(FilesHelper.GetMd5DirPath(rootPath, md5), FilesHelper.GetMd5FileName(md5, name));
            if (name.IsEmpty())
            {
                name = file.Name;
            }

            long length = 0;
            var datas = FilesHelper.ReadFile(file.FullName, begin, end, chunk, out length);

            response.ContentType = "application/octet-stream";
            response.Headers.Add("Content-Disposition", "attachment;filename=" + name);
            response.Headers.Add("Content-Range", "bytes " + begin + "-" + (datas == null ? 0 : datas.Length) + "/" + length);

            return File(datas, "application/octet-stream", name);
        }

        public virtual JsonResult Upload(IFormCollection files)
        {
            var model = WebHelper.GetFileUploadModel(files);

            var rootPath = EngineHelper.Configuration.FileServer.Upload;
            var fileDir = FilesHelper.GetMd5DirPath(rootPath, model.Md5);
            var fileName = FilesHelper.GetMd5FileName(model.Md5, model.Name);

            if (model.Chunks <= 1)
            {
                var file = FilesHelper.SaveFile(fileDir, fileName, model.Bytes);
                InsertAssets(model.Md5, model.Name, file, model.AssetsType);
            }
            else
            {
                var chunkDir = FilesHelper.GetMd5DirPath(rootPath, model.Md5, "/temp");
                FilesHelper.SaveChunks(chunkDir, fileName, model.Chunk, model.Chunks, model.Bytes);

                if (model.AutoMerge && model.Chunk == model.Chunks - 1)
                {
                    var file = FilesHelper.MergeChunks(chunkDir, fileDir, fileName);
                    InsertAssets(model.Md5, model.Name, file, model.AssetsType);
                }
            }

            return new JsonResult(true);
        }

        public virtual JsonResult Merge(string md5, string name, EnumAssetsType assetsType = EnumAssetsType.Default)
        {
            var rootPath = EngineHelper.Configuration.FileServer.Upload;
            var fileDir = FilesHelper.GetMd5DirPath(rootPath, md5);
            var chunkDir = FilesHelper.GetMd5DirPath(rootPath, md5, "/temp");
            var fileName = FilesHelper.GetMd5FileName(md5, name);

            var file = FilesHelper.MergeChunks(chunkDir, fileDir, fileName);
            InsertAssets(md5, name, file, assetsType);

            return new JsonResult(true);
        }

        protected void InsertAssets(string md5, string name, FileInfo file, EnumAssetsType assetsType)
        {
            assetsService.Insert(new AssetsEntity()
            {
                AssetsType = assetsType,
                MD5 = md5,
                Name = name,
                Size = file.Length,
                Type = file.Extension,
                AccountId = session.User.Id,
                AppCode = session.Accessor.AppCode
            });
        }
    }
}