namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;

    /// <summary>
    /// 文件上传数据信息
    /// </summary>
    public class FileUploadData
    {
        /// <summary>
        /// 边界
        /// </summary>
        public static String Boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

        private String contentTypeFormat = "Content-Type: {0}";
        private String fileFormat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
        private String fieldFormat = "Content-Disposition: form-data; name=\"{0}\"";
        private Encoding encode = Encoding.GetEncoding("UTF-8");
        private List<byte> formData;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileUploadData()
        {
            formData = new List<byte>();
        }

        /// <summary>
        /// 添加数据字段
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddField(string name, string value)
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(string.Format(fieldFormat, name) + "\r\n\r\n"));
            formData.AddRange(encode.GetBytes(value + "\r\n"));
        }

        /// <summary>
        /// 添加文件内容
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        public void AddFile(string name, string fileName, byte[] bytes)
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(string.Format(fileFormat, name, fileName) + "\r\n"));
            formData.AddRange(encode.GetBytes(string.Format(contentTypeFormat, "application/octet-stream") + "\r\n\r\n"));
            formData.AddRange(bytes);
            formData.AddRange(encode.GetBytes("\r\n"));
        }

        /// <summary>
        /// 添加字节内容
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="bytes"></param>
        /// <param name="contentType"></param>
        public void AddByte(string name, byte[] bytes, string contentType)
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(string.Format(fieldFormat, name) + "\r\n"));
            formData.AddRange(bytes);
            formData.AddRange(encode.GetBytes("\r\n"));
        }

        /// <summary>
        /// 预备表单数据   AddField/AddFile/AddByte后执行
        /// </summary>
        public void PrepareFormData()
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "--"));
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <returns></returns>
        public List<byte> GetFormData()
        {
            return formData;
        }

        #region User Demo 

        //FileUploadData data = new FileUploadData();
        //data.AddField("id", file.Id);
        //data.AddField("name", file.Name);
        //data.AddField("type", file.Type);
        //data.AddField("size", StringHelper.Get(file.Size));
        //data.AddField("chunk", StringHelper.Get(file.Chunk));
        //data.AddField("chunks", StringHelper.Get(file.Chunks));
        //data.AddField("lastModifiedDate", StringHelper.Get(file.LastModifiedDate));
        //data.AddFile("file", file.Name, file.Bytes);
        //data.PrepareFormData();

        //option.URL = url;
        //option.Method = "POST";
        //option.PostDataType = EnumHttpData.File;
        //option.PostDataByte = data.GetFormData().ToArray();

        #endregion
    }
}
