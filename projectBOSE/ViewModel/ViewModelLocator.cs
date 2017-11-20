/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:SecurityControl.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
//using SecurityControl.Model;
//using SecurityControl.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using projectBOSE.ClientAuthentication;

namespace projectBOSE.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        private static ViewModelLocator instance;

        public static ViewModelLocator Instance
        {
            get { return instance; }
        }
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //SimpleIoc.Default.Register<IDbService, DbService>();
            //SimpleIoc.Default.Register<IComPortService>(() => new ComPortService());

            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();
            SimpleIoc.Default.Register<IAuthViewModel, AuthViewModel>();
            SimpleIoc.Default.Register<IPage3ButtonsViewModel, Page3ButtonsViewModel>();

            //SimpleIoc.Default.Register<ITimeViewModel>(() => new TimeViewModel() { BottomLabelText = "", DateProperty = DateTime.Now }, "PC");
            //SimpleIoc.Default.Register<ITimeViewModel>(() => new TimeViewModel() { BottomLabelText = "", DateProperty = null }, "PRS");
        }

        public ViewModelLocator()
        {
            if (instance != null)
            {
                if (ViewModelBase.IsInDesignModeStatic) return;
                else throw new Exception("Can be only one instance. Use ViewModelLocator.Instance instead.");
            }
            instance = this;
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IMainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMainViewModel>();
            }
        }

        public IAuthViewModel AuthenticationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IAuthViewModel>();
            }
        }

        public IPage3ButtonsViewModel Page3ButtonsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPage3ButtonsViewModel>();
            }
        }

        //public ITimeViewModel PCTimeViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ITimeViewModel>("PC");
        //    }
        //}

        //public ITimeViewModel PRSTimeViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ITimeViewModel>("PRS");
        //    }
        //}

        //public IPRSDatabaseViewModel PRSDatabaseViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IPRSDatabaseViewModel>();
        //    }
        //}

        //public IMarksDatabaseViewModel MarksDatabaseViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IMarksDatabaseViewModel>();
        //    }
        //}

        //public IEventsViewModel EventsViewModel
        //{ 
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IEventsViewModel>();
        //    }
        //}

        //public IStatisticsControlViewModel StatisticsControlViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IStatisticsControlViewModel>();
        //    }
        //}

        //public IConnectedPRSViewModel ConnectedPRSViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IConnectedPRSViewModel>();
        //    }
        //}

        //public IDbService DatabaseService
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IDbService>();
        //    }
        //}

        //public IComPortService ComPortService
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<IComPortService>();
        //    }
        //}

        //public void RunOperationAsynchronously(Action action)
        //{
        //            Main.IsFree = false;
        //            Main.OperationProgress = 10;
        //            try
        //            {
        //                //if (action != null)  action.Invoke();
        //                action();
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Windows.Forms.MessageBox.Show("Operation failure: " + ex.ToString());
        //            }
        //            Main.IsFree = true;
        //            Main.OperationProgress = 0;
        //}

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {

        }
    }
}