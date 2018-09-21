using System;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Http Client 请求操作 辅助类
    /// </summary>
    public abstract class HttpClientUtility
    {
        /// <summary>
        /// 获取请求结果对象
        /// </summary>
        /// <param name="options">请求配置</param>
        /// <returns></returns>
        abstract public HttpClientResult Request(HttpClientOptions options);
    }
}
