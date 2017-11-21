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
            ViewModelLocator.Instance.AuthenticationViewModel.Identifying += AuthenticationViewModel_Identifying;
            ViewModelLocator.Instance.Page3ButtonsViewModel.OnPaymentClicked += Page3ButtonsViewModel_OnPaymentClicked;
        }

        void AuthenticationViewModel_Identifying(object sender, EventArgs e)
        {
            ViewModelPage = controlsList[1];
        }

        void Page3ButtonsViewModel_OnPaymentClicked(object sender, EventArgs e)
        {
            ViewModelPage = controlsList[2];
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
        private List<UserControl> controlsList = new List<UserControl>() {
            new Controls.UserControlAuthentication(),
            new Controls.UserControl3Buttons(),
            // add pay control
            new Controls.UserControlPay(),
        };

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
