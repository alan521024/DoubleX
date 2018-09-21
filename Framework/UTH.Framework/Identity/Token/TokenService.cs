using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework
{
    /// <summary>
    /// Token操作服务
    /// </summary>
    public class TokenService : ITokenService
    {
        public TokenService(IAccessor accessor, IApplicationSession current, ITokenStore store)
        {
            Accessor = accessor;
            Current = current;
            Store = store;
        }

        protected IAccessor Accessor { get; set; }

        protected IApplicationSession Current { get; }

        protected ITokenStore Store { get; }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public string Generate(string accountId, string account, string mobile, string email, string realName, string role, int type, int status)
        {
            EngineHelper.Configuration.Authentication.CheckNull();

            var setting = EngineHelper.Configuration.Authentication;
            var nowDt = DateTime.Now;
            var expiresDt = nowDt.AddSeconds(setting.ExpireTime);

            #region claim 说明

            //"jti" => '222we',                                 #非必须。JWT ID。针对当前token的唯一标识
            //"sub" => "jrocket@example.com",                   #非必须。该JWT所面向的用户
            //"iss" => "http://example.org",                    #非必须。issuer 请求实体，可以是发起请求的用户的信息，也可是jwt的签发者。
            //"aud" => "http://example.com",                    #非必须。接收该JWT的一方。
            //"nbf" => 1357000000,                              #非必须。not before。如果当前时间在nbf里的时间之前，则Token不被接受；一般都会留一些余地，比如几分钟。
            //"exp" => "1548333419",                            #非必须。expire 指定token的生命周期。unix时间戳格式
            //"iat" => 1356999524,                              #非必须。issued at。 token创建时间，unix时间戳格式
            //"GivenName" => "Jonny",                           # 自定义字段
            //"Surname" => "Rocket",                            # 自定义字段
            //"Email" => "jrocket@example.com",                 # 自定义字段
            //"Role" => ["Manager", "Project Administrator"]    # 自定义字段

            #endregion

            var tokenModel = new TokenModel();
            tokenModel.Id = Guid.NewGuid().FormatString(removeSplit: true);
            tokenModel.AccountId = GuidHelper.Get(accountId);
            tokenModel.Account = account;
            tokenModel.Mobile = mobile;
            tokenModel.Email = email;
            tokenModel.RealName = realName;
            tokenModel.Role = role;
            tokenModel.Type = type;
            tokenModel.Status = status;
            tokenModel.LastDt = nowDt;
            tokenModel.ExpiresDt = expiresDt;

            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey));
            var jwtToken = new JwtSecurityToken(
                   issuer: setting.Issuer,
                   audience: Current.AppCode,
                   //验证时间根据redis的有效期处理
                   //notBefore: nowDt,
                   //expires: expiresDt,
                   claims: tokenModel.ToClaims(),
                   signingCredentials: new SigningCredentials(security, SecurityAlgorithms.HmacSha256));
            var token = (new JwtSecurityTokenHandler()).WriteToken(jwtToken);

            tokenModel.Token = token;
            Store.Set<TokenModel>(accountId, tokenModel, GetStoreExpireTime(nowDt));

            return tokenModel.Token;

        }

        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Remove(string token)
        {
            string accountId = GetAccountIdByToken(token);
            if (!accountId.IsEmpty())
            {
                Store.Remove(accountId);
            }
            return true;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Refresh(string token)
        {
            var jwtToken = Resolve(token);
            if (jwtToken.IsNull())
                return string.Empty;

            var accountId = GetAccountIdByToken(jwtToken);
            if (accountId.IsEmpty())
                return string.Empty;

            var storeModel = Store.Get<TokenModel>(accountId);
            if (storeModel.IsNull())
                return string.Empty;

            if (storeModel.AccountId.FormatString() != accountId)
                return string.Empty;

            return Generate(accountId, storeModel.Account, storeModel.Mobile, storeModel.Email, storeModel.RealName, storeModel.Role, storeModel.Type, storeModel.Status);
        }

        /// <summary>
        /// 校验Token
        /// 验证成功后：按配置规则延长过期时间
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public EnumCode Verify(string token)
        {
            TokenModel sotreModel = null;
            return Verify(token, out sotreModel);
        }

        /// <summary>
        /// 校验Token
        /// 验证成功后：按配置规则延长过期时间
        /// </summary>
        /// <param name="token"></param>
        /// <param name="storeModel">TokenModel(store)</param>
        /// <returns></returns>
        public EnumCode Verify(string token, out TokenModel storeModel)
        {
            EngineHelper.Configuration.Authentication.CheckNull();

            storeModel = null;
            var setting = EngineHelper.Configuration.Authentication;
            var nowDt = DateTime.Now;

            var jwtToken = Resolve(token);
            if (jwtToken.IsNull())
            {
                return EnumCode.认证失败;
            }

            var accountId = GetAccountIdByToken(jwtToken);
            if (accountId.IsEmpty())
            {
                return EnumCode.认证失败;
            }

            storeModel = Store.Get<TokenModel>(accountId);
            if (storeModel.IsNull())
            {
                return EnumCode.认证失败;
            }

            if (storeModel.AccountId.FormatString() != accountId)
            {
                return EnumCode.认证失败;
            }

            if (nowDt > storeModel.ExpiresDt)
            {
                return EnumCode.认证过期;
            }

            storeModel.LastDt = nowDt;
            storeModel.ExpiresDt = nowDt.AddSeconds(setting.ExpireTime);
            Store.Set<TokenModel>(accountId, storeModel, GetStoreExpireTime(nowDt));

            return EnumCode.成功;
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken Resolve(string token)
        {
            if (token.IsEmpty())
                return null;
            JwtSecurityToken jwtToken = null;
            try
            {
                jwtToken = (new JwtSecurityTokenHandler()).ReadJwtToken(token);
            }
            catch (Exception ex)
            {
            }
            return jwtToken;
            //var handle = new JwtSecurityTokenHandler();
            //var model = handle.ReadJwtToken(token);
            //return new ClaimsIdentity(model.Claims, authenticationType);
        }

        /// <summary>
        /// 从Token中获取AccountId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string GetAccountIdByToken(string token)
        {
            return GetAccountIdByToken(Resolve(token));
        }

        /// <summary>
        /// 从JwtToken中获取AccountId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string GetAccountIdByToken(JwtSecurityToken jwtToken)
        {
            if (!jwtToken.IsNull())
            {
                var accountClaim = jwtToken.Claims.Where(x => x.Type == ClaimTypesExtend.AccountId).FirstOrDefault();
                if (!accountClaim.IsNull())
                {
                    return accountClaim.Value;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 未使用1天后过期(自动删除)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private DateTime GetStoreExpireTime(DateTime dateTime)
        {
            EngineHelper.Configuration.Authentication.CheckNull();

            var setting = EngineHelper.Configuration.Authentication;
            if (!setting.IsNull() && !setting.TokenStore.IsNull() && setting.TokenStore.ExpireTime > 0)
            {
                return dateTime.AddSeconds(setting.TokenStore.ExpireTime);
            }
            else
            {
                return dateTime.AddDays(1);
            }
        }
    }
}
