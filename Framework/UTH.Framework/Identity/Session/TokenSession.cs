using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Principal;
using UTH.Infrastructure.Utility;

namespace UTH.Framework
{
    /// <summary>
    /// (Token字符串/对象)会话信息
    /// </summary>
    [Serializable]
    public class TokenSession : DefaultSession, IApplicationSession
    {
        public TokenSession(string token, TokenModel model) : base(null)
        {

        }
    }
}
