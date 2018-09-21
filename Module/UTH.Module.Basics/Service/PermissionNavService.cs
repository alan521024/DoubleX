namespace UTH.Module.Basics
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
    using UTH.Plug;

    /// <summary>
    /// 权限路径业务
    /// </summary>
    public class PermissionNavService : ApplicationTree<PermissionNavEntity>, IPermissionNavService
    {
        #region 构造函数

        public PermissionNavService(IRepository<PermissionNavEntity> _repository) : base(_repository)
        {

        }

        #endregion

        #region 私有变量

        #endregion

        #region 公共属性

        #endregion
    }
}
