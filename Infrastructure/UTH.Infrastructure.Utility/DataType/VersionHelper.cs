namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 版本辅助类
    /// </summary>
    public static class VersionHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取版本信息
        /// </summary>
        public static Version Get()
        {
            Assembly assem = Assembly.GetEntryAssembly();
            return Get(assem);
        }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Version Get(Assembly assem)
        {
            assem.CheckNull();
            AssemblyName assemName = assem.GetName();
            assemName.CheckNull();
            return assemName.Version;
        }

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取可空类型值(为空时返回默认)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetByNullType(bool? value, bool defaultNullValue = false)
        {
            if (value == null)
                return defaultNullValue;

            return value.Value;
        }

        #endregion
    }
}
