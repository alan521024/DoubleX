#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/jeremyskinner/FluentValidation
#endregion

namespace FluentValidation.Validators
{
    using System;
    using System.Text.RegularExpressions;
    using Internal;
    using Resources;
    using Results;

    /// <summary>
    /// ÊÖ»úºÅÂëÐ£Âë
    /// </summary>
    public class MobileValidator : PropertyValidator, IRegularExpressionValidator, IMobileValidator
    {
        private readonly Regex regex;

        const string expression = @"^[1]+[3,4,5,7,8]+\d{9}"; //@"^1[34578]\\d{9}$";

        public MobileValidator() : base(new LanguageStringSource(nameof(MobileValidator)))
        {
            regex = CreateRegEx();
        }


        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return true;

            if (!regex.IsMatch((string)context.PropertyValue))
            {
                return false;
            }

            return true;
        }

        public string Expression => expression;

        private static Regex CreateRegEx()
        {
            // Workaround for CVE-2015-2526
            // If no REGEX_DEFAULT_MATCH_TIMEOUT is specified in the AppDomain, default to 2 seconds. 
            // if we're on Netstandard 1.0 we don't have access to AppDomain, so just always use 2 second timeout there. 

#if NETSTANDARD1_0
			return new Regex(expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
#else
            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
                }
            }
            catch
            {
            }


            return new Regex(expression, RegexOptions.IgnoreCase);
#endif

        }
    }

    public interface IMobileValidator : IRegularExpressionValidator
    {

    }
}
