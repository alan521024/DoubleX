/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UTH.Meeting.Win"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/
namespace UTH.Meeting.Win.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using CommonServiceLocator;
    using UTH.Infrastructure.Resource;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;

    public partial class AppViewModelLocator
    {
        public AppViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Design & Run

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    // Create design time view services and models
            //    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            //}
            //else
            //{
            //    // Create run time view services and models
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            #endregion

            SimpleIoc.Default.Register<StartupViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<HelpViewModel>();

            SimpleIoc.Default.Register<_LayoutSettingViewModel>();
            SimpleIoc.Default.Register<MySelfViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();

            SimpleIoc.Default.Register<_LayoutAccountViewModel>();
            SimpleIoc.Default.Register<EditPwdViewModel>();
            SimpleIoc.Default.Register<FindPwdViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegistViewModel>();
            SimpleIoc.Default.Register<RegAgreementViewModel>();

            SimpleIoc.Default.Register<MainViewModel>();

            //User
            this.UserInstall();
        }

        /* *如需每次都是新实例 使用 new 创建对象*/
        //return ServiceLocator.Current.GetInstance<SettingViewModel>();
        //return new SettingViewModel();

        public StartupViewModel StartupModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartupViewModel>();
            }
        }
        public AboutViewModel AboutModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }
        public HelpViewModel HelpModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HelpViewModel>();
            }
        }

        public _LayoutSettingViewModel LayoutSettingModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<_LayoutSettingViewModel>();
            }
        }
        public MySelfViewModel MySelfModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MySelfViewModel>();
            }
        }
        public SettingViewModel SettingModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingViewModel>();
            }
        }

        public _LayoutAccountViewModel LayoutAccountModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<_LayoutAccountViewModel>();
            }
        }
        public EditPwdViewModel EditPwdModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<EditPwdViewModel>();
                return new EditPwdViewModel();
            }
        }
        public FindPwdViewModel FindPwdModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<FindPwdViewModel>();
                return new FindPwdViewModel();
            }
        }
        public LoginViewModel LoginModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<LoginViewModel>();
                return new LoginViewModel();
            }
        }
        public RegistViewModel RegistModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<RegistViewModel>();
                return new RegistViewModel();
            }
        }
        public RegAgreementViewModel RegAgreementModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegAgreementViewModel>();
            }
        }

        public MainViewModel MainModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}

