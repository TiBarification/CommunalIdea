using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace projectBOSE.ViewModel
{
    class Page3ButtonsViewModel : ViewModelBase, IPage3ButtonsViewModel
    {


        RelayCommand _paymentCommand, _payersCommand, _settingsCommand;


        public RelayCommand PaymentCommand
        {
            get
            {
                return _paymentCommand ?? (_paymentCommand = new RelayCommand(this.PaymentCommandMethod));
            }
        }

        public RelayCommand PayersCommand
        {
            get
            {
                return _payersCommand ?? (_payersCommand = new RelayCommand(this.PayersCommandMethod));
            }
        }

        public RelayCommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new RelayCommand(this.SettingsCommandMethod));
            }
        }



        private void PaymentCommandMethod()
        {

        }

        private void PayersCommandMethod()
        {

        }
        private void SettingsCommandMethod()
        {

        }


    }
}
