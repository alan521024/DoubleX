using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Principal;
using UTH.Infrastructure.Utility;

namespace UTH.Framework
{
    /// <summary>
    /// (Identity组件)会话信息
    /// </summary>
    [Serializable]
    public class IdentifierSession : DefaultSession, IApplicationSession
    {
        public IdentifierSession(IAccessor accessor) : base(accessor)
        {
            User = new DefaultIdentifier(accessor.Principal.Identity);
        }
    }
}
