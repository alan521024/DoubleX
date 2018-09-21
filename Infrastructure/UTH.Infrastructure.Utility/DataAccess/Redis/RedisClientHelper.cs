using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Redis 辅助类
    /// </summary>
    public static class RedisHelper
    {
        //注意这个exp的时间，之前以为是以毫秒计算，所以设置一天过期的话只写了86400000，然而，他这里的最小单位似乎是。。100ns。。也就是1个ticks=100毫微秒=100纳秒。
        //所以应该写成864000000000表示一天。
        //1秒=1000毫秒(ms) 1毫秒=1／1,000秒(s)
        //1秒=1,000,000 微秒(μs) 1微秒=1／1,000,000秒(s)
        //1秒=1,000,000,000 纳秒(ns) 1纳秒=1／1,000,000,000秒(s)
        //1秒=1,000,000,000,000 皮秒(ps) 1皮秒=1／1,000,000,000,000秒(s)

        private readonly static ConcurrentDictionary<string, RedisClient> _clientDictionary =
            new ConcurrentDictionary<string, RedisClient>();

        #region 存储库信息获取

        public static RedisClient GetClient(string connection, int database = 0)
        {
            connection.CheckEmpty();

            string connectionKey = string.Format("{0}_{1}", connection, database);

            RedisClient client = null;
            _clientDictionary.TryGetValue(connectionKey, out client);

            if (client == null)
            {
                client = new RedisClient(connection, database: database);
                _clientDictionary.TryAdd(connectionKey, client);
            }

            if (!client.IsNull() && client.Database != database)
            {
                client.Database = database;
                _clientDictionary[connectionKey] = client;
            }
            return client;
        }

        #endregion

        #region String 操作

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool StringSet(this RedisClient client, string key, string value, TimeSpan? expiry = null)
        {
            return client.GetConnection().GetDatabase(client.Database).StringSet(client.FormatKey(key), value, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static bool StringSet(this RedisClient client, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(client.FormatKey(x.Key), x.Value));
            return client.GetConnection().GetDatabase(client.Database).StringSet(pairs.ToArray());
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool StringSet<TEntity>(this RedisClient client, string key, TEntity value, TimeSpan? expiry = null)
        {
            return client.GetConnection().GetDatabase(client.Database).StringSet(client.FormatKey(key), Serialize(value), expiry);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static string StringGet(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).StringGet(client.FormatKey(key));
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static TEntity StringGet<TEntity>(this RedisClient client, string key, TimeSpan? expiry = null)
        {
            return Deserialize<TEntity>(client.GetConnection().GetDatabase(client.Database).StringGet(client.FormatKey(key)));
        }

        /// <summary>
        /// 移除指定key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool StringDelete(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).KeyDelete(client.FormatKey(key));
        }

        #endregion

        #region String Async

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(this RedisClient client, string key, string value, TimeSpan? expiry = null)
        {
            return await client.GetConnection().GetDatabase(client.Database).StringSetAsync(client.FormatKey(key), value, expiry);
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(this RedisClient client, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(client.FormatKey(x.Key), x.Value));
            return await client.GetConnection().GetDatabase(client.Database).StringSetAsync(pairs.ToArray());
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync<TEntity>(this RedisClient client, string key, TEntity value, TimeSpan? expiry = null)
        {
            return await client.GetConnection().GetDatabase(client.Database).StringSetAsync(client.FormatKey(key), Serialize(value), expiry);
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<string> StringGetAsync(this RedisClient client, string key, string value, TimeSpan? expiry = null)
        {
            return await client.GetConnection().GetDatabase(client.Database).StringGetAsync(client.FormatKey(key));
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<TEntity> StringGetAsync<TEntity>(this RedisClient client, string key, TimeSpan? expiry = null)
        {
            return Deserialize<TEntity>(await client.GetConnection().GetDatabase(client.Database).StringGetAsync(client.FormatKey(key)));
        }

        /// <summary>
        /// 移除指定key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool StringDeleteAsync(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).KeyDeleteAsync(client.FormatKey(key)).Result;
        }

        #endregion

        #region Hash 操作

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HashSet(this RedisClient client, string key, string hashField, string value)
        {
            return client.GetConnection().GetDatabase(client.Database).HashSet(client.FormatKey(key), hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        public static void HashSet(this RedisClient client, string key, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));
            client.GetConnection().GetDatabase(client.Database).HashSet(client.FormatKey(key), entries.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HashSet<TEntity>(this RedisClient client, string key, string hashField, TEntity value)
        {
            return client.GetConnection().GetDatabase(client.Database).HashSet(client.FormatKey(key), hashField, Serialize(value));
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static string HashGet(this RedisClient client, string key, string hashField)
        {
            return client.GetConnection().GetDatabase(client.Database).HashGet(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public static IEnumerable<string> HashGet(this RedisClient client, string key, IEnumerable<string> hashFields)
        {
            var fields = hashFields.Select(x => (RedisValue)x);
            return ConvertStrings(client.GetConnection().GetDatabase(client.Database).HashGet(client.FormatKey(key), fields.ToArray()));
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static TEntity HashGet<TEntity>(this RedisClient client, string key, string hashField)
        {
            return Deserialize<TEntity>(client.GetConnection().GetDatabase(client.Database).HashGet(client.FormatKey(key), hashField));
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static bool HashDelete(this RedisClient client, string key, string hashField)
        {
            return client.GetConnection().GetDatabase(client.Database).HashDelete(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public static long HashDelete(this RedisClient client, string key, IEnumerable<string> hashFields)
        {
            var fields = hashFields.Select(x => (RedisValue)x);
            return client.GetConnection().GetDatabase(client.Database).HashDelete(client.FormatKey(key), fields.ToArray());
        }

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static bool HashExists(this RedisClient client, string key, string hashField)
        {
            return client.GetConnection().GetDatabase(client.Database).HashExists(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<string> HashKeys(this RedisClient client, string key)
        {
            return ConvertStrings(client.GetConnection().GetDatabase(client.Database).HashKeys(client.FormatKey(key)));
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<string> HashValues(this RedisClient client, string key)
        {
            return ConvertStrings(client.GetConnection().GetDatabase(client.Database).HashValues(client.FormatKey(key)));
        }

        #endregion

        #region Hash Async

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> HashSetAsync(this RedisClient client, string key, string hashField, string value)
        {
            return await client.GetConnection().GetDatabase(client.Database).HashSetAsync(client.FormatKey(key), hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        public static async Task HashSetAsync(this RedisClient client, string key, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));
            await client.GetConnection().GetDatabase(client.Database).HashSetAsync(client.FormatKey(key), entries.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> HashSetAsync<TEntity>(this RedisClient client, string key, string hashField, TEntity value)
        {
            return await client.GetConnection().GetDatabase(client.Database).HashSetAsync(client.FormatKey(key), hashField, Serialize(value));
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<string> HashGetAsync(this RedisClient client, string key, string hashField)
        {
            return await client.GetConnection().GetDatabase(client.Database).HashGetAsync(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> HashGetAsync(this RedisClient client, string key, IEnumerable<string> hashFields, string value)
        {
            var fields = hashFields.Select(x => (RedisValue)x);
            return ConvertStrings(await client.GetConnection().GetDatabase(client.Database).HashGetAsync(client.FormatKey(key), fields.ToArray()));
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<TEntity> HashGetAsync<TEntity>(this RedisClient client, string key, string hashField)
        {
            return Deserialize<TEntity>(await client.GetConnection().GetDatabase(client.Database).HashGetAsync(client.FormatKey(key), hashField));
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<bool> HashDeleteAsync(this RedisClient client, string key, string hashField)
        {
            return await client.GetConnection().GetDatabase(client.Database).HashDeleteAsync(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public static async Task<long> HashDeleteAsync(this RedisClient client, string key, IEnumerable<string> hashFields)
        {
            var fields = hashFields.Select(x => (RedisValue)x);
            return await client.GetConnection().GetDatabase(client.Database).HashDeleteAsync(client.FormatKey(key), fields.ToArray());
        }

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public static async Task<bool> HashExistsAsync(this RedisClient client, string key, string hashField)
        {
            return await client.GetConnection().GetDatabase(client.Database).HashExistsAsync(client.FormatKey(key), hashField);
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> HashKeysAsync(this RedisClient client, string key)
        {
            return ConvertStrings(await client.GetConnection().GetDatabase(client.Database).HashKeysAsync(client.FormatKey(key)));
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> HashValuesAsync(this RedisClient client, string key)
        {
            return ConvertStrings(await client.GetConnection().GetDatabase(client.Database).HashValuesAsync(client.FormatKey(key)));
        }

        #endregion

        #region List 操作

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListRightPush(this RedisClient client, string key, string value)
        {
            return client.GetConnection().GetDatabase(client.Database).ListRightPush(client.FormatKey(key), value);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListLeftPush(this RedisClient client, string key, string value)
        {
            return client.GetConnection().GetDatabase(client.Database).ListLeftPush(client.FormatKey(key), value);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListRightPush<TEntity>(this RedisClient client, string key, TEntity value)
        {
            return client.GetConnection().GetDatabase(client.Database).ListRightPush(client.FormatKey(key), Serialize(value));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListLeftPush<TEntity>(this RedisClient client, string key, TEntity value)
        {
            return client.GetConnection().GetDatabase(client.Database).ListLeftPush(client.FormatKey(key), Serialize(value));
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long ListLength(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).ListLength(client.FormatKey(key));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ListLeftPop(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).ListLeftPop(client.FormatKey(key));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ListRightPop(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).ListRightPop(client.FormatKey(key));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TEntity ListLeftPop<TEntity>(this RedisClient client, string key)
        {
            return Deserialize<TEntity>(client.GetConnection().GetDatabase(client.Database).ListLeftPop(client.FormatKey(key)));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TEntity ListRightPop<TEntity>(this RedisClient client, string key)
        {
            return Deserialize<TEntity>(client.GetConnection().GetDatabase(client.Database).ListRightPop(client.FormatKey(key)));
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListRemove(this RedisClient client, string key, string value)
        {
            return client.GetConnection().GetDatabase(client.Database).ListRemove(client.FormatKey(key), value);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static IEnumerable<string> ListRange(this RedisClient client, string key, long start = 0L, long stop = -1L)
        {
            return ConvertStrings(client.GetConnection().GetDatabase(client.Database).ListRange(client.FormatKey(key), start, stop));
        }

        #endregion

        #region  List Async

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync(this RedisClient client, string key, string value)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListRightPushAsync(client.FormatKey(key), value);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync(this RedisClient client, string key, string value)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListLeftPushAsync(client.FormatKey(key), value);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync<TEntity>(this RedisClient client, string key, TEntity value)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListRightPushAsync(client.FormatKey(key), Serialize(value));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync<TEntity>(this RedisClient client, string key, TEntity value)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListLeftPushAsync(client.FormatKey(key), Serialize(value));
        }


        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> ListRangeAsync(this RedisClient client, string key, long start = 0L, long stop = -1L)
        {
            var query = await client.GetConnection().GetDatabase(client.Database).ListRangeAsync(client.FormatKey(key), start, stop);
            return query.Select(x => x.ToString());
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<string> ListLeftPopAsync(this RedisClient client, string key)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListLeftPopAsync(client.FormatKey(key));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<string> ListRightPopAsync(this RedisClient client, string key)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListRightPopAsync(client.FormatKey(key));
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<long> ListRemoveAsync(this RedisClient client, string key, string value)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListRemoveAsync(client.FormatKey(key), value);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<long> ListLengthAsync(this RedisClient client, string key)
        {
            return await client.GetConnection().GetDatabase(client.Database).ListLengthAsync(client.FormatKey(key));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<TEntity> ListLeftPopAsync<TEntity>(this RedisClient client, string key)
        {
            return Deserialize<TEntity>(await client.GetConnection().GetDatabase(client.Database).ListLeftPopAsync(client.FormatKey(key)));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<TEntity> ListRightPopAsync<TEntity>(this RedisClient client, string key)
        {
            return Deserialize<TEntity>(await client.GetConnection().GetDatabase(client.Database).ListRightPopAsync(client.FormatKey(key)));
        }

        #endregion List-async

        #region SortedSet 操作

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(this RedisClient client, string key, string member, double score)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetAdd(client.FormatKey(key), member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IEnumerable<string> SortedSetRangeByRank(this RedisClient client, string key, long start = 0L, long stop = -1L, EnumSortType sort = EnumSortType.Ascending)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetRangeByRank(client.FormatKey(key), start, stop, GetRedisOrderBySortType(sort)).Select(x => x.ToString());
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long SortedSetLength(this RedisClient client, string key)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetLength(client.FormatKey(key));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public static bool SortedSetLength(this RedisClient client, string key, string memebr)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetRemove(client.FormatKey(key), memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd<TEntity>(this RedisClient client, string key, TEntity member, double score)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetAdd(client.FormatKey(key), Serialize(member), score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double SortedSetIncrement(this RedisClient client, string key, string member, double value = 1)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetIncrement(client.FormatKey(key), member, value);
        }

        #endregion

        #region SortedSet Async

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetAddAsync(this RedisClient client, string key, string member, double score)
        {
            return await client.GetConnection().GetDatabase(client.Database).SortedSetAddAsync(client.FormatKey(key), member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> SortedSetRangeByRankAsync(this RedisClient client, string key)
        {
            return ConvertStrings(await client.GetConnection().GetDatabase(client.Database).SortedSetRangeByRankAsync(client.FormatKey(key)));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<long> SortedSetLengthAsync(this RedisClient client, string key)
        {
            return await client.GetConnection().GetDatabase(client.Database).SortedSetLengthAsync(client.FormatKey(key));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetRemoveAsync(this RedisClient client, string key, string memebr)
        {
            return await client.GetConnection().GetDatabase(client.Database).SortedSetRemoveAsync(client.FormatKey(key), memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetAddAsync<TEntity>(this RedisClient client, string key, TEntity member, double score)
        {
            return await client.GetConnection().GetDatabase(client.Database).SortedSetAddAsync(client.FormatKey(key), Serialize(member), score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task<double> SortedSetIncrementAsync(this RedisClient client, string key, string member, double value = 1)
        {
            return client.GetConnection().GetDatabase(client.Database).SortedSetIncrementAsync(client.FormatKey(key), member, value);
        }

        #endregion

        #region 辅助操作

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static TEntity Deserialize<TEntity>(byte[] data)
        {
            if (data == null)
                return default(TEntity);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (TEntity)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private static IEnumerable<string> ConvertStrings<TEntity>(IEnumerable<TEntity> list) where TEntity : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 获取redis框架排序类型
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        private static Order GetRedisOrderBySortType(EnumSortType sortType)
        {
            if (sortType == EnumSortType.Ascending)
                return Order.Ascending;
            return Order.Descending;
        }

        #endregion
    }
}