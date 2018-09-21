namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 配置服务接口(基于对象配置,默认使用内存缓存基于文件)
    /// eg:https://blog.csdn.net/bdstjk/article/details/7210742
    /// eg:https://www.mgenware.com/blog/?p=141
    /// eg:https://www.cnblogs.com/KeithWang/archive/2012/02/22/2363443.html
    /// </summary>
    public interface IConfigObjService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 配置对象对应保存的.config文件名称(保存路由为引擎配置config值)
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 缓存信息Key
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// 加载配置对象
        /// </summary>
        /// <returns></returns>
        TEntity Load(bool checkTime = true);

        /// <summary>
        /// 保存配置对象
        /// </summary>
        /// <returns></returns>
        bool Save(TEntity entity);
    }
}
