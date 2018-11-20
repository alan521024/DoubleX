namespace UTH.Module
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
    using UTH.Domain;

    /// <summary>
    /// 安全码领域服务
    /// </summary>
    public class SecurityCodeService : DomainService, ISecurityCodeService
    {
        private char[] contents = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public SecurityCodeService(IApplicationSession session, ICachingService caching) :
            base(session, caching)
        {

        }

        /// <summary>
        /// 获取安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public SecurityCodeModel Get(EnumSecurityCodeType type, int length, string key = null, long expire = 0)
        {
            if (key.IsEmpty())
            {
                key = GuidHelper.GetToString(Guid.NewGuid());
            }

            if (expire == 0)
            {
                expire = EngineHelper.Configuration.CaptchaExpire;
            }

            if (length <= 0)
            {
                length = 4;
            }

            var result = Caching.Get<SecurityCodeModel>(key);
            if (!result.IsNull())
            {
                return result;
            }

            result = new SecurityCodeModel()
            {
                //Type = type,
                Key = key,
                Code = RandomHelper.GetToRandomString(length, contents: contents)
            };

            if (expire > 0)
            {
                Caching.Set(key, result, TimeSpan.FromSeconds(expire));
            }
            else
            {
                Caching.Set(key, result);
            }

            return result;
        }

        /// <summary>
        /// 校验安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Verify(EnumSecurityCodeType type, string key, string code)
        {
            key.CheckNull();
            var model = Caching.Get<SecurityCodeModel>(key);
            if (model.IsNull())
            {
                return false;
            }

            return StringHelper.IsEqual(code, model.Code, ignoreCase: true);
        }

        /// <summary>
        /// 移除安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public void Remove(EnumSecurityCodeType type, string key)
        {
            key.CheckNull();
            Caching.Remove(key);
        }

    }
}
