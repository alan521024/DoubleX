using System;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// HTTP 请求配置项
    /// </summary>
    public class HttpClientOptions
    {
        #region 私有变量

        string _url = string.Empty;
        string _method = "GET";

        int _timeout = 100000;
        int _readWriteTimeout = 30000;
        int _maximumAutomaticRedirections;
        Version _version;
        DateTime? _ifModifiedSince = null;
        Boolean _isToLower = false;
        ICredentials _iCredentials = CredentialCache.DefaultCredentials;
        Boolean _expect100continue = true;
        int _connectionlimit = 1024;

        WebHeaderCollection _header = new WebHeaderCollection();
        string _accept = "text/html, application/xhtml+xml, */*";
        string _contentType = "text/html";
        string _userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        Boolean _keepAlive = true;

        EnumHttpData _postDataType = EnumHttpData.String;
        Encoding _postEncoding = Encoding.UTF8; //WPF 默认非UTF-8 Encoding.Default;发送中文有问题
        string _postData = string.Empty;
        byte[] _postDataByte = null;

        CookieCollection _requestCookieCollection = null;
        string _requestCookieValue = string.Empty;

        Boolean _allowautoredirect = false;
        string _referer = string.Empty;


        WebProxy _proxyWeb;
        string _proxyUserName = string.Empty;
        string _proxyIp = string.Empty;
        string _proxyPwd = string.Empty;

        X509CertificateCollection _clentCertificates;
        string _cerPath = string.Empty;


        Encoding _resultEncoding = Encoding.UTF8; //WPF 默认非UTF-8 Encoding.Default;中文有问题
        bool _resultByte = true;
        EnumHttpData _resultType = EnumHttpData.String;

        #endregion

        #region 公共属性

        #region 基本信息

        /// <summary>
        /// 请求URL必须填写
        /// </summary>
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 请求方式默认为GET方式,当为POST方式时必须设置PostData的值
        /// </summary>
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        /// <summary>
        /// 请求返回类型
        /// text/html(默认)
        /// application/x-www-form-urlencoded
        /// application/json
        /// </summary>
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        #endregion

        #region HTTP配置

        /// <summary>
        /// 默认请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// 默认写入Post数据超时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _readWriteTimeout; }
            set { _readWriteTimeout = value; }
        }

        /// <summary>
        /// 设置请求将跟随的重定向的最大数目
        /// </summary>
        public int MaximumAutomaticRedirections
        {
            get { return _maximumAutomaticRedirections; }
            set { _maximumAutomaticRedirections = value; }
        }

        /// <summary>
        /// 获取或设置用于请求的 HTTP 版本。返回结果:用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11。
        /// </summary>
        public Version ProtocolVersion
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 获取和设置IfModifiedSince，默认为当前日期和时间
        /// </summary>
        public DateTime? IfModifiedSince
        {
            get { return _ifModifiedSince; }
            set { _ifModifiedSince = value; }
        }

        /// <summary>
        /// 是否设置为全文小写，默认为不转化
        /// </summary>
        public Boolean IsToLower
        {
            get { return _isToLower; }
            set { _isToLower = value; }
        }

        /// <summary>
        /// 获取或设置请求的身份验证信息。(安全凭证)
        /// </summary>
        public ICredentials ICredentials
        {
            get { return _iCredentials; }
            set { _iCredentials = value; }
        }

        /// <summary>
        ///  获取或设置一个 System.Boolean 值，该值确定是否使用 100-Continue 行为。如果 POST 请求需要 100-Continue 响应，则为 true；否则为 false。默认值为 true。
        /// </summary>
        public Boolean Expect100Continue
        {
            get { return _expect100continue; }
            set { _expect100continue = value; }
        }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int Connectionlimit
        {
            get { return _connectionlimit; }
            set { _connectionlimit = value; }
        }

        #endregion

        #region 头部信息

        /// <summary>
        /// Header对象
        /// </summary>
        public WebHeaderCollection Header
        {
            get { return _header; }
            set { _header = value; }
        }

        /// <summary>
        /// 请求标头值 默认为text/html, application/xhtml+xml, */*
        /// </summary>
        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        /// <summary>
        /// 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        ///  获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接默认为true。
        /// </summary>
        public Boolean KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        #endregion

        #region POST设置

        /// <summary>
        /// 返回数据编码默认为NUll,可以自动识别,一般为utf-8,gbk,gb2312
        /// </summary>
        public Encoding PostEncoding
        {
            get { return _postEncoding; }
            set { _postEncoding = value; }
        }

        /// <summary>
        /// Post的数据类型
        /// </summary>
        public EnumHttpData PostDataType
        {
            get { return _postDataType; }
            set { _postDataType = value; }
        }

        /// <summary>
        /// Post请求时要发送的字符串Post数据
        /// </summary>
        public string PostData
        {
            get { return _postData; }
            set { _postData = value; }
        }

        /// <summary>
        /// Post请求时要发送的Byte类型的Post数据
        /// </summary>
        public byte[] PostDataByte
        {
            get { return _postDataByte; }
            set { _postDataByte = value; }
        }

        #endregion

        #region 代理设置

        /// <summary>
        /// 设置代理对象，不想使用IE默认配置就设置为Null，而且不要设置ProxyIp
        /// </summary>
        public WebProxy ProxyWeb
        {
            get { return _proxyWeb; }
            set { _proxyWeb = value; }
        }

        /// <summary>
        /// 代理Proxy 服务器用户名
        /// </summary>
        public string ProxyUserName
        {
            get { return _proxyUserName; }
            set { _proxyUserName = value; }
        }

        /// <summary>
        /// 代理 服务器密码
        /// </summary>
        public string ProxyPwd
        {
            get { return _proxyPwd; }
            set { _proxyPwd = value; }
        }

        /// <summary>
        /// 代理 服务IP ,如果要使用IE代理就设置为ieproxy
        /// </summary>
        /// 
        public string ProxyIp
        {
            get { return _proxyIp; }
            set { _proxyIp = value; }
        }


        #endregion

        #region 证书设置

        /// <summary>
        /// 设置509证书集合
        /// </summary>
        public X509CertificateCollection ClentCertificates
        {
            get { return _clentCertificates; }
            set { _clentCertificates = value; }
        }

        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath
        {
            get { return _cerPath; }
            set { _cerPath = value; }
        }

        #endregion

        #region Cookie

        /// <summary>
        /// Http请求返回的Cookie[Header]
        /// </summary>
        public string CookieContent { get; set; }

        /// <summary>
        /// 请求Cookie对象集合(Request.Cookie)
        /// </summary>
        public CookieCollection CookieCollection { get; set; }

        #endregion

        #region 跳转设置

        /// <summary>
        /// 支持跳转页面，查询结果将是跳转后的页面，默认是不跳转
        /// </summary>
        public Boolean Allowautoredirect
        {
            get { return _allowautoredirect; }
            set { _allowautoredirect = value; }
        }

        /// <summary>
        /// 来源地址，上次访问地址
        /// </summary>
        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        #endregion

        #region 结果内容

        /// <summary>
        /// 设置或获取Post参数编码,默认的为Default编码
        /// </summary>
        public Encoding ResultEncoding
        {
            get { return _resultEncoding; }
            set { _resultEncoding = value; }
        }

        /// <summary>
        ///  设置返回结果是否含 byte  默认True返回
        /// </summary>
        public bool ResultByte
        {
            get { return _resultByte; }
            set { _resultByte = value; }
        }

        /// <summary>
        /// 结果类型 默认String
        /// </summary>
        public EnumHttpData ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }


        #endregion

        #endregion
    }
}
