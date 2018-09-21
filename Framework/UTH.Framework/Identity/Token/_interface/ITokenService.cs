using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace UTH.Framework
{
    /// <summary>
    /// Token操作接口
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        string Generate(string accountId, string account, string mobile, string email, string realName, string role,int type,int status);

        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool Remove(string token);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string Refresh(string token);

        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        EnumCode Verify(string token);

        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="storeModel">TokenModel(store)</param>
        /// <returns></returns>
        EnumCode Verify(string token, out TokenModel storeModel);

        /// <summary>
        /// 解析Token
        /// </summary>
        JwtSecurityToken Resolve(string token);

    }
}
