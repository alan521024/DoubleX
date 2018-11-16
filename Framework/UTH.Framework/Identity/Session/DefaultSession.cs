using System;
using System.Collections.Generic;
using System.Text;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;

namespace UTH.Framework
{
    /// <summary>
    /// 默认会话信息
    /// </summary>
    [Serializable]
    public class DefaultSession : IApplicationSession
    {
        public DefaultSession(IAccessor accessor = null, IIdentifier user = null)
        {
            Accessor = accessor;
            User = user;
            IsAuthenticated = Accessor == null ? false : Accessor.Principal.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 访问信息
        /// </summary>
        public virtual IAccessor Accessor { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual IIdentifier User { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool IsAuthenticated { get; set; }

        /// <summary>
        /// 检查企业参数
        /// </summary>
        /// <param name="organize"></param>
        /// <param name="isThrow"></param>
        /// <returns></returns>
        public virtual bool CheckAccountOrganize(string organize, bool isThrow = true)
        {
            if (!IsAuthenticated) { throw new DbxException(EnumCode.认证过期); }

            if (User.Type == EnumAccountType.管理员)
            {
                return true;
            }

            bool isCheck = false;

            if (User.Type == EnumAccountType.组织用户 || User.Type == EnumAccountType.组织成员)
            {
                isCheck = !organize.IsEmpty() && User.Organize == organize;
            }

            if (!isCheck && isThrow)
            {
                throw new DbxException(EnumCode.参数异常, "organize");
            }

            return isCheck;
        }
    }
}
