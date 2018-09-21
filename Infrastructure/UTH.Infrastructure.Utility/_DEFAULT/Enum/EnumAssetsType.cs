namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum EnumAssetsType
    {
        /// <summary>
        /// 无类别
        /// </summary>
        Default = 0,
        /// <summary>
        /// 应用更新包
        /// </summary>
        AppUpdate = 1,
        /// <summary>
        /// 内容资源(含内容信息资料的文档，图片，影像)
        /// </summary>
        Content = 2
    }
}
