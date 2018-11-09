using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// HttpWebRequest / HttpWebResponse 请求Http
    /// </summary>
    public class HttpWebClientUtility : HttpClientUtility
    {
        /// <summary>
        /// 请求对象
        /// </summary>
        protected HttpWebRequest request { get; set; }

        /// <summary>
        /// 结果对象
        /// </summary>
        protected HttpWebResponse response { get; set; }

        /// <summary>
        /// 获取请求结果对象
        /// </summary>
        public override HttpClientResult Request(HttpClientOptions options)
        {
            HttpClientResult result = new HttpClientResult();
            try
            {
                ConfigRequest(options);
            }
            catch (Exception ex)
            {
                //配置参数时出错
                result.Header = null;
                result.CookieContent = string.Empty;
                result.StatusDescription = "HttpClientOptions 配置参数时出错：" + ex.Message;
                result.Content = ex.Message;
                return result;
            }
            try
            {
                var rep = request.GetResponse();
                //请求数据
                using (response = (HttpWebResponse)rep)
                {
                    ConfigResponse(options, response, result);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (response = (HttpWebResponse)ex.Response)
                    {
                        ConfigResponse(options, response, result);
                        result.Content = ExceptionHelper.GetMessage(ex);
                    }
                }
                else
                {
                    //无法连接到远程服务器
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Content = ex.Message;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Content = ExceptionHelper.GetMessage(ex);
            }
            return result;
        }

        #region 设置请求信息

        /// <summary>
        /// 设置请求对象参数信息
        /// </summary>
        private void ConfigRequest(HttpClientOptions item)
        {
            //初始化对像，并设置请求的URL地址/方式 Get或者Post
            request = (HttpWebRequest)WebRequest.Create(item.URL);
            request.Method = item.Method;

            //文件上传特殊处理
            if (item.PostDataType == EnumHttpData.File && item.PostBytes != null && item.PostBytes.Length > 0)
            {
                item.Accept = "*/*";
                item.Method = "POST";
                item.ContentType = "multipart/form-data; boundary=" + FileUploadData.Boundary;
            }

            //设置HTTP相关基础信息
            SetHttpBase(item);

            //设置头部信息
            SetHeader(item);

            //设置请求Cookie
            SetCookie(item);

            //设置POST请求
            SetPostData(item);

            // 设置代理
            SetProxy(item);

            // 验证证书
            SetCer(item);

            //来源地址及是否执行跳转功
            request.Referer = item.Referer;
            request.AllowAutoRedirect = item.Allowautoredirect;

        }

        /// <summary>
        /// 设置HTTP相关基础信息
        /// </summary>
        private void SetHttpBase(HttpClientOptions item)
        {
            request.Timeout = item.Timeout;
            request.ReadWriteTimeout = item.ReadWriteTimeout;

            if (item.MaximumAutomaticRedirections > 0)
            {
                request.MaximumAutomaticRedirections = item.MaximumAutomaticRedirections;
            }

            if (item.ProtocolVersion != null) request.ProtocolVersion = item.ProtocolVersion;

            if (item.IfModifiedSince != null) request.IfModifiedSince = Convert.ToDateTime(item.IfModifiedSince);

            request.Credentials = item.ICredentials;

            //request.ServicePoint.Expect100Continue = item.Expect100Continue;

            //if (item.Connectionlimit > 0) request.ServicePoint.ConnectionLimit = item.Connectionlimit;
        }

        /// <summary>
        /// 设置请求头部信息
        /// </summary>
        private void SetHeader(HttpClientOptions item)
        {
            //设置Header参数
            if (item.Header != null && item.Header.Count > 0)
            {
                foreach (string key in item.Header.AllKeys)
                {
                    request.Headers.Add(key, item.Header[key]);
                }
            }

            //Accept
            request.Accept = item.Accept;

            //ContentType返回类型
            request.ContentType = item.ContentType;

            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            request.UserAgent = item.UserAgent;
            request.KeepAlive = item.KeepAlive;
        }

        /// <summary>
        /// 设置Post数据
        /// </summary>
        private void SetPostData(HttpClientOptions item)
        {
            //验证在得到结果时是否有传入数据
            if (!request.Method.Trim().ToLower().Contains("get"))
            {
                byte[] buffer = null;

                if (item.PostDataType == EnumHttpData.Byte && item.PostBytes != null && item.PostBytes.Length > 0)
                {
                    //写入Byte字节流类型
                    buffer = item.PostBytes;
                }
                else if (item.PostDataType == EnumHttpData.File && item.PostBytes != null && item.PostBytes.Length > 0)
                {
                    //写入文件
                    buffer = item.PostBytes;
                }
                else if (!string.IsNullOrWhiteSpace(item.PostString))
                {
                    buffer = item.PostEncoding.GetBytes(item.PostString);
                    //string str = item.PostEncoding.GetString(buffer);
                }

                if (buffer != null)
                {
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// 设置请求Cookie
        /// </summary>
        /// <param name="item">Http参数</param>
        private void SetCookie(HttpClientOptions item)
        {
            request.CookieContainer = new CookieContainer();
            if (!string.IsNullOrEmpty(item.CookieContent))
            {
                string cookieHeadName = HttpRequestHeader.Cookie.ToString();
                if (!string.IsNullOrWhiteSpace(request.Headers.Get(cookieHeadName)))
                {
                    request.Headers.Set(cookieHeadName, item.CookieContent);
                }
                else
                {
                    request.Headers.Add(cookieHeadName, item.CookieContent);
                }
            }
            if (item.CookieCollection != null && item.CookieCollection.Count > 0)
            {
                request.CookieContainer.Add(item.CookieCollection);
            }
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        private void SetProxy(HttpClientOptions item)
        {
            bool isIeProxy = false;
            if (!string.IsNullOrEmpty(item.ProxyIp))
            {
                isIeProxy = item.ProxyIp.ToLower().Contains("ieproxy");
            }
            if (!string.IsNullOrEmpty(item.ProxyIp) && !isIeProxy)
            {
                //设置代理服务器
                if (item.ProxyIp.Contains(":"))
                {
                    string[] plist = item.ProxyIp.Split(':');
                    WebProxy myProxy = new WebProxy(plist[0].Trim(), Convert.ToInt32(plist[1].Trim()));
                    //建议连接
                    myProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    //给当前请求对象
                    request.Proxy = myProxy;
                }
                else
                {
                    WebProxy myProxy = new WebProxy(item.ProxyIp, false);
                    //建议连接
                    myProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    //给当前请求对象
                    request.Proxy = myProxy;
                }
            }
            else if (isIeProxy)
            {
                //设置为IE代理
            }
            else if (item.ProxyWeb != null)
            {
                request.Proxy = item.ProxyWeb;
            }
        }

        /// <summary>
        /// 设置证书
        /// </summary>
        private void SetCer(HttpClientOptions item)
        {
            if (!string.IsNullOrEmpty(item.CerPath))
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                SetCerList(item);
                //将证书添加到请求里
                request.ClientCertificates.Add(new X509Certificate(item.CerPath));
            }
            else
            {
                SetCerList(item);
            }
        }

        /// <summary>
        /// 设置多个证书
        /// </summary>
        private void SetCerList(HttpClientOptions item)
        {
            if (item.ClentCertificates != null && item.ClentCertificates.Count > 0)
            {
                foreach (X509Certificate c in item.ClentCertificates)
                {
                    request.ClientCertificates.Add(c);
                }
            }
        }

        #endregion

        #region 辅助操作方法

        private void ConfigResponse(HttpClientOptions options, HttpWebResponse response, HttpClientResult result)
        {
            SetResultBase(response, result);
            SetResultContent(options, result);
        }

        private void SetResultBase(HttpWebResponse response, HttpClientResult result)
        {
            if (response.IsNull())
                return;

            //获取StatusCode
            result.StatusCode = response.StatusCode;
            //获取StatusDescription
            result.StatusDescription = response.StatusDescription;
            //获取Headers
            result.Header = response.Headers;

            //获取CookieCollection
            if (response.Cookies != null)
                result.CookieCollection = response.Cookies;

            //获取set-cookie
            if (response.Headers["set-cookie"] != null)
                result.CookieContent = response.Headers["set-cookie"];


        }

        /// <summary>
        /// 获取数据的并解析的方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="result"></param>
        private void SetResultContent(HttpClientOptions item, HttpClientResult result)
        {
            #region byte

            //处理返回Byte
            byte[] responseByte = GetByte();

            #endregion

            #region encoding

            //设置编码
            if (responseByte != null & responseByte.Length > 0)
            {
                SetEncoding(item, result, responseByte);
            }

            #endregion

            #region result

            result.Bytes = null;
            result.Content = string.Empty;

            if (item.ResultByte)
            {
                result.Bytes = responseByte;
            }

            if (responseByte != null & responseByte.Length > 0)
            {
                switch (item.ResultType)
                {
                    case EnumHttpData.Base64:
                        result.Content = CodingHelper.GetBase64ByByte(responseByte);
                        break;
                    default:
                        result.Content = item.ResultEncoding.GetString(responseByte);
                        break;
                }
            }
            #endregion
        }

        /// <summary>
        /// 回调验证证书问题
        /// </summary>
        /// <param name="sender">流对象</param>
        /// <param name="certificate">证书</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="errors">SslPolicyErrors</param>
        /// <returns>bool</returns>
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="item">HttpItem</param>
        /// <param name="result">HttpResult</param>
        /// <param name="ResponseByte">byte[]</param>
        private void SetEncoding(HttpClientOptions item, HttpClientResult result, byte[] ResponseByte)
        {
            //从这里开始我们要无视编码了
            var resolveCoding = Encoding.Default;
            if (resolveCoding == null || resolveCoding == Encoding.Default)
            {
                Match meta = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta[^<]*charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                string c = string.Empty;
                if (meta != null && meta.Groups.Count > 0)
                {
                    c = meta.Groups[1].Value.ToLower().Trim();
                }
                if (c.Length > 2)
                {
                    try
                    {
                        resolveCoding = Encoding.GetEncoding(c.Replace("\"", string.Empty).Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
                    }
                    catch
                    {
                        if (string.IsNullOrEmpty(response.CharacterSet))
                        {
                            resolveCoding = Encoding.UTF8;
                        }
                        else
                        {
                            resolveCoding = Encoding.GetEncoding(response.CharacterSet);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(response.CharacterSet))
                    {
                        resolveCoding = Encoding.UTF8;
                    }
                    else
                    {
                        resolveCoding = Encoding.GetEncoding(response.CharacterSet);
                    }
                }
            }

            if (resolveCoding != Encoding.Default && item.ResultEncoding != resolveCoding)
            {
                item.ResultEncoding = resolveCoding; //重置内容编辑
            }
        }

        /// <summary>
        /// 提取网页Byte
        /// </summary>
        /// <returns></returns>
        private byte[] GetByte()
        {
            byte[] ResponseByte = null;
            MemoryStream _stream = new MemoryStream();

            //GZIIP处理
            if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
            {
                //开始读取流并设置编码方式
                _stream = GetMemoryStream(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
            }
            else
            {
                //开始读取流并设置编码方式
                _stream = GetMemoryStream(response.GetResponseStream());
            }
            //获取Byte
            ResponseByte = _stream.ToArray();
            _stream.Close();
            return ResponseByte;
        }

        /// <summary>
        /// 4.0以下.net版本取数据使用
        /// </summary>
        /// <param name="streamResponse">流</param>
        private MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }

        //public static string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        //{
        //    string result = string.Empty;
        //    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        //    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        //    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        //    wr.ContentType = "multipart/form-data; boundary=" + boundary;
        //    wr.Method = "POST";
        //    wr.KeepAlive = true;
        //    wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        //    Stream = wr.GetRequestStream();

        //    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        //    foreach (string key in nvc.Keys)
        //    {
        //        rs.Write(boundarybytes, 0, boundarybytes.Length);
        //        string formitem = string.Format(formdataTemplate, key, nvc[key]);
        //        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
        //        rs.Write(formitembytes, 0, formitembytes.Length);
        //    }
        //    rs.Write(boundarybytes, 0, boundarybytes.Length);

        //    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        //    string header = string.Format(headerTemplate, paramName, file, contentType);
        //    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        //    rs.Write(headerbytes, 0, headerbytes.Length);

        //    FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    byte[] buffer = new byte[4096];
        //    int bytesRead = 0;
        //    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        //    {
        //        rs.Write(buffer, 0, bytesRead);
        //    }
        //    fileStream.Close();

        //    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        //    rs.Write(trailer, 0, trailer.Length);
        //    rs.Close();

        //    WebResponse wresp = null;
        //    try
        //    {
        //        wresp = wr.GetResponse();
        //        Stream stream2 = wresp.GetResponseStream();
        //        StreamReader reader2 = new StreamReader(stream2);

        //        result = reader2.ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (wresp != null)
        //        {
        //            wresp.Close();
        //            wresp = null;
        //        }
        //    }
        //    finally
        //    {
        //        wr = null;
        //    }

        //    return result;
        //}

        #endregion



        public class MsMultiPartFormData
        {
            private List<byte> formData;
            public String Boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            private String fieldName = "Content-Disposition: form-data; name=\"{0}\"";
            private String fileContentType = "Content-Type: {0}";
            private String fileField = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
            private Encoding encode = Encoding.GetEncoding("UTF-8");

            public MsMultiPartFormData()
            {
                formData = new List<byte>();
            }
            public void AddFormField(String FieldName, String FieldValue)
            {
                String newFieldName = fieldName;
                newFieldName = string.Format(newFieldName, FieldName);
                formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
                formData.AddRange(encode.GetBytes(newFieldName + "\r\n\r\n"));
                formData.AddRange(encode.GetBytes(FieldValue + "\r\n"));
            }
            public void AddFile(String FieldName, String FileName, byte[] FileContent, String ContentType)
            {
                String newFileField = fileField;
                String newFileContentType = fileContentType;
                newFileField = string.Format(newFileField, FieldName, FileName);
                newFileContentType = string.Format(newFileContentType, ContentType);
                formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
                formData.AddRange(encode.GetBytes(newFileField + "\r\n"));
                formData.AddRange(encode.GetBytes(newFileContentType + "\r\n\r\n"));
                formData.AddRange(FileContent);
                formData.AddRange(encode.GetBytes("\r\n"));
            }

            public void AddStreamFile(String FieldName, String FileName, byte[] FileContent)
            {
                AddFile(FieldName, FileName, FileContent, "application/octet-stream");
            }

            public void PrepareFormData()
            {
                formData.AddRange(encode.GetBytes("--" + Boundary + "--"));
            }

            public List<byte> GetFormData()
            {
                return formData;
            }
        }

        public static class PostDataServer
        {
            public static string HttpPostData(string url, MsMultiPartFormData form, string filePath, string fileKeyName = "file", int timeOut = 360000)
            {
                string result = "";
                WebRequest request = WebRequest.Create(url);
                request.Timeout = timeOut;
                FileStream file = new FileStream(filePath, FileMode.Open);
                byte[] bb = new byte[file.Length];
                file.Read(bb, 0, (int)file.Length);
                file.Close();

                string fileName = Path.GetFileName(filePath);

                form.AddStreamFile(fileKeyName, fileName, bb);
                form.PrepareFormData();
                request.ContentType = "multipart/form-data; boundary=" + form.Boundary;
                request.Method = "POST";
                Stream stream = request.GetRequestStream();
                foreach (var b in form.GetFormData())
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                WebResponse response = request.GetResponse();
                using (var httpStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    result = httpStreamReader.ReadToEnd();
                }
                response.Close(); request.Abort(); return result;
            }
        }
    }
}
