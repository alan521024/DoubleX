namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using Castle.DynamicProxy;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    public static class FluentValidationOptions
    {
        public static void Configuration()
        {
            ValidatorOptions.LanguageManager = new FluentValidatorLanguageManager();
        }
    }
}
