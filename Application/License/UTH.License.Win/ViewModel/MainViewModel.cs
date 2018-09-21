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

        #region ˽�б���

        protected string KeyFileRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        //��Կ�ļ�·��(����/��ֱֹ�Ӹ����˶�ȡkey�ļ�)
        protected string PublicKeyFilePath
        {
            get { return KeyFileRootPath + "/key/uth.key"; }
        }

        //˽Կ�ļ�·��(����/��ֱֹ�Ӹ����˶�ȡkey�ļ�)
        private string PrivateKeyFilePath
        {
            get
            {
                return KeyFileRootPath + "/key/private.key";
            }
        }

        //��Ȩ�ļ�����·����ʽ
        private string LicenseFilePathFormat
        {
            get
            {
                return KeyFileRootPath + "/key/license/{0}/license.key";
            }
        }

        #endregion

        #region ��������

        /// <summary>
        /// ��Ȩ��Ϣ
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
        /// �汾�б�
        /// (������)basic,(רҵ��)professional,(��ҵ��)enterprise
        /// </summary>
        public string[] EditionSource
        {
            get { return _editionSource; }
            set { _editionSource = value; RaisePropertyChanged(() => EditionSource); }
        }
        private string[] _editionSource = new string[] { "basic", "professional", "enterprise" };

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public RelayCommand OnSysteminfo { get; set; }

        /// <summary>
        /// ���ɹ�/˽Կ��
        /// </summary>
        public RelayCommand OnSignFile { get; set; }

        /// <summary>
        /// ������Կ�ļ�
        /// </summary>
        public RelayCommand OnGenerateLic { get; set; }

        /// <summary>
        /// ��֤��Կ�ļ�
        /// </summary>
        public RelayCommand OnVerifyLic { get; set; }

        /// <summary>
        /// ���ļ�Ŀ¼
        /// </summary>
        public RelayCommand OnOpenFileDic { get; set; }


        #endregion

        #region ��������

        private void Initialize()
        {
            OnSysteminfo = new RelayCommand(() =>
            {
                LicenseSource.Mac = MacHelper.GetMacAddress();
                LicenseSource.CPU = Win32Helper.GetCpuID();
            });

            OnSignFile = new RelayCommand(() =>
             {
                 this.ViewConfirm("ȷ�������µĹ�/˽Կ�ļ���Key�ļ��У������Ḳ���滻ԭ�ļ�����/�񣩣�", () =>
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
                         this.ViewMessage("��Կ�ļ�����ʧ��");
                     }
                 });
             });

            OnGenerateLic = new RelayCommand(() =>
            {
                if (LicenseSource.Product.IsEmpty() || LicenseSource.Edition.IsEmpty() || LicenseSource.Email.IsEmpty() || LicenseSource.Mobile.IsEmpty() ||
                    LicenseSource.Mac.IsEmpty() || LicenseSource.CPU.IsEmpty() || LicenseSource.UseTimes.IsEmpty() || LicenseSource.ExpirationTime.IsEmpty() || LicenseSource.IsTrial.IsEmpty())
                {
                    this.ViewMessage("����������(��ʶ�����䡢�ֻ���Mac��ַ��CPU��ʹ�ô���������ʱ�����Ϣ)");
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
                    this.ViewMessage("��Ȩ�ļ�����ɹ�", () =>
                    {
                        AppEvent.ProcessStart.SendAction<TParam<string, Action>>(this, new TParam<string, Action>()
                        {
                            Param1 = System.IO.Path.GetDirectoryName(filePath)
                        });
                    });
                }
                else
                {
                    this.ViewMessage("��Ȩ�ļ�����ʧ��");
                }
            });

            OnVerifyLic = new RelayCommand(() =>
            {
                if (LicenseSource.Mac.IsEmpty() || LicenseSource.CPU.IsEmpty())
                {
                    this.ViewMessage("��Ϣ����");
                    return;
                }

                var filePath = string.Format(LicenseFilePathFormat, (LicenseSource.Mac + LicenseSource.CPU).Replace("-", ""));
                if (!File.Exists(filePath))
                {
                    this.ViewMessage("δ�ҵ���Ȩ�ļ�");
                    return;
                }

                var model = LicenseFileHelper.GetPemLicense(filePath, PublicKeyFilePath);
                if (model.IsNull())
                {
                    this.ViewMessage("��Ȩ�ļ�����");
                    return;
                }

                this.ViewMessage("��Ȩ�ļ�У��ͨ��");

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