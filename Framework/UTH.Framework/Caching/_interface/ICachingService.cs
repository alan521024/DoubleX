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
    /// 缓存服务接口
    /// </summary>
    public interface ICachingService
    {
        /// <summary>
        /// 获取字符串缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>string</returns>
        string Get(string key);

        /// <summary>
        /// 获取对象缓存内容
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns>TEntity</returns>
        TEntity Get<TEntity>(string key) where TEntity : class;


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="str">字符串</param>
        void Set(string key, string str);

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="entity">TEntity</param>
        void Set<TEntity>(string key, TEntity entity) where TEntity : class;


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="str">字符串</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        void Set(string key, string str, DateTime expirationDateTime);

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        void Set<TEntity>(string key, TEntity entity, DateTime expirationDateTime) where TEntity : class;


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="str">内容对象</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        void Set(string key, string str, TimeSpan slidingExpirationTimeSpan);

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        void Set<TEntity>(string key, TEntity entity, TimeSpan slidingExpirationTimeSpan) where TEntity : class;


        /// <summary>
        /// 移除缓存(根据缓存Key)
        /// </summary>
        /// <param name="key">键Key</param>
        void Remove(string key);

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        void Clear();
    }
}
