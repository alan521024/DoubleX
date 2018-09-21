using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public static class JsonHelper
    {
        #region 属性变量

        /// <summary>
        /// 默认格式化配置
        /// </summary>
        public static JsonSerializerSettings DefaultSetting
        {
            get
            {
                JsonSerializerSettings jsetting = new JsonSerializerSettings();

                //对象格式
                jsetting.Formatting = Formatting.None; //是否带\r\n(Indented 如果带预览查看有结构)

                //时间
                jsetting.Converters.Insert(0, JsonDateTimeConverter);

                //json 属性名称 原格式：new DefaultContractResolver(),  驼峰式： new CamelCasePropertyNamesContractResolver() 但是是javascript 首字母小写形式
                jsetting.ContractResolver = new CamelCasePropertyNamesContractResolver();

                // 循环引用
                jsetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                ////GUID
                //jsetting.Converters.Add(new GuidConverter());
                ////因类型判断问题这里不统一注册，在需要GUID序列化的地方增加[JsonConverter(typeof(GuidConverter))]  

                ////Bool
                //jsetting.Converters.Add(new BoolConverter());

                //接口或继承类ref:http://www.cnblogs.com/OpenCoder/p/4524786.html
                //jsetting.TypeNameHandling = TypeNameHandling.Auto;

                //空值忽略
                //jsetting.NullValueHandling = NullValueHandling.Ignore;

                return jsetting;
            }
        }

        /// <summary>
        /// json 序例化时间处理
        /// </summary>
        public static IsoDateTimeConverter JsonDateTimeConverter
        {
            get
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                timeFormat.Culture = CultureInfo.InvariantCulture;
                return timeFormat;
            }
        }

        /// <summary>
        /// 默认格式化配置
        /// </summary>
        public static void Configure(JsonSerializerSettings serializerSettings)
        {
            if (serializerSettings.IsNull())
                return;

            serializerSettings.Formatting = DefaultSetting.Formatting;
            serializerSettings.Converters.Insert(0, JsonDateTimeConverter);
            serializerSettings.ContractResolver = DefaultSetting.ContractResolver;
            serializerSettings.ReferenceLoopHandling = DefaultSetting.ReferenceLoopHandling;
            serializerSettings.NullValueHandling = DefaultSetting.NullValueHandling;

        }

        #endregion

        #region 操作方法

        /// <summary>
        /// 将字符串序列化成对象
        /// </summary>
        public static object Deserialize(string str, Type type = null)
        {
            object returnObj = null;
            if (!string.IsNullOrWhiteSpace(str))
            {
                returnObj = JsonConvert.DeserializeObject(str, type, DefaultSetting);
            }
            return returnObj;
        }

        /// <summary>
        /// 将对象序列化成对象
        /// </summary>
        public static TEntity Deserialize<TEntity>(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return default(TEntity);
            }
            return JsonConvert.DeserializeObject<TEntity>(str, DefaultSetting);
        }

        /// <summary>
        /// 将JObject对象序列化成对象
        /// </summary>
        public static TEntity Deserialize<TEntity>(JObject obj)
        {
            string jsonString = obj.ToString();
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(TEntity);
            }
            return JsonConvert.DeserializeObject<TEntity>(jsonString, DefaultSetting);
        }

        /// <summary>
        /// 将JObject对象序列化成对象
        /// </summary>
        public static TEntity Deserialize<TEntity>(JArray obj)
        {
            string jsonString = obj.ToString();
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(TEntity);
            }
            return JsonConvert.DeserializeObject<TEntity>(jsonString, DefaultSetting);
        }

        /// <summary>
        /// 将对象序列化成对象(处理异常)
        /// </summary>
        public static TEntity TryDeserialize<TEntity>(string str) where TEntity : class
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    return JsonConvert.DeserializeObject<TEntity>(str, DefaultSetting);
                }
            }
            catch (Exception ex) { }

            return default(TEntity);
        }

        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, DefaultSetting);
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取JObject的Token Value
        /// </summary>
        public static int GetInt(this JObject obj, string key)
        {
            return IntHelper.Get(GetString(obj, key));
        }

        /// <summary>
        /// 获取JObject的Token Value
        /// </summary>
        public static string GetString(this JObject obj, string key)
        {
            if (obj != null && obj.Count > 0)
            {
                JToken token = obj.GetValue(key, StringComparison.InvariantCultureIgnoreCase);
                return token == null ? "" : token.ToString();
            }
            return "";
        }


        /// <summary>
        /// 获取JObject的Token Value
        /// </summary>
        public static Guid GetGuid(this JObject obj, string key)
        {
            Guid value = Guid.Empty;
            if (obj != null && obj.Count > 0)
            {
                JToken token = obj.GetValue(key, StringComparison.InvariantCultureIgnoreCase);
                if (token != null)
                {
                    Guid.TryParse(token.ToString(), out value);
                }
            }
            return value;
        }


        /// <summary>
        /// 获取JObject的Token Value 并转为对象
        /// </summary>
        public static TEntity GetItem<TEntity>(this JObject obj, string key)
        {
            if (obj != null && obj.Count > 0)
            {
                JToken token = obj.GetValue(key, StringComparison.InvariantCultureIgnoreCase);
                if (token != null)
                {
                    return token.ToObject<TEntity>();
                }
            }
            return default(TEntity);
        }


        /// <summary>
        /// 合并两个JObject对象(将Json字符串序列成JObject)(只支持第一级差异对比)
        /// </summary>
        /// <param name="obj">源JObject对象</param>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>合并后的JObject</returns>
        public static JObject BuildJObject(this JObject obj, string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return obj;
            }
            JObject obj2 = Deserialize<JObject>(str);
            if (obj2 == null)
                return obj;
            return BuildJObject(obj, obj2);
        }

        /// <summary>
        /// 合并两个JObject对象(将Json字符串序列成JObject)(只支持第一级差异对比)
        /// </summary>
        /// <param name="obj1">对象1</param>
        /// <param name="obj2">对象2</param>
        /// <returns>合并后的JObject</returns>
        public static JObject BuildJObject(this JObject obj1, JObject obj2)
        {
            if (obj1 == null)
                obj1 = new JObject();
            if (obj2 == null)
                return obj1;

            var newObj = new JObject();

            //设置源
            foreach (JProperty item in obj1.Properties())
            {
                newObj.Add(item);
            }

            //设置目标
            foreach (JProperty item in obj2.Properties())
            {
                var sObj = newObj.Properties().FirstOrDefault(x => x.Name.ToLower() == item.Name.ToLower());
                if (sObj != null)
                {
                    newObj[sObj.Name] = item.Value;
                }
                else
                {
                    newObj.Add(item.Name, item.Value);
                }
            }
            return newObj;
        }

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion
    }

    #region  时间处理(source from abp)

    //public class JsonDateTimeConverter : IsoDateTimeConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
    //        {
    //            return true;
    //        }

    //        return false;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

    //        if (date.HasValue)
    //        {
    //            return Clock.Normalize(date.Value);
    //        }

    //        return null;
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var date = value as DateTime?;
    //        base.WriteJson(writer, date.HasValue ? Clock.Normalize(date.Value) : value, serializer);
    //    }
    //}

    #endregion 
}
