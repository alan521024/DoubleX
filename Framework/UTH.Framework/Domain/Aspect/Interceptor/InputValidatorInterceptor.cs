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

    /// <summary>
    /// 输入校验拦截器
    /// </summary>
    public class InputValidatorInterceptor : IInputValidatorInterceptor, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //excute before
            InterceptBefore(invocation);

            //proceed
            invocation.Proceed();

            //excute after
            InterceptAfter(invocation);
        }

        private void InterceptBefore(IInvocation invocation)
        {
            if (!invocation.Arguments.IsEmpty())
            {
                List<KeyValueModel<Type, object>> validatorModels = new List<KeyValueModel<Type, object>>();

                var paramers = invocation.Method.GetParameters();
                for (var i = 0; i < invocation.Arguments.Length; i++)
                {
                    if (paramers[i].ParameterType.IsBaseFrom<IInput>())
                    {
                        validatorModels.Add(new KeyValueModel<Type, object>() { Key = paramers[i].ParameterType, Value = invocation.Arguments[i] });
                    }
                }

                if (validatorModels.IsEmpty())
                {
                    return;
                }

                var factory = EngineHelper.Resolve<IValidatorFactory>();
                if (factory.IsNull())
                {
                    return;
                }

                foreach (var item in validatorModels)
                {
                    if (item.Key.IsNull())
                        continue;
                    if (item.Value.IsNull())
                        continue;

                    IValidator validator = null;
                    try
                    {
                        validator=factory.GetValidator(item.Key);
                    }
                    catch (Exception ex) { }

                    if (validator.IsNull())
                    {
                        continue;
                    }
                    var result = validator.Validate(item.Value);
                    if (result != null && !result.IsValid && !result.Errors.IsEmpty())
                    {
                        throw FrameworkHelper.GeValidationException(result);
                    }
                }
            }
        }

        private void InterceptAfter(IInvocation invocation)
        {

        }

    }
}
