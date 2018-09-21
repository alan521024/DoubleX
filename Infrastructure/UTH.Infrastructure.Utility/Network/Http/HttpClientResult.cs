using System;
using System.Net;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Http 请求结果
    /// </summary>
    public class HttpClientResult
    {
        /// <summary>
        /// 返回状态码,默认为OK
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 返回状态说明
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header { get; set; }

        /// <summary>
        /// Http请求返回的Cookie[Header]
        /// </summary>
        public string CookieContent { get; set; }

        /// <summary>
        /// Cookie对象集合(Request.Cookie)
        /// </summary>
        public CookieCollection CookieCollection { get; set; }

        /// <summary>
        /// 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string Content { get; set; }

    }
}
