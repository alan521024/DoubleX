namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// Web认证扩展
    /// </summary>
    public static class WebAuthentExtensions
    {
        /// <summary>
        /// 添加Web认证
        /// </summary>
        public static IServiceCollection AddAuthent(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<WebAuthentOptions>, WebAuthentConfigure>();
            return services.AddAuthent(WebAuthentOptions.DefaultScheme, _ => { });
        }

        /// <summary>
        /// 添加Web认证
        /// </summary>
        public static IServiceCollection AddAuthent(this IServiceCollection services, Action<WebAuthentOptions> configureOptions)
            => services.AddAuthent(WebAuthentOptions.DefaultScheme, configureOptions);

        /// <summary>
        /// 添加Web认证
        /// </summary>
        public static IServiceCollection AddAuthent(this IServiceCollection services, string authenticationScheme, Action<WebAuthentOptions> configureOptions)
        {
            var setting = EngineHelper.Configuration.Authentication;

            if (setting.AuthenticationType == EnumAuthenticationType.Cookie)
            {
                var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
                services.AddAuthentication(scheme)
                    .AddCookie(scheme, options =>
                     {
                         options.LoginPath = new PathString(setting.LoginPath);
                         options.AccessDeniedPath = new PathString(setting.AccessDeniedPath);
                         options.ExpireTimeSpan = TimeSpan.FromSeconds(setting.ExpireTime);
                         options.SlidingExpiration = true;
                     });
            }

            if (setting.AuthenticationType == EnumAuthenticationType.Jwt)
            {
                var scheme = JwtBearerDefaults.AuthenticationScheme;
                services.AddAuthentication(options => { options.DefaultAuthenticateScheme = scheme; options.DefaultChallengeScheme = scheme; })
                    .AddJwtBearer(scheme, options =>
                    {
                        //OnMessageReceived
                        //   通过:OnTokenValidated  ==>  (错误:OnChallenge)
                        //   未通过: OnChallenge
                        //OnAuthenticationFailed(只有错误：throw 异常)

                        options.Events = new JwtBearerEvents()
                        {
                            OnMessageReceived = (context) =>
                            {
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = (context) => JwtOnTokenValidated(context),
                            OnAuthenticationFailed = (context) => JwtOnAuthFailed(context),
                            OnChallenge = (context) => JwtOnChallengeResult(context)
                        };
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,                      //是否验证Issuer
                            ValidateAudience = true,                    //是否验证Audience
                            ValidIssuer = setting.Issuer,
                            ValidAudiences = setting.Audiences,
                            //验证时间根据redis的有效期处理
                            RequireExpirationTime = false,                //是否必须有过期时间
                                                                          //ValidateLifetime = true,                    //是否验证过期时间
                                                                          //ClockSkew = TimeSpan.Zero,                  //验证时间偏移量
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey))
                        };
                    });
            }

            return services;
        }

        private static Task JwtOnTokenValidated(TokenValidatedContext context)
        {
            context.CheckNull();

            //jwt token 格式校验成功后，与readis  token model 进行校验
            var jwtToken = (context.SecurityToken as JwtSecurityToken).RawData;

            //TODO:Redis TokenModel 可通该信息，修改Claims
            TokenModel storeToken = null;
            var verify = EngineHelper.Resolve<ITokenService>().Verify(jwtToken, out storeToken);
            if (verify == EnumCode.认证失败)
            {
                context.Fail(new Exception("invalid_token"));
            }
            else if (verify == EnumCode.认证过期)
            {
                context.Fail(new SecurityTokenExpiredException());
            }
            return Task.FromResult(0);
        }

        private static Task JwtOnAuthFailed(AuthenticationFailedContext context)
        {
            context.CheckNull();

            //Microsoft.IdentityModel.Tokens.SecurityTokenInvalidAudienceException
            if (context.Exception != null && context.Exception is DbxException)
            {
                return Task.CompletedTask;
            }
            throw new DbxException(EnumCode.认证错误, ExceptionHelper.GetMessage(context.Exception));
        }

        private static Task JwtOnChallengeResult(JwtBearerChallengeContext context)
        {
            context.CheckNull();

            string message = string.Empty;
            if (!context.ErrorUri.IsEmpty())
            {
                message = context.ErrorUri;
            }
            else if (!context.ErrorDescription.IsEmpty())
            {
                message = context.ErrorDescription;
            }
            else if (!context.Error.IsNull())
            {
                message = context.Error;
            }
            if (message.IsEmpty())
            {
                message = context.Options.Challenge;
            }

            var code = EnumCode.认证失败;
            if (!context.AuthenticateFailure.IsNull() && context.AuthenticateFailure is SecurityTokenExpiredException)
            {
                code = EnumCode.认证过期;
            }

            throw new DbxException(code, message);
        }
    }
}