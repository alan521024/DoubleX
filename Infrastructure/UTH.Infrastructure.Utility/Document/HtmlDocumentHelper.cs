using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Html文件辅助操作
    /// </summary>
    public static class HtmlDocumentHelper
    {
        /// <summary>
        /// 加载HTML文档
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlDocument Load(string html)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xPath">eg:"//h2[@id='summary']"</param>
        /// <returns></returns>
        public static HtmlNode GetNode(HtmlDocument doc, string xPath)
        {
            if (doc == null || (doc != null && doc.DocumentNode == null))
                return null;

            return doc.DocumentNode.SelectSingleNode(xPath);
        }

        /// <summary>
        /// 获取节点子集合
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static HtmlNodeCollection FindChildNodes(HtmlDocument doc, string xPath)
        {
            if (doc == null || (doc != null && doc.DocumentNode == null))
                return null;

            var currentNode = doc.DocumentNode.SelectSingleNode(xPath);
            if (currentNode == null)
                return null;

            return currentNode.ChildNodes;
        }
    }
}
