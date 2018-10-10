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
    /// Token业务服务
    /// </summary>
    public class TokenService : ITokenService
    {
        public TokenService(ITokenStore store)
        {
            Store = store;
        }

        /// <summary>
        /// 存储
        /// </summary>
        protected ITokenStore Store { get; }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public string Generate(string appCode, Guid id, string account, string mobile, string email, string realName, string role, string organize, string employe, EnumAccountType type, EnumAccountStatus status)
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


            var model = new TokenModel() { _Key = Guid.NewGuid().FormatString(removeSplit: true), AppCode = appCode };
            model.Id = id;
            model.Account = account;
            model.Mobile = mobile;
            model.Email = email;
            model.RealName = realName;
            model.Role = role;
            model.Organize = organize;
            model.Employe = employe;
            model.Type = type;
            model.Status = status;
            model.LastDt = nowDt;
            model.ExpiresDt = expiresDt;

            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey));
            var jwtToken = new JwtSecurityToken(
                   issuer: setting.Issuer,
                   audience: appCode,
                   //验证时间根据redis的有效期处理
                   //notBefore: nowDt,
                   //expires: expiresDt,
                   claims: model.ToClaims(),
                   signingCredentials: new SigningCredentials(security, SecurityAlgorithms.HmacSha256));

            var token = (new JwtSecurityTokenHandler()).WriteToken(jwtToken);

            Store.Set<TokenModel>(id.FormatString(), model, GetStoreExpireTime(nowDt));

            return token;

        }

        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Remove(string token)
        {
            string id = GetAccountIdByToken(token);
            if (!id.IsEmpty())
            {
                Store.Remove(id);
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

            var id = GetAccountIdByToken(jwtToken);
            if (id.IsEmpty())
                return string.Empty;

            var model = Store.Get<TokenModel>(id);
            if (model.IsNull())
                return string.Empty;

            if (model.Id.FormatString() != id)
                return string.Empty;

            return Generate(model.AppCode, GuidHelper.Get(id), model.Account, model.Mobile, model.Email, model.RealName, model.Role, model.Organize, model.Employe, model.Type, model.Status);
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
        /// <param name="model">TokenModel(store)</param>
        /// <returns></returns>
        public EnumCode Verify(string token, out TokenModel model)
        {
            EngineHelper.Configuration.Authentication.CheckNull();

            model = null;
            var setting = EngineHelper.Configuration.Authentication;
            var nowDt = DateTime.Now;

            var jwtToken = Resolve(token);
            if (jwtToken.IsNull())
            {
                return EnumCode.认证失败;
            }

            var id = GetAccountIdByToken(jwtToken);
            if (id.IsEmpty())
            {
                return EnumCode.认证失败;
            }

            model = Store.Get<TokenModel>(id);
            if (model.IsNull())
            {
                return EnumCode.认证失败;
            }

            if (model.Id.FormatString() != id)
            {
                return EnumCode.认证失败;
            }

            if (nowDt > model.ExpiresDt)
            {
                return EnumCode.认证过期;
            }

            model.LastDt = nowDt;
            model.ExpiresDt = nowDt.AddSeconds(setting.ExpireTime);
            Store.Set<TokenModel>(id, model, GetStoreExpireTime(nowDt));

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
                var accountClaim = jwtToken.Claims.Where(x => x.Type == ClaimTypesExtend.Id).FirstOrDefault();
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
