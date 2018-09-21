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
        public void UserInstall()
        {
            SimpleIoc.Default.Register<EmployeViewModel>();

            SimpleIoc.Default.Register<MeetingViewModel>();
            SimpleIoc.Default.Register<PresideViewModel>();
            SimpleIoc.Default.Register<ParticipantViewModel>();
        }

        #region User

        public EmployeViewModel EmployeModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EmployeViewModel>();
            }
        }

        #endregion

        #region Conference

        public MeetingViewModel MeetingModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetingViewModel>();
            }
        }

        public PresideViewModel PresideModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PresideViewModel>();
            }
        }

        public ParticipantViewModel ParticipantModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParticipantViewModel>();
            }
        }


        #endregion
    }
}

