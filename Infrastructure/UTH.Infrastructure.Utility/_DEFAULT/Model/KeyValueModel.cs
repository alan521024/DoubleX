using System;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Key/Value实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class KeyValueModel<TKey, TValue>
    {
        public KeyValueModel() { }
        public KeyValueModel(TKey k, TValue v)
        {
            Key = k;
            Value = v;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }

    /// <summary>
    /// Key/Value实体
    /// </summary>
    [Serializable]
    public class KeyValueModel<TValue> : KeyValueModel<string, TValue>
    {
        public KeyValueModel() { }
        public KeyValueModel(string key, TValue value) : base(key, value) { }
    }

    /// <summary>
    /// Key/Value实体
    /// </summary>
    [Serializable]
    public class KeyValueModel : KeyValueModel<string, string>
    {
        public KeyValueModel() { }
        public KeyValueModel(string key, string value) : base(key, value) { }
    }

    /// <summary>
    /// Key/TValue1,TValue2实体
    /// </summary>
    [Serializable]
    public class KeyValueModel<TKey, TValue1, TValue2>
    {
        public KeyValueModel() { }
        public KeyValueModel(TKey k, TValue1 v1)
        {
            Key = k;
            Value1 = v1;
        }
        public KeyValueModel(TKey k, TValue1 v1, TValue2 v2)
        {
            Key = k;
            Value1 = v1;
            Value2 = v2;
        }

        public TKey Key { get; set; }

        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
    }
}
