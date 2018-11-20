namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 应用程序详细信息
    /// </summary>
    public class AppDetail
    {
        /// <summary>
        /// 应用信息
        /// </summary>
        public AppDTO Application { get; set; }

        /// <summary>
        /// 最新版本
        /// </summary>
        public AppVersionDTO Versions { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
    }
}
