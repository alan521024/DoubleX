using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 集合辅助类
    /// </summary>
    public static class CollectionHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        /// <summary>
        /// 移除所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IList<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            if (source != null)
            {
                var items = source.Where(predicate).ToList();
                foreach (var item in items)
                {
                    source.Remove(item);
                }
                return items;
            }
            return source as IList<T>;
        }

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion
    }

}
