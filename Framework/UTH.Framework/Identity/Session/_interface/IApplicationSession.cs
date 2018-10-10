using System;
using System.Collections.Generic;
using System.Text;

namespace UTH.Framework
{
    /// <summary>
    /// 应用程序用户会话信息接口
    /// </summary>
    public interface IApplicationSession
    {

        /// <summary>
        /// 访问信息
        /// </summary>
        IAccessor Accessor { get; set; }

        /// <summary>
        /// 访问用户
        /// </summary>
        IIdentifier User { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        bool IsAuthenticated { get; set; }


        /// <summary>
        /// 检查企业参数
        /// </summary>
        /// <param name="organize"></param>
        /// <param name="isThrow"></param>
        /// <returns></returns>
        bool CheckAccountOrganize(string organize, bool isThrow = true);


    }
}
