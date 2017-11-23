using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace projectBOSE.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    class MainViewModel : ViewModelBase, projectBOSE.ViewModel.IMainViewModel
    {
        public MainViewModel()
        {
            //_viewModelPage = pagesList[0];
            _viewModelPage = controlsList[0];
            // Подписка на обработчики событий
            ViewModelLocator.Instance.AuthenticationViewModel.Identifying += AuthenticationViewModel_Identifying;
            ViewModelLocator.Instance.Page3ButtonsViewModel.paymentPressed += Page3ButtonsViewModel_paymentPressed;

        }

        private PaymentWindow paymentWindow;

        void Page3ButtonsViewModel_paymentPressed(object sender, EventArgs e)
        {
            if (paymentWindow != null) return;
            paymentWindow = new PaymentWindow();
            paymentWindow.Closed += paymentWindow_Closed;
            paymentWindow.Show();
        }

        void paymentWindow_Closed(object sender, EventArgs e)
        {
            paymentWindow.Closed -= paymentWindow_Closed;
            paymentWindow = null;
        }

        void AuthenticationViewModel_Identifying(object sender, EventArgs e)
        {
            ViewModelPage = controlsList[1];
        }

        // команда добавления нового объекта
        //private RelayCommand auth_Command;
        //public RelayCommand AuthCommand
        //{
        //    get
        //    {
        //        return auth_Command ??
        //          (auth_Command = new RelayCommand(obj =>
        //          {
        //              int x = 0345;
        //              for(int i =0; i < 10; i++)
        //              {
        //                  int y = 3;
        //              }
        //                //Действия
        //          }));
        //    }
        //}
        
        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName]string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}


        //private List<Page> pagesList = new List<Page>() {new PageAuthentication(), new Page3Buttons() };
        private List<UserControl> controlsList = new List<UserControl>() { new Controls.UserControlAuthentication(), new Controls.UserControl3Buttons() };

        /// <summary>
        /// The <see cref="ViewModelPage" /> property's name.
        /// </summary>
        public const string _viewModelPagePropertyName = "ViewModelPage";

        private UserControl _viewModelPage = null;

        public UserControl ViewModelPage
        {
            get
            {
                return _viewModelPage;
            }

            set
            {
                //this.OnPropertyChanging("MyProperty");
                //_viewModelPage = value;
                //this.OnPropertyChanged("MyProperty");


                if (_viewModelPage == value)
                    return;
                _viewModelPage = value;
                RaisePropertyChanged(_viewModelPagePropertyName);
            }
        }


    }
}
