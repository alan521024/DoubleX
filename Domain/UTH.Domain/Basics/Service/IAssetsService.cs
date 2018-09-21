﻿namespace UTH.Domain
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
    /// 资源业务接口
    /// </summary>
    public interface IAssetsService : IApplicationDefault<AssetsEntity, AssetsEditInput, AssetsOutput>
    {
    }
}
