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
    /// 基于Redis的缓存服务
    /// </summary>
    public class RedisCachingService : ICachingService
    {

        public RedisCachingService(ConnectionModel model)
        {
            model.CheckNull();
            connection = model;
            if (!model.ConnectionString.IsEmpty())
            {
                client = RedisHelper.GetClient(model.ConnectionString, IntHelper.Get(model.DbName));
            }
        }

        protected ConnectionModel connection { get; private set; }

        /// <summary>
        /// redis客户端
        /// </summary>
        protected RedisClient client { get; private set; }


        /// <summary>
        /// 获取字符串缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>string</returns>
        public string Get(string key)
        {
            if (key.IsEmpty())
                return string.Empty;

            return client.StringGet(key);
        }

        /// <summary>
        /// 获取对象缓存内容
        /// </summary>
        /// <typeparam name="TEntity">返回类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns>返回的类型对象</returns>
        public TEntity Get<TEntity>(string key) where TEntity : class
        {
            string jsonStr = Get(key);
            if (!jsonStr.IsEmpty())
            {
                return JsonHelper.Deserialize<TEntity>(jsonStr);
            }
            return default(TEntity);
        }


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="string">字符串</param>
        public void Set(string key, string str)
        {
            if (key.IsEmpty())
                return;

            if (connection.ExpireTime > 0)
            {
                Set(key, str, TimeSpan.FromSeconds(connection.ExpireTime));
            }
            else
            {
                client.StringSet(key, str);
            }
        }

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        public void Set<TEntity>(string key, TEntity entity) where TEntity : class
        {
            if (key.IsEmpty())
                return;

            string jsonStr = JsonHelper.Serialize(entity);
            Set(key, jsonStr);
        }


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="str">字符串</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        public void Set(string key, string str, DateTime expirationDateTime)
        {
            if (key.IsEmpty())
                return;

            var expiryTimespan = DateTimeHelper.GetToDifference(expirationDateTime);
            client.StringSet(key, str, TimeSpan.FromSeconds(Math.Abs(expiryTimespan.TotalSeconds)));
        }

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        public void Set<TEntity>(string key, TEntity entity, DateTime expirationDateTime) where TEntity : class
        {
            if (key.IsEmpty())
                return;

            string jsonStr = JsonHelper.Serialize(entity);
            Set(key, jsonStr, expirationDateTime);
        }


        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="str">字符串</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        public void Set(string key, string str, TimeSpan slidingExpirationTimeSpan)
        {
            if (key.IsEmpty())
                return;

            client.StringSet(key, str, expiry: slidingExpirationTimeSpan);
        }

        /// <summary>
        /// 设置对象缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        public void Set<TEntity>(string key, TEntity entity, TimeSpan slidingExpirationTimeSpan) where TEntity : class
        {
            if (key.IsEmpty())
                return;

            string jsonStr = JsonHelper.Serialize(entity);
            Set(key, jsonStr, slidingExpirationTimeSpan);
        }


        /// <summary>
        /// 移除缓存(根据缓存Key)
        /// </summary>
        /// <param name="key">键Key</param>
        public void Remove(string key)
        {
            client.StringDelete(key);
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public void Clear()
        {

        }
    }
}
