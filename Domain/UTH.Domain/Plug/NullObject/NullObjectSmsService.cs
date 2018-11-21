namespace UTH.Domain
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
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 空对象(Null) 短信口,使用插件替换
    /// </summary>
    public class NullObjectSmsService : ISmsService
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public SmsSendOutput Send(string mobile, string content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发送(异步)
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<SmsSendOutput> SendAsync(string mobile, string content)
        {
            throw new NotImplementedException();
        }
    }
}
