using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 流辅助类
    /// </summary>
    public static class StreamHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取字节流
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Stream Get(string content, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            if (encoding == null)
                encoding = Encoding.UTF8;

            byte[] strBytes = encoding.GetBytes(content);
            if (strBytes == null)
                return null;
            return new MemoryStream(strBytes);
        }

        /// <summary>
        /// 获取字节流
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Stream流</returns>
        public static Stream Get(byte[] bytes)
        {
            if (bytes == null)
                return null;
            return new MemoryStream(bytes);
        }

        #endregion

        #region 扩展方法

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取流根据字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Stream GetByFilePath(string path, Encoding encoding = null)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ////可尝试设置分块读取
                //byte[] bytes = new byte[fileStream.Length];
                //fileStream.Read(bytes, 0, bytes.Length);
                //fileStream.Close();
                //Stream stream = new MemoryStream(bytes);
                //return stream;
                return stream;
            }
        }

        #endregion

    }
}
