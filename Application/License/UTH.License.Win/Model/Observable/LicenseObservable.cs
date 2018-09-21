using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using UTH.Infrastructure.Resource;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.License.Win
{
    public class LicenseObservable : ObservableObject, INotifyPropertyChanged
    {
        public string IsTrial
        {
            get { return _isTrial; }
            set { _isTrial = value; RaisePropertyChanged(() => IsTrial); }
        }
        private string _isTrial = "1";

        public string Product
        {
            get { return _product; }
            set { _product = value; RaisePropertyChanged(() => Product); }
        }
        private string _product = "";

        public string Edition
        {
            get { return _edition; }
            set { _edition = value; RaisePropertyChanged(() => Edition); }
        }
        private string _edition = "basic";

        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(() => Email); }
        }
        private string _email = "meeting_try@utranshub.com";

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; RaisePropertyChanged(() => Mobile); }
        }
        private string _mobile = "18600000000";

        public string Mac
        {
            get { return _mac; }
            set { _mac = value; RaisePropertyChanged(() => Mac); }
        }
        private string _mac = "XX-XX-XX-XX-XX-XX";

        public string CPU
        {
            get { return _cpu; }
            set { _cpu = value; RaisePropertyChanged(() => CPU); }
        }
        private string _cpu = "XXXXXXXXXXXXXXXX";

        public string UseTimes
        {
            get { return _useTimes; }
            set { _useTimes = value; RaisePropertyChanged(() => UseTimes); }
        }
        private string _useTimes = "0";

        /// <summary>
        /// 日期格式：1900-01-01(不过期) 指定日期过期
        /// 数字格式：eg:30  第一次安装后，指定天数过期
        /// </summary>
        public string ExpirationTime
        {
            get { return _expirationTime; }
            set { _expirationTime = value; RaisePropertyChanged(() => ExpirationTime); }
        }
        private string _expirationTime = "1900-01-01";
    }
}
