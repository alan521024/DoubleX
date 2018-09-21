using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 资源文件辅助操作
    /// </summary>
    public class ResourceHelper
    {
        public static void ToJsonFile()
        {

        }


        /// <summary>
        /// 资源文件生成JSON内容
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="properties"></param>
        /// <param name="manager"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToJsonContent(string namespaceName, PropertyInfo[] properties, ResourceManager manager, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(namespaceName) || manager == null | culture == null)
            {
                return string.Empty;
            }

            //var 命名空间/对象属性根名称 生成
            var nameArr = namespaceName.Split('.');
            var nameItems = new List<string>();
            for (int i = 0; i < nameArr.Length; i++)
            {
                nameItems.Add(String.Join(".", nameArr.Take(i + 1).ToArray()));
            }
            var namespaceBuild = new StringBuilder();
            foreach (var item in nameItems)
            {
                namespaceBuild.AppendFormat("{0} = {0} || {{}},", item);
            }
            namespaceBuild = namespaceBuild.TrimEnd(',');

            //资源属性内容
            var keyvalueString = properties.Select(x =>
            {
                //对象为资源文件（涉及语言环境）
                if (manager != null)
                {
                    return string.Format("{0}: \"{1}\"", x.Name, manager.GetString(x.Name, culture));
                }
                return string.Format("{0}: \"{1}\"", x.Name, x.GetValue(null));

            }).ToArray();

            return string.Format("var {0};{1} = {{{2}}}", namespaceBuild.ToString(), namespaceName, String.Join(",", keyvalueString));
        }
    }
}
