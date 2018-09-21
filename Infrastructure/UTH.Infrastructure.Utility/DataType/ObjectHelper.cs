using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 对象辅助类
    /// </summary>
    public static class ObjectHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取类型值
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public static dynamic GetTypeValue<TType>(dynamic value = null)
        {
            string objType = typeof(TType).FullName;

            if (objType == "System.String" || objType == "Microsoft.Extensions.Primitives.StringValues")
            {
                return "";
            }
            else if (objType == "System.DateTime")
            {
                return DateTimeHelper.DefaultDateTime;
            }
            else if (objType == "System.Guid")
            {
                return Guid.NewGuid();
            }
            else if (objType == "System.Int32" || objType == "System.Decimal" || objType == "System.Double")
            {
                return 0;
            }
            return null;
        }


        #endregion

        #region 扩展方法

        public static TEntity Parse<TEntity>(object value)
        {
            if (value == null || value is DBNull) return default(TEntity);
            if (value is TEntity) return (TEntity)value;

            var type = typeof(TEntity);
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum())
            {
                if (value is float || value is double || value is decimal)
                {
                    value = Convert.ChangeType(value, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
                }
                return (TEntity)Enum.ToObject(type, value);
            }

            //typeHandlers 为类型字典，将所有类型可以放入一个类型库存，并尝试转换(暂无)
            //if (typeHandlers.TryGetValue(type, out ITypeHandler handler))
            //{
            //    return (TEntity)handler.Parse(type, value);
            //}
            return (TEntity)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        public static TEntity As<TEntity>(this object obj) where TEntity : class
        {
            return (TEntity)obj;
        }

        public static TEntity To<TEntity>(this object obj) where TEntity : struct
        {
            return (TEntity)Convert.ChangeType(obj, typeof(TEntity), CultureInfo.InvariantCulture);
        }

        public static bool IsIn<TEntity>(this TEntity item, params TEntity[] list)
        {
            return list.Contains(item);
        }

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <param name="parameters">构造函数参数</param>
        /// <returns></returns>
        public static TEntity CreateInstance<TEntity>(string nameSpace, string className, object[] parameters)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                object ect = Assembly.GetExecutingAssembly().CreateInstance(fullName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (TEntity)ect;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(TEntity);
            }
        }

        #region Object 对象

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetValue(object obj, string fieldName)
        {
            if (obj.IsNull())
                return null;

            PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
            if (propertyInfo.IsNull())
                return null;

            return propertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="fieldValue">属性值</param>
        public static void SetValue(object obj, string fieldName, object value)
        {
            if (obj.IsNull())
                return;

            PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
            if (propertyInfo.IsNull())
                return;

            if (propertyInfo.PropertyType.IsType("System.String"))
            {
                propertyInfo.SetValue(obj, value, null);
                return;
            }
            else if (propertyInfo.PropertyType.IsType("System.Boolean"))
            {
                propertyInfo.SetValue(obj, BoolHelper.Get(value), null);
                return;
            }
            else if (propertyInfo.PropertyType.IsType("System.Int32"))
            {
                propertyInfo.SetValue(obj, IntHelper.Get(value), null);
                return;
            }
            else if (propertyInfo.PropertyType.IsType("System.Decimal"))
            {
                propertyInfo.SetValue(obj, DecimalHelper.Get(value), null);
                return;
            }
            else if (propertyInfo.PropertyType.IsType("System.Guid"))
            {
                propertyInfo.SetValue(obj, GuidHelper.Get(value), null);
                return;
            }
            else if (propertyInfo.PropertyType.IsType("System.Nullable`1[System.DateTime]"))
            {
                if (!string.IsNullOrWhiteSpace(value.ToString()))
                {
                    try
                    {
                        propertyInfo.SetValue(
                            obj,
                            (DateTime?)DateTime.ParseExact(value.ToString(), "yyyy-MM-dd HH:mm:ss", null), null);
                    }
                    catch
                    {
                        propertyInfo.SetValue(obj, (DateTime?)DateTime.ParseExact(value.ToString(), "yyyy-MM-dd", null), null);
                    }
                }
                else
                    propertyInfo.SetValue(obj, null, null);
                return;
            }
            else
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }

        #endregion

        #region Attribute 属性

        /// <summary>
        /// 获取成员属性(eg: 某Type.GetSingleAttributeOrNull<XXX>())
        /// </summary>
        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true) where TAttribute : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
            if (attrs.Length > 0)
            {
                return (TAttribute)attrs[0];
            }

            return default(TAttribute);
        }

        /// <summary>
        /// 获取成员属性(不存在从基本中查找)
        /// </summary>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(this Type type, bool inherit = true) where TAttribute : Attribute
        {
            var attr = type.GetSingleAttributeOrNull<TAttribute>();
            if (attr != null)
            {
                return attr;
            }

            if (type.BaseType == null)
            {
                return null;
            }

            return type.BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(inherit);
        }

        /// <summary>
        /// 尝试获取一个为类成员定义的属性，它声明类型包括继承属性。
        /// </summary>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true) where TAttribute : Attribute
        {
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取方法属性，如未找到在继承链中继续查找
        /// </summary>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true) where TAttribute : Attribute
        {
            //Get attribute on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            //Get attribute from class
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        #endregion

        #endregion
    }
}