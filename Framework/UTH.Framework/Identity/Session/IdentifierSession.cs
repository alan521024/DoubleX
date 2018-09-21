using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UTH.Infrastructure.Utility;

namespace UTH.Framework
{
    /// <summary>
    /// (Identity组件)会话信息
    /// </summary>
    [Serializable]
    public class IdentifierSession : DefaultSession, IApplicationSession
    {
        protected IAccessor access { get { return EngineHelper.Resolve<IAccessor>(); } }

        public override string Culture
        {
            get
            {
                string val = null;
                access.Items.TryGetValue("Culture", out val);
                return val != null ? val : null;
            }
        }

        public override string AppCode
        {
            get
            {
                string val = null;
                access.Items.TryGetValue("AppCode", out val);
                return val != null ? val : null;
            }
        }

        public override string ClientIp
        {
            get
            {
                string val = null;
                access.Items.TryGetValue("ClientIp", out val);
                return val != null ? val : null;
            }
        }

        public override string Token
        {
            get
            {
                return access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Token)?.Value;
            }
        }

        public override string Account
        {
            get
            {
                return access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Account)?.Value;
            }
        }

        public override string Mobile
        {
            get
            {
                return access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Mobile)?.Value;
            }
        }

        public override string Email
        {
            get
            {
                return access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Email)?.Value;
            }
        }

        public override string Role
        {
            get
            {
                return access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Role)?.Value;
            }
        }

        public override int Type
        {
            get
            {
                return IntHelper.Get(access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Type)?.Value);
            }
        }

        public override int Status
        {
            get
            {
                return IntHelper.Get(access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.Status)?.Value);
            }
        }

        public override Guid AccountId
        {
            get
            {
                return GuidHelper.Get(access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.AccountId)?.Value);
            }
        }

        public override Guid TenantId
        {
            get
            {
                return GuidHelper.Get(access.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypesExtend.TenantId)?.Value);
            }
        }

    }
}
