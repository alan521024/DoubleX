//namespace UTH.Plug.Notification
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using System.Text;
//    using UTH.Infrastructure.Resource;
//    using UTH.Infrastructure.Resource.Culture;
//    using UTH.Infrastructure.Utility;
//    using UTH.Framework;
//    using UTH.Plug;

//    /// <summary>
//    /// 阿里云短信服务
//    /// </summary>
//    public class AliyunSmsService : ApplicationService,ISmsService
//    {
//        public AliyunSmsService(IConfigObjService<SmsConfigModel> _config)
//        {
//            var list = _config.Load().Platforms;
//            if (!list.IsEmpty())
//            {
//                config = list.Find(x => x.Name == EnumSmsPlatform.Aliyun);
//            }
//        }

//        private SmsPlatformModel config { get; }

//        public SmsResult Send(string mobile, string content)
//        {
//            //TODO: Aliyun SMS
//            return new SmsResult() { };
//        }

//        public Task<SmsResult> SendAsync(string mobile, string content)
//        {
//            //TODO: Aliyun SMS
//            return Task.FromResult<SmsResult>(new SmsResult() { });
//        }
//    }
//}
