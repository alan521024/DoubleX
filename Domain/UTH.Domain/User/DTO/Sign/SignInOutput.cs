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
    /// 账户签入返回
    /// </summary>
    public class SignInOutput:IOutput
    {
        /// <summary>
        /// 账号编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public EnumAccountType Type { get; set; }


        /// <summary>
        /// 登录Token
        /// </summary>
        public string Token { get; set; }
    }
}
