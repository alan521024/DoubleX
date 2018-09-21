namespace FluentValidation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Internal;
    using Results;
    using Validators;

    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> Mobile<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new MobileValidator());
        }
    }
}
