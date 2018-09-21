namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using FluentValidation;
    using FluentValidation.Validators;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 验证信息对象解析实现
    /// </summary>
    public class FluentValidatorDefaultFactory : ValidatorFactoryBase, IValidatorFactory
    {
        public FluentValidatorDefaultFactory() : base() { }

        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = EngineHelper.Resolve(validatorType) as IValidator;
            return validator;
        }
    }
}
