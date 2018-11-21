namespace UTH.Module
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
    /// 文档领域服务接口
    /// </summary>
    public interface ITest
    {
        string Get();
    }

    public class Test : ITest
    {
        public string Get()
        {
            return "abc";
        }
    }
}
