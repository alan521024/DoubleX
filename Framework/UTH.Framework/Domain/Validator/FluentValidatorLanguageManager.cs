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

    public class FluentValidatorLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public FluentValidatorLanguageManager()
        {
            //语言标识
            //"zh-CN","en","hr","cs","da","nl","fi","fr","ka","de","hi","it","ko","mk","fa","pl","pt","ro","ru","es","sv","tr";

            #region 规则列表(示例-中文)

            //Translate<EmailValidator>("'{PropertyName}' 不是有效的电子邮件地址。");
            //Translate<GreaterThanOrEqualValidator>("'{PropertyName}' 必须大于或等于 '{ComparisonValue}'。");
            //Translate<GreaterThanValidator>("'{PropertyName}' 必须大于 '{ComparisonValue}'。");
            //Translate<LengthValidator>("'{PropertyName}' 的长度必须在 {MinLength} 到 {MaxLength} 字符，您已经输入了 {TotalLength} 字符。");
            //Translate<MinimumLengthValidator>("\"{PropertyName}\"必须大于或等于{MinLength}个字符。您输入了{TotalLength}个字符。");
            //Translate<MaximumLengthValidator>("\"{PropertyName}\"必须小于或等于{MaxLength}个字符。您输入了{TotalLength}个字符。");
            //Translate<LessThanOrEqualValidator>("'{PropertyName}' 必须小于或等于 '{ComparisonValue}'。");
            //Translate<LessThanValidator>("'{PropertyName}' 必须小于 '{ComparisonValue}'。");
            //Translate<NotEmptyValidator>("请填写 '{PropertyName}'。");
            //Translate<NotEqualValidator>("'{PropertyName}' 不能和 '{ComparisonValue}' 相等。");
            //Translate<NotNullValidator>("请填写 '{PropertyName}'。");
            //Translate<PredicateValidator>("指定的条件不符合 '{PropertyName}'。");
            //Translate<AsyncPredicateValidator>("指定的条件不符合 '{PropertyName}'。");
            //Translate<RegularExpressionValidator>("'{PropertyName}' 的格式不正确。");
            //Translate<EqualValidator>("'{PropertyName}' 应该和 '{ComparisonValue}' 相等。");
            //Translate<ExactLengthValidator>("'{PropertyName}' 必须是 {MaxLength} 个字符，您已经输入了 {TotalLength} 字符。");
            //Translate<InclusiveBetweenValidator>("'{PropertyName}' 必须在 {From} 和 {To} 之间， 您输入了 {Value}。");
            //Translate<ExclusiveBetweenValidator>("'{PropertyName}' 必须在 {From} 和 {To} 之外， 您输入了 {Value}。");
            //Translate<CreditCardValidator>("'{PropertyName}' 不是有效的信用卡号。");
            //Translate<ScalePrecisionValidator>("'{PropertyName}' 总位数不能超过 {expectedPrecision} 位，其中整数部分 {expectedScale} 位。您填写了 {digits} 位小数和 {actualScale} 位整数。");
            //Translate<EmptyValidator>("\"{PropertyName}\"应该是空的。");
            //Translate<NullValidator>("\"{PropertyName}\"必须为空。");
            //Translate<EnumValidator>("\"{PropertyName}\"的值范围不包含\"{PropertyValue}\"。");

            #endregion

            #region 规则列表(示例-英文)

            //Translate<EmailValidator>("'{PropertyName}' is not a valid email address.");
            //Translate<GreaterThanOrEqualValidator>("'{PropertyName}' must be greater than or equal to '{ComparisonValue}'.");
            //Translate<GreaterThanValidator>("'{PropertyName}' must be greater than '{ComparisonValue}'.");
            //Translate<LengthValidator>("'{PropertyName}' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.");
            //Translate<MinimumLengthValidator>("The length of '{PropertyName}' must be at least {MinLength} characters. You entered {TotalLength} characters.");
            //Translate<MaximumLengthValidator>("The length of '{PropertyName}' must {MaxLength} characters or fewer. You entered {TotalLength} characters.");
            //Translate<LessThanOrEqualValidator>("'{PropertyName}' must be less than or equal to '{ComparisonValue}'.");
            //Translate<LessThanValidator>("'{PropertyName}' must be less than '{ComparisonValue}'.");
            //Translate<NotEmptyValidator>("'{PropertyName}' should not be empty.");
            //Translate<NotEqualValidator>("'{PropertyName}' should not be equal to '{ComparisonValue}'.");
            //Translate<NotNullValidator>("'{PropertyName}' must not be empty.");
            //Translate<PredicateValidator>("The specified condition was not met for '{PropertyName}'.");
            //Translate<AsyncPredicateValidator>("The specified condition was not met for '{PropertyName}'.");
            //Translate<RegularExpressionValidator>("'{PropertyName}' is not in the correct format.");
            //Translate<EqualValidator>("'{PropertyName}' should be equal to '{ComparisonValue}'.");
            //Translate<ExactLengthValidator>("'{PropertyName}' must be {MaxLength} characters in length. You entered {TotalLength} characters.");
            //Translate<InclusiveBetweenValidator>("'{PropertyName}' must be between {From} and {To}. You entered {Value}.");
            //Translate<ExclusiveBetweenValidator>("'{PropertyName}' must be between {From} and {To} (exclusive). You entered {Value}.");
            //Translate<CreditCardValidator>("'{PropertyName}' is not a valid credit card number.");
            //Translate<ScalePrecisionValidator>("'{PropertyName}' may not be more than {expectedPrecision} digits in total, with allowance for {expectedScale} decimals. {digits} digits and {actualScale} decimals were found.");
            //Translate<EmptyValidator>("'{PropertyName}' should be empty.");
            //Translate<NullValidator>("'{PropertyName}' must be empty.");
            //Translate<EnumValidator>("'{PropertyName}' has a range of values which does not include '{PropertyValue}'.");

            #endregion

            var cultureZHCN = new CultureInfo("zh-CN");
            var cultureEN = new CultureInfo("en");

            AddTranslation("zh-CN", "NotNullValidator", Lang.ResourceManager.GetString("sysQingShuRuFormat", cultureZHCN));
            AddTranslation("en", "NotNullValidator", Lang.ResourceManager.GetString("sysQingShuRuFormat", cultureEN));

            AddTranslation("zh-CN", "NotEmptyValidator", Lang.ResourceManager.GetString("sysQingShuRuFormat", cultureZHCN));
            AddTranslation("en", "NotEmptyValidator", Lang.ResourceManager.GetString("sysQingShuRuFormat", cultureEN));

            AddTranslation("zh-CN", "LengthValidator", Lang.ResourceManager.GetString("sysChangDuQuJianFormat", cultureZHCN));
            AddTranslation("en", "LengthValidator", Lang.ResourceManager.GetString("sysChangDuQuJianFormat", cultureEN));
            
            AddTranslation("zh-CN", "EmailValidator", Lang.ResourceManager.GetString("sysQingShuRuYouXiaoFormat", cultureZHCN));
            AddTranslation("en", "EmailValidator", Lang.ResourceManager.GetString("sysQingShuRuYouXiaoFormat", cultureEN));

            AddTranslation("zh-CN", "MobileValidator", Lang.ResourceManager.GetString("sysQingShuRuYouXiaoFormat", cultureZHCN));
            AddTranslation("en", "MobileValidator", Lang.ResourceManager.GetString("sysQingShuRuYouXiaoFormat", cultureEN));

        }
    }
}
