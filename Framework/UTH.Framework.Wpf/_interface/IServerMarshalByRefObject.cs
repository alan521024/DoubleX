namespace UTH.Framework.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 远程服务对象接口
    /// </summary>
    public interface IServerMarshalByRefObject
    {
        /// <summary>
        /// 是否连接
        /// </summary>
        bool IsConnection { get; set; }
    }
}
