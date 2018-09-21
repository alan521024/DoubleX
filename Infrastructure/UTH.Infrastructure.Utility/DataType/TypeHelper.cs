using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 类型、方法，成员 辅助类
    /// </summary>
    public static class TypeHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        /// <summary>
        /// 类型匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsType(this Type type, string typeName)
        {
            if (type.ToString() == typeName)
                return true;
            if (type.ToString() == "System.Object")
                return false;

            return IsType(type.BaseType, typeName);
        }

        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"> 要处理的类型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {
            if (type == null)
                return false;

            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 判断类型是否为集合类型
        /// </summary>
        /// <param name="type">要处理的类型</param>
        /// <returns>是返回True，不是返回False</returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type == null)
                return false;

            if (type == typeof(string))
            {
                return false;
            }
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// 是否类型是否为枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }

        /// <summary>
        /// 判断当前泛型类型是否可由指定类型的实例填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericType, Type type, bool typeIsOnlyGenericType = true)
        {
            if (genericType == null)
                throw new NullReferenceException("genericType is null");


            if (type == null)
                throw new NullReferenceException("type is null");

            if (!genericType.IsGenericType)
            {
                throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");
            }

            //type仅泛型类型
            if (typeIsOnlyGenericType && !type.IsGenericType)
            {
                return false;
            }

            List<Type> allOthers = new List<Type> { type };
            if (genericType.IsInterface)
            {
                allOthers.AddRange(type.GetInterfaces());
            }

            foreach (var other in allOthers)
            {
                Type cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType)
                    {
                        cur = cur.GetGenericTypeDefinition();
                    }
                    if (cur.IsSubclassOf(genericType) || cur == genericType)
                    {
                        return true;
                    }
                    cur = cur.BaseType;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断当前泛型参数类型(可判断多个泛型参数是否多个类型中的某一个)
        /// </summary>
        /// <param name="genericType"></param>
        /// <param name="argumentTypes"></param>
        /// <returns></returns>
        public static bool IsGenericArgumentsFrom(this Type genericType, params Type[] types)
        {
            if (genericType == null || types == null)
                return false;

            if (types.Length == 0)
                return false;

            Type[] argumentTypes = genericType.GetGenericArguments();
            if (argumentTypes.Length == 0)
                return false;

            foreach (var arg in argumentTypes)
            {
                foreach (var t in types)
                {
                    if (t.IsAssignableFrom(arg))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// 子类:A 父:XX[(A.IsBaseFrom(XX)]
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="baseType">要判断的基类型</param>
        /// <returns></returns>
        public static bool IsBaseFrom(this Type type, Type baseType)
        {
            if (type.IsGenericTypeDefinition)
            {
                return IsGenericAssignableFrom(baseType, type);
            }
            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// 子类:A 父:XX[(A.IsBaseFrom<XX>()]
        /// </summary>
        /// <typeparam name="TBaseType">要判断的基类型</typeparam>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsBaseFrom<TBaseType>(this Type type)
        {
            if (type == null)
                return false;
            return type.IsBaseFrom(typeof(TBaseType));
        }

        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// 子类：A  父类：XX [(XX.IsBaseTo(A)]
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <param name="baseType">当前基类型</param>
        /// <returns></returns>
        public static bool IsBaseTo(this Type baseType, Type type)
        {
            if (type.IsGenericTypeDefinition)
            {
                return IsGenericAssignableFrom(baseType, type);
            }
            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 返回当前类型是否是指定基类的派生类
        /// 子类：A  父类：XX [(XX.IsBaseTo<A>()]
        /// </summary>
        /// <typeparam name="TType">要判断的类型</typeparam>
        /// <param name="basetype">当前基类型</param>
        /// <returns></returns>
        public static bool IsBaseTo<TType>(this Type basetype)
        {
            if (basetype == null)
                return false;
            return basetype.IsBaseTo(typeof(TType));
        }


        /// <summary>
        /// 方法是否是异步
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            if (method == null)
                return false;
            return method.ReturnType == typeof(Task)
                || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            if (memberInfo == null)
                return false;
            return memberInfo.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 是否销毁方法
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool IsIDisposableMethod(MethodInfo methodInfo)
        {
            // Ideally we do not want Dispose method to be exposed as an action. However there are some scenarios where a user
            // might want to expose a method with name "Dispose" (even though they might not be really disposing resources)
            // Example: A controller deriving from MVC's Controller type might wish to have a method with name Dispose,
            // in which case they can use the "new" keyword to hide the base controller's declaration.

            // Find where the method was originally declared
            var baseMethodInfo = methodInfo.GetBaseDefinition();
            var declaringTypeInfo = baseMethodInfo.DeclaringType.GetTypeInfo();

            return
                (typeof(IDisposable).GetTypeInfo().IsAssignableFrom(declaringTypeInfo) &&
                 declaringTypeInfo.GetRuntimeInterfaceMap(typeof(IDisposable)).TargetMethods[0] == baseMethodInfo);
        }


        /// <summary>
        /// 值类型是否为原始类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitiveExtendedIncludingNullable(Type type)
        {
            if (IsPrimitiveExtended(type))
            {
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtended(type.GenericTypeArguments[0]);
            }

            return false;
        }

        private static bool IsPrimitiveExtended(Type type)
        {
            if (type.IsPrimitive)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 由类型的Nullable类型返回实际类型
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetNonNummableType(Type type)
        {
            if (type.IsNullableType())
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        /// <summary>
        /// 通过类型转换器获取Nullable类型的基础类型
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(Type type)
        {
            if (type.IsNullableType())
            {
                NullableConverter nullableConverter = new NullableConverter(type);
                return nullableConverter.UnderlyingType;
            }
            return type;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            if (memberInfo == null)
                return default(T);

            var descripts = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return descripts.FirstOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>返回所有指定Attribute特性的数组</returns>
        public static T[] GetAttributes<T>(MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            if (memberInfo == null)
                return new T[] { };
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        /// <summary>
        /// 获取指定属性信息（需要明确指定属性类型，但不存在装箱与拆箱）
        /// eg: var ps1 = GetPropertyInfos<T>(t => t);//获取类型所有属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="select"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T, TR>(Expression<Func<T, TR>> select)
        {
            var body = select.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                var o = (body as UnaryExpression).Operand;
                return (o as MemberExpression).Member as PropertyInfo;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
                return (body as MemberExpression).Member as PropertyInfo;
            }
            return null;
        }

        /// <summary>
        /// 获取指定属性信息（非String类型存在装箱与拆箱）
        /// eg:var p = GetPropertyInfo<T>(t => t.Age);//获取指定属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T, dynamic>> select)
        {
            var body = select.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                var o = (body as UnaryExpression).Operand;
                return (o as MemberExpression).Member as PropertyInfo;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
                return (body as MemberExpression).Member as PropertyInfo;
            }
            return null;
        }

        /// <summary>
        /// 获取类型的所有属性信息
        /// eg:var ps2 = GetPropertyInfos<People>(t => new { t.Name, t.Age });//获取部份属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertyInfos<T>(Expression<Func<T, dynamic>> select)
        {
            var body = select.Body;
            if (body.NodeType == ExpressionType.Parameter)
            {
                return (body as ParameterExpression).Type.GetProperties();
            }
            else if (body.NodeType == ExpressionType.New)
            {
                return (body as NewExpression).Members.Select(m => m as PropertyInfo).ToArray();
            }
            return null;
        }

        /// <summary>
        /// 获取类型的Description特性描述信息
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="inherit">是否搜索类型的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
        public static string GetDescription(Type type, bool inherit = false)
        {
            if (type == null)
                return "";
            DescriptionAttribute desc = GetAttribute<DescriptionAttribute>(type, inherit);
            return desc == null ? type.FullName : desc.Description;
        }

        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDescription(MemberInfo member, bool inherit = false)
        {
            if (member == null)
                return "";

            DescriptionAttribute desc = GetAttribute<DescriptionAttribute>(member, inherit);
            if (desc != null)
            {
                return desc.Description;
            }
            DisplayNameAttribute displayName = GetAttribute<DisplayNameAttribute>(member, inherit);
            if (displayName != null)
            {
                return displayName.DisplayName;
            }

            //DisplayAttribute display = GetAttribute<DisplayAttribute>(member, inherit);
            //if (display != null)
            //{
            //    return display.Name;
            //}
            return member.Name;
        }

        #endregion
    }
}