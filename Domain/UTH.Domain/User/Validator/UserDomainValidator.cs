namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 用户模块领域验证规则
    /// </summary>
    public static class UserDomainValidator
    {
        public const int PasswordLengthMin = 6;
        public const int PasswordLengthMax = 36;
    }
}
