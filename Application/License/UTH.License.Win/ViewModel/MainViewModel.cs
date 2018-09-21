using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using UTH.Infrastructure.Resource;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.License.Win.ViewModel
{
    public class MainViewModel : UTHViewModel
    {
        public MainViewModel() : base(culture.Lang.winZhuJieMian, "")
        {
            Initialize();
        }

        #region 私有变量

        protected string KeyFileRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        //公钥文件路径(保存/防止直接覆盖了读取key文件)
        protected string PublicKeyFilePath
        {
            get { return KeyFileRootPath + "/key/uth.key"; }
        }

        //私钥文件路径(保存/防止直接覆盖了读取key文件)
        private string PrivateKeyFilePath
        {
            get
            {
                return KeyFileRootPath + "/key/private.key";
            }
        }

        //授权文件保存路径格式
        private string LicenseFilePathFormat
        {
            get
            {
                return KeyFileRootPath + "/key/license/{0}/license.key";
            }
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 授权信息
        /// </summary>
        public LicenseObservable LicenseSource
        {
            get { return _licenseSource; }
            set
            {
                _licenseSource = value;
                RaisePropertyChanged(() => LicenseSource);
            }
        }
        private LicenseObservable _licenseSource = new LicenseObservable() { Product = "UTH.Meeting.Win" };

        /// <summary>
        /// 版本列表
        /// (基础版)basic,(专业版)professional,(企业版)enterprise
        /// </summary>
        public string[] EditionSource
        {
            get { return _editionSource; }
            set { _editionSource = value; RaisePropertyChanged(() => EditionSource); }
        }
        private string[] _editionSource = new string[] { "basic", "professional", "enterprise" };

        /// <summary>
        /// 获取设置信息
        /// </summary>
        public RelayCommand OnSysteminfo { get; set; }

        /// <summary>
        /// 生成公/私钥对
        /// </summary>
        public RelayCommand OnSignFile { get; set; }

        /// <summary>
        /// 生成密钥文件
        /// </summary>
        public RelayCommand OnGenerateLic { get; set; }

        /// <summary>
        /// 验证密钥文件
        /// </summary>
        public RelayCommand OnVerifyLic { get; set; }

        /// <summary>
        /// 打开文件目录
        /// </summary>
        public RelayCommand OnOpenFileDic { get; set; }


        #endregion

        #region 辅助操作

        private void Initialize()
        {
            OnSysteminfo = new RelayCommand(() =>
            {
                LicenseSource.Mac = MacHelper.GetMacAddress();
                LicenseSource.CPU = Win32Helper.GetCpuID();
            });

            OnSignFile = new RelayCommand(() =>
             {
                 this.ViewConfirm("确定生成新的公/私钥文件至Key文件夹，操作会覆盖替换原文件（是/否）？", () =>
                 {
                     if (LicenseFileHelper.SavePemKeyFile(PublicKeyFilePath, PrivateKeyFilePath))
                     {
                         AppEvent.ProcessStart.SendAction<TParam<string, Action>>(this, new TParam<string, Action>()
                         {
                             Param1 = System.IO.Path.GetDirectoryName(PublicKeyFilePath)
                         });
                     }
                     else
                     {
                         this.ViewMessage("密钥文件保存失败");
                     }
                 });
             });

            OnGenerateLic = new RelayCommand(() =>
            {
                if (LicenseSource.Product.IsEmpty() || LicenseSource.Edition.IsEmpty() || LicenseSource.Email.IsEmpty() || LicenseSource.Mobile.IsEmpty() ||
                    LicenseSource.Mac.IsEmpty() || LicenseSource.CPU.IsEmpty() || LicenseSource.UseTimes.IsEmpty() || LicenseSource.ExpirationTime.IsEmpty() || LicenseSource.IsTrial.IsEmpty())
                {
                    this.ViewMessage("请输入内容(标识、邮箱、手机，Mac地址，CPU，使用次数，过期时间等信息)");
                    return;
                }

                var licenseModel = new LicenseModel()
                {
                    Product = LicenseSource.Product,
                    Edition = LicenseSource.Edition,
                    Email = LicenseSource.Email,
                    Mobile = LicenseSource.Mobile,
                    Mac = LicenseSource.Mac,
                    CPU = LicenseSource.CPU,
                    UseTimes = LicenseSource.UseTimes,
                    ExpirationTime = LicenseSource.ExpirationTime,
                    IsTrial = LicenseSource.IsTrial
                };

                var filePath = string.Format(LicenseFilePathFormat, (LicenseSource.Mac + LicenseSource.CPU).Replace("-", ""));

                if (LicenseFileHelper.SavePemLicenseFile(licenseModel, filePath, PrivateKeyFilePath))
                {
                    this.ViewMessage("授权文件保存成功", () =>
                    {
                        AppEvent.ProcessStart.SendAction<TParam<string, Action>>(this, new TParam<string, Action>()
                        {
                            Param1 = System.IO.Path.GetDirectoryName(filePath)
                        });
                    });
                }
                else
                {
                    this.ViewMessage("授权文件保存失败");
                }
            });

            OnVerifyLic = new RelayCommand(() =>
            {
                if (LicenseSource.Mac.IsEmpty() || LicenseSource.CPU.IsEmpty())
                {
                    this.ViewMessage("信息错误");
                    return;
                }

                var filePath = string.Format(LicenseFilePathFormat, (LicenseSource.Mac + LicenseSource.CPU).Replace("-", ""));
                if (!File.Exists(filePath))
                {
                    this.ViewMessage("未找到授权文件");
                    return;
                }

                var model = LicenseFileHelper.GetPemLicense(filePath, PublicKeyFilePath);
                if (model.IsNull())
                {
                    this.ViewMessage("授权文件错误");
                    return;
                }

                this.ViewMessage("授权文件校验通过");

            });

            OnOpenFileDic = new RelayCommand(() =>
            {
                AppEvent.ProcessStart.SendAction<TParam<string, Action>>(this, new TParam<string, Action>()
                {
                    Param1 = System.IO.Path.GetDirectoryName(PublicKeyFilePath),
                    Param2 = null
                });
            });
        }

        #endregion
    }
}