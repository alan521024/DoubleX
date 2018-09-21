using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 手机号码验证
    /// </summary>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MobileNumberAttribute : ValidationAttribute
    {
        public MobileNumberAttribute()
        {

        }

        private bool NullOrEmpty { get; set; }

        public MobileNumberAttribute(bool nullOrEmpty = false)
        {
            this.NullOrEmpty = nullOrEmpty;
        }

        private bool validate(object value)
        {
            //TODO:手机号码验证(考虑下国家)
            return true;
        }

        public override bool IsValid(object value)
        {
            return validate(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validate(value))
            {
                return ValidationResult.Success;
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return new ValidationResult(string.Format("{0} 字段非法", validationContext.DisplayName), new[] { validationContext.MemberName });
            }
        }
    }
}
