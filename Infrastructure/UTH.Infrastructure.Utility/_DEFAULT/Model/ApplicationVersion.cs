using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 应用程序版本信息
    /// </summary>
    public class ApplicationVersion
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        public Version No { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Descript { get; set; }

        /// <summary>
        /// 更新类型
        /// </summary>
        public EnumUpdateType UpdateType { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseDt { get; set; }


        /// <summary>
        /// 版本大小(KB)
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileAddress { get; set; }

        /// <summary>
        /// 文件MD5(用于校验)
        /// </summary>
        public string FileMd5 { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
}
