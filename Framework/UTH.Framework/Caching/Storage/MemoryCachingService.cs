

//namespace UTH.Core
//{//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using UTH.Infrastructure.Resource;
//using UTH.Infrastructure.Resource.Culture;
//using UTH.Infrastructure.Utility;
//    /// <summary>
//    /// 基于内存的缓存服务
//    /// </summary>
//    public class MemoryCachingService :  ICachingService
//    {
//        /// <summary>
//        /// 会话提共者
//        /// </summary>
//        protected readonly HttpRuntimeCaching manage = new HttpRuntimeCaching();

//        /// <summary>
//        /// 会话配置信息
//        /// </summary>
//        protected StoreModel Setting { get; set; }

//        public RuntimeConversationService(StoreModel model)
//        {
//            Setting = model;
//        }


//        /// <summary>
//        /// 获取会话(根据会话Key)
//        /// </summary>
//        /// <param name="configKey">会话Key</param>
//        /// <returns>返回的类型对象</returns>
//        public object Get(string key)
//        {
//            return manage.Get(key);
//        }

//        /// <summary>
//        /// 获取会话(根据会话Key)
//        /// </summary>
//        /// <typeparam name="TEntity">返回的类型</typeparam>
//        /// <param name="configKey">会话Key</param>
//        /// <returns>返回的类型对象</returns>
//        public TEntity Get<TEntity>(string key) where TEntity : class
//        {
//            return manage.Get<TEntity>(key);
//        }

//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="obj">内容对象</param>
//        public void Set(string key, object obj)
//        {
//            if (Setting.TimeOut > 0)
//            {
//                switch (Setting.ExpiresType)
//                {
//                    case EnumExpiresType.Sliding:
//                        Set(key, obj, new TimeSpan(Setting.TimeOut * 1000));
//                        break;
//                    case EnumExpiresType.Expiration:
//                    default:
//                        Set(key, obj, DateTime.Now.AddSeconds(Setting.TimeOut));
//                        break;
//                }
//            }
//            else
//            {
//                manage.Set(key, obj);
//            }
//        }

//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="entity">内容对象</param>
//        public void Set<TEntity>(string key, TEntity entity) where TEntity : class
//        {
//            if (Setting.TimeOut > 0)
//            {
//                switch (Setting.ExpiresType)
//                {
//                    case EnumExpiresType.Sliding:
//                        Set<TEntity>(key, entity, new TimeSpan(Setting.TimeOut * 1000));
//                        break;
//                    case EnumExpiresType.Expiration:
//                    default:
//                        Set<TEntity>(key, entity, DateTime.Now.AddSeconds(Setting.TimeOut));
//                        break;
//                }
//            }
//            else
//            {
//                manage.Set<TEntity>(key, entity);
//            }
//        }

//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="obj">内容对象</param>
//        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
//        public void Set(string key, object obj, DateTime expirationDateTime)
//        {
//            manage.Set(key, obj, expirationDateTime);
//        }

//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="entity">内容对象</param>
//        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
//        public void Set<TEntity>(string key, TEntity entity, DateTime expirationDateTime) where TEntity : class
//        {
//            manage.Set<TEntity>(key, entity, expirationDateTime);
//        }


//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="obj">内容对象</param>
//        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
//        public void Set(string key, object obj, TimeSpan slidingExpirationTimeSpan)
//        {
//            manage.Set(key, obj, slidingExpirationTimeSpan);
//        }


//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="entity">内容对象</param>
//        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
//        public void Set<TEntity>(string key, TEntity entity, TimeSpan slidingExpirationTimeSpan) where TEntity : class
//        {
//            manage.Set<TEntity>(key, entity, slidingExpirationTimeSpan);
//        }


//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="obj">内容对象</param>
//        /// <param name="filePaths">文件依赖</param>
//        public void Set(string key, object obj, List<string> filePaths)
//        {
//            manage.Set(key, obj, filePaths);
//        }

//        /// <summary>
//        /// 设置会话
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        /// <param name="entity">内容对象</param>
//        /// <param name="filePaths">文件依赖</param>
//        public void Set<TEntity>(string key, TEntity entity, List<string> filePaths) where TEntity : class
//        {
//            manage.Set<TEntity>(key, entity, filePaths);
//        }


//        /// <summary>
//        /// 移除会话(根据会话Key)
//        /// </summary>
//        /// <param name="configKey">键Key</param>
//        public void Remove(string key)
//        {
//            manage.Remove(key);
//        }

//        /// <summary>
//        /// 移除所有会话
//        /// </summary>
//        public void Clear()
//        {
//            manage.Clear();
//        }
//    }
//}
