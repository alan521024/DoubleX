namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 安全码领域服务接口
    /// </summary>
    public interface ISecurityCodeService : IDomainService
    {
        /// <summary>
        /// 获取安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        SecurityCodeModel Get(EnumSecurityCodeType type, int length, string key = null, long expire = 0);

        /// <summary>
        /// 校验安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Verify(EnumSecurityCodeType type, string key, string code);

        /// <summary>
        /// 移除安全码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        void Remove(EnumSecurityCodeType type, string key);
    }
}
