using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 授权信息
    /// 考虑写注册表，统一字符串
    /// </summary>
    public class LicenseModel
    {
        /// <summary>
        /// 0试用,1正式
        /// </summary>
        public string IsTrial { get; set; } = "0";
        public string Product { get; set; } = "";
        public string Edition { get; set; } = "basic"; //(基础版)basic,(专业版)professional,(企业版)enterprise
        public string Email { get; set; } = "meeting_try@utranshub.com";
        public string Mobile { get; set; } = "18600000000";
        public string Mac { get; set; } = "XX-XX-XX-XX-XX-XX";
        public string CPU { get; set; } = "XXXXXXXXXXXXXXXX";
        public string UseTimes { get; set; } = "0";
        public string ExpirationTime { get; set; } = "1900-01-01"; //输入时，日期格式(1900-01-01,不过期)  /  数字(30,意为添加日期加30天)
    }
}
