/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UTH.Update.Win"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace UTH.Update.Win.ViewModel
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

    public class AppViewModelLocator
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

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public StartupViewModel StartupModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartupViewModel>();
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